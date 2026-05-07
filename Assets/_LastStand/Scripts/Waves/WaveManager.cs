// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections;
using System.Collections.Generic;
using LastStand.AI;
using LastStand.Spawning;
using UnityEngine;

namespace LastStand.Waves
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<WaveDefinition> waves = new();
        [SerializeField] private SpawnDirector spawnDirector;
        [SerializeField] private bool autoStartOnPlay;
        [SerializeField] private float firstWaveDelaySeconds = 2f;
        [SerializeField] private bool logWaveEvents;

        private int currentWaveIndex = -1;
        private WaveDefinition currentWave;
        private WaveState state = WaveState.Idle;
        private int enemiesSpawnedThisWave;
        private int totalEnemiesToSpawnThisWave;
        private readonly List<GameObject> aliveEnemies = new();
        private readonly Queue<EnemyDefinition> spawnQueue = new();
        private Coroutine waveRoutine;

        public WaveState State => state;
        public int CurrentWaveNumber => currentWave != null ? currentWave.WaveNumber : 0;
        public int CurrentWaveIndex => currentWaveIndex;
        public int TotalWaves => waves.Count;
        public int EnemiesSpawnedThisWave => enemiesSpawnedThisWave;
        public int TotalEnemiesToSpawnThisWave => totalEnemiesToSpawnThisWave;
        public int AliveEnemyCount
        {
            get
            {
                CleanupAliveEnemies();
                return aliveEnemies.Count;
            }
        }

        public bool IsFinalWave => currentWaveIndex >= 0 && currentWaveIndex == waves.Count - 1;
        public bool ExtractionShouldUnlock => currentWave != null && (currentWave.UnlockExtractionAfterWave || IsFinalWave) && state == WaveState.Completed;

        private void Start()
        {
            if (autoStartOnPlay)
            {
                StartWaves();
            }
        }

        public void StartWaves()
        {
            if (waves.Count == 0)
            {
                state = WaveState.Failed;
                Log("Cannot start waves because no WaveDefinition assets are assigned.");
                return;
            }

            StartWave(0);
        }

        public void StartWave(int waveIndex)
        {
            if (waveIndex < 0 || waveIndex >= waves.Count || waves[waveIndex] == null)
            {
                state = WaveState.Failed;
                Log($"Invalid wave index {waveIndex}.");
                return;
            }

            StopActiveRoutine();
            waveRoutine = StartCoroutine(RunWaveRoutine(waveIndex));
        }

        public void StopWaves()
        {
            StopActiveRoutine();
            currentWaveIndex = -1;
            currentWave = null;
            enemiesSpawnedThisWave = 0;
            totalEnemiesToSpawnThisWave = 0;
            spawnQueue.Clear();
            aliveEnemies.Clear();
            state = WaveState.Idle;
        }

        public void RegisterEnemyRemoved(GameObject enemy)
        {
            if (enemy != null)
            {
                aliveEnemies.Remove(enemy);
            }
        }

        public void NotifyEnemyDefeated(GameObject enemy)
        {
            RegisterEnemyRemoved(enemy);
        }

        [ContextMenu("Debug Start Waves")]
        private void DebugStartWaves()
        {
            StartWaves();
        }

        [ContextMenu("Debug Start Current Wave")]
        private void DebugStartCurrentWave()
        {
            StartWave(Mathf.Max(0, currentWaveIndex));
        }

        private IEnumerator RunWaveRoutine(int waveIndex)
        {
            currentWaveIndex = waveIndex;
            currentWave = waves[waveIndex];
            state = WaveState.Starting;
            enemiesSpawnedThisWave = 0;
            aliveEnemies.Clear();
            BuildSpawnQueue(currentWave);
            totalEnemiesToSpawnThisWave = spawnQueue.Count;

            float delay = waveIndex == 0 ? firstWaveDelaySeconds : currentWave.StartDelaySeconds;
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            state = WaveState.Spawning;
            while (spawnQueue.Count > 0)
            {
                CleanupAliveEnemies();

                if (aliveEnemies.Count >= currentWave.MaxAliveAtOnce)
                {
                    yield return null;
                    continue;
                }

                EnemyDefinition enemyDefinition = spawnQueue.Dequeue();
                GameObject enemy = spawnDirector != null
                    ? spawnDirector.SpawnEnemy(enemyDefinition, currentWave.WaveNumber)
                    : null;

                if (enemy != null)
                {
                    aliveEnemies.Add(enemy);
                    enemiesSpawnedThisWave++;
                }
                else
                {
                    Log($"Spawn failed for wave {currentWave.WaveNumber}.");
                }

                if (spawnQueue.Count > 0)
                {
                    yield return new WaitForSeconds(currentWave.SpawnIntervalSeconds);
                }
            }

            state = WaveState.WaitingForClear;
            while (aliveEnemies.Count > 0)
            {
                CleanupAliveEnemies();
                yield return null;
            }

            if (currentWave.UnlockExtractionAfterWave || IsFinalWave)
            {
                state = WaveState.Completed;
                waveRoutine = null;
                yield break;
            }

            state = WaveState.Intermission;
            if (currentWave.IntermissionAfterWaveSeconds > 0f)
            {
                yield return new WaitForSeconds(currentWave.IntermissionAfterWaveSeconds);
            }

            int nextWaveIndex = currentWaveIndex + 1;
            if (nextWaveIndex < waves.Count)
            {
                waveRoutine = StartCoroutine(RunWaveRoutine(nextWaveIndex));
            }
            else
            {
                state = WaveState.Completed;
                waveRoutine = null;
            }
        }

        private void BuildSpawnQueue(WaveDefinition waveDefinition)
        {
            spawnQueue.Clear();
            if (waveDefinition == null)
            {
                return;
            }

            foreach (WaveEnemyEntry entry in waveDefinition.Enemies)
            {
                if (entry?.EnemyDefinition == null)
                {
                    continue;
                }

                for (int i = 0; i < entry.Count; i++)
                {
                    spawnQueue.Enqueue(entry.EnemyDefinition);
                }
            }
        }

        private void CleanupAliveEnemies()
        {
            aliveEnemies.RemoveAll(enemy => enemy == null || !enemy.activeInHierarchy);
        }

        private void StopActiveRoutine()
        {
            if (waveRoutine != null)
            {
                StopCoroutine(waveRoutine);
                waveRoutine = null;
            }
        }

        private void OnValidate()
        {
            firstWaveDelaySeconds = Mathf.Max(0f, firstWaveDelaySeconds);
        }

        private void Log(string message)
        {
            if (logWaveEvents)
            {
                Debug.Log($"[WaveManager] {message}", this);
            }
        }
    }
}
