// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;

namespace LastStand.Stats
{
    public class LastStandStatsManager : MonoBehaviour
    {
        [SerializeField] private bool resetOnStart = true;
        [SerializeField] private bool trackSurvivalTime = true;
        [SerializeField] private bool logStatEvents;
        [SerializeField] private int fallbackScoreValue;

        private int currentWaveNumber;
        private int totalWaves;
        private int kills;
        private int score;
        private int enemiesAlive;
        private int enemiesSpawnedThisWave;
        private int enemiesTotalThisWave;
        private float survivalTimeSeconds;
        private bool runActive;

        public int CurrentWaveNumber => currentWaveNumber;
        public int TotalWaves => totalWaves;
        public int Kills => kills;
        public int Score => score;
        public int EnemiesAlive => enemiesAlive;
        public int EnemiesSpawnedThisWave => enemiesSpawnedThisWave;
        public int EnemiesTotalThisWave => enemiesTotalThisWave;
        public float SurvivalTimeSeconds => survivalTimeSeconds;
        public string FormattedSurvivalTime => FormatTime(survivalTimeSeconds);

        private void Start()
        {
            if (resetOnStart && !runActive)
            {
                ResetStats();
            }
        }

        private void Update()
        {
            if (runActive && trackSurvivalTime)
            {
                survivalTimeSeconds += Time.deltaTime;
            }
        }

        public void ResetStats()
        {
            currentWaveNumber = 0;
            totalWaves = 0;
            kills = 0;
            score = 0;
            enemiesAlive = 0;
            enemiesSpawnedThisWave = 0;
            enemiesTotalThisWave = 0;
            survivalTimeSeconds = 0f;
            runActive = false;
        }

        public void BeginRun(int totalWaveCount)
        {
            ResetStats();
            totalWaves = Mathf.Max(0, totalWaveCount);
            runActive = true;
            Log($"Run started with {totalWaves} wave(s).");
        }

        public void EndRun()
        {
            runActive = false;
            Log("Run ended.");
        }

        public void SetCurrentWave(int waveNumber, int totalWaveCount)
        {
            currentWaveNumber = Mathf.Max(0, waveNumber);
            totalWaves = Mathf.Max(0, totalWaveCount);
        }

        public void SetWaveEnemyCounts(int spawned, int total, int alive)
        {
            enemiesSpawnedThisWave = Mathf.Max(0, spawned);
            enemiesTotalThisWave = Mathf.Max(0, total);
            enemiesAlive = Mathf.Max(0, alive);
        }

        public void RegisterEnemyDefeated(GameObject enemy)
        {
            if (enemy == null)
            {
                return;
            }

            RegisterEnemyDefeated(enemy.GetComponent<SpawnedEnemyRuntimeInfo>());
        }

        public void RegisterEnemyDefeated(SpawnedEnemyRuntimeInfo info)
        {
            if (info != null)
            {
                if (info.CountedAsDefeated)
                {
                    return;
                }

                info.MarkDefeated();
                score += Mathf.Max(0, info.HasDefinition ? info.ScoreValue : fallbackScoreValue);
            }
            else
            {
                score += Mathf.Max(0, fallbackScoreValue);
            }

            kills++;
            Log($"Enemy defeated. Kills: {kills}, Score: {score}.");
        }

        private static string FormatTime(float seconds)
        {
            int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(seconds));
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;
            return $"{minutes:00}:{remainingSeconds:00}";
        }

        private void OnValidate()
        {
            fallbackScoreValue = Mathf.Max(0, fallbackScoreValue);
        }

        private void Log(string message)
        {
            if (logStatEvents)
            {
                Debug.Log($"[LastStandStatsManager] {message}", this);
            }
        }
    }
}
