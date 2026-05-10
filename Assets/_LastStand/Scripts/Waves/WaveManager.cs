// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections;
using System.Collections.Generic;
using LastStand.AI;
using LastStand.Spawning;
using LastStand.Stats;
using UnityEngine;

namespace LastStand.Waves
{
    public class WaveManager : MonoBehaviour
    {
        public event System.Action<WaveDefinition> FinalWaveCompleted;

        [SerializeField] private List<WaveDefinition> waves = new();
        [SerializeField] private SpawnDirector spawnDirector;
        [SerializeField] private LastStandStatsManager statsManager;
        [SerializeField] private bool autoStartOnPlay;
        [SerializeField] private float firstWaveDelaySeconds = 2f;
        [SerializeField] private float failedSpawnRetryDelaySeconds = 1f;
        [SerializeField] private bool logWaveEvents;

        private int currentWaveIndex = -1;
        private WaveDefinition currentWave;
        private WaveState state = WaveState.Idle;
        private int enemiesSpawnedThisWave;
        private int totalEnemiesToSpawnThisWave;
        private readonly List<GameObject> aliveEnemies = new();
        private readonly Queue<EnemyDefinition> spawnQueue = new();
        private Coroutine waveRoutine;
        private bool hasCompletedAllWaves;
        private int spawnFailureCountThisWave;
        [SerializeField] private string lastSpawnFailureReason;

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
        public bool HasCompletedAllWaves => hasCompletedAllWaves;
        public int PendingSpawnQueueCount => spawnQueue.Count;
        public int SpawnFailureCountThisWave => spawnFailureCountThisWave;
        public string LastSpawnFailureReason => lastSpawnFailureReason;

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

            if (statsManager != null)
            {
                statsManager.BeginRun(waves.Count);
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
            spawnFailureCountThisWave = 0;
            lastSpawnFailureReason = string.Empty;
            spawnQueue.Clear();
            aliveEnemies.Clear();
            state = WaveState.Idle;
            hasCompletedAllWaves = false;

            if (statsManager != null)
            {
                statsManager.EndRun();
                statsManager.SetWaveEnemyCounts(0, 0, 0);
            }
        }

        public void RegisterEnemyRemoved(GameObject enemy)
        {
            if (enemy != null)
            {
                aliveEnemies.Remove(enemy);
                PublishWaveEnemyCounts();
            }
        }

        public void NotifyEnemyDefeated(GameObject enemy)
        {
            if (statsManager != null)
            {
                statsManager.RegisterEnemyDefeated(enemy);
            }

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
            if (waveIndex == 0)
            {
                hasCompletedAllWaves = false;
            }

            enemiesSpawnedThisWave = 0;
            spawnFailureCountThisWave = 0;
            lastSpawnFailureReason = string.Empty;
            aliveEnemies.Clear();
            BuildSpawnQueue(currentWave);
            totalEnemiesToSpawnThisWave = spawnQueue.Count;
            PublishCurrentWaveStats();
            PublishWaveEnemyCounts();

            float delay = waveIndex == 0 ? firstWaveDelaySeconds : currentWave.StartDelaySeconds;
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            state = WaveState.Spawning;
            while (spawnQueue.Count > 0)
            {
                CleanupAliveEnemies();
                PublishWaveEnemyCounts();

                if (aliveEnemies.Count >= currentWave.MaxAliveAtOnce)
                {
                    yield return null;
                    continue;
                }

                EnemyDefinition enemyDefinition = spawnQueue.Peek();
                GameObject enemy = spawnDirector != null
                    ? spawnDirector.SpawnEnemy(enemyDefinition, currentWave.WaveNumber)
                    : null;

                if (enemy != null)
                {
                    spawnQueue.Dequeue();
                    aliveEnemies.Add(enemy);
                    ConfigureLifecycleReporter(enemy);
                    enemiesSpawnedThisWave++;
                    lastSpawnFailureReason = string.Empty;
                    PublishWaveEnemyCounts();

                    if (spawnQueue.Count > 0)
                    {
                        yield return new WaitForSeconds(currentWave.SpawnIntervalSeconds);
                    }
                }
                else
                {
                    spawnFailureCountThisWave++;
                    lastSpawnFailureReason = enemyDefinition != null
                        ? $"Wave {currentWave.WaveNumber} spawn failed for {enemyDefinition.DisplayName}. Queue item retained for retry."
                        : $"Wave {currentWave.WaveNumber} spawn failed because queued enemy definition was missing. Queue item retained for retry.";

                    if (spawnFailureCountThisWave == 1 || spawnFailureCountThisWave % 5 == 0)
                    {
                        Log(lastSpawnFailureReason);
                    }

                    float retryDelay = failedSpawnRetryDelaySeconds > 0f
                        ? failedSpawnRetryDelaySeconds
                        : currentWave.SpawnIntervalSeconds;
                    yield return new WaitForSeconds(retryDelay);
                }
            }

            state = WaveState.WaitingForClear;
            while (aliveEnemies.Count > 0)
            {
                CleanupAliveEnemies();
                PublishWaveEnemyCounts();
                yield return null;
            }

            if (currentWave.UnlockExtractionAfterWave || IsFinalWave)
            {
                state = WaveState.Completed;
                hasCompletedAllWaves = true;
                FinalWaveCompleted?.Invoke(currentWave);
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
                hasCompletedAllWaves = true;
                FinalWaveCompleted?.Invoke(currentWave);
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

        private void PublishCurrentWaveStats()
        {
            if (statsManager != null && currentWave != null)
            {
                statsManager.SetCurrentWave(currentWave.WaveNumber, waves.Count);
            }
        }

        private void PublishWaveEnemyCounts()
        {
            if (statsManager != null)
            {
                statsManager.SetWaveEnemyCounts(enemiesSpawnedThisWave, totalEnemiesToSpawnThisWave, aliveEnemies.Count);
            }
        }

        private void ConfigureLifecycleReporter(GameObject enemy)
        {
            if (enemy == null)
            {
                return;
            }

            EnemyLifecycleReporter reporter = enemy.GetComponent<EnemyLifecycleReporter>();
            if (reporter == null)
            {
                reporter = enemy.AddComponent<EnemyLifecycleReporter>();
            }

            reporter.Configure(this);
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
            failedSpawnRetryDelaySeconds = Mathf.Max(0.1f, failedSpawnRetryDelaySeconds);
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
