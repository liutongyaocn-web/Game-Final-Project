// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections.Generic;
using LastStand.AI;
using UnityEngine;

namespace LastStand.Waves
{
    [CreateAssetMenu(fileName = "WaveDefinition_New", menuName = "Last Stand/Waves/Wave Definition")]
    public class WaveDefinition : ScriptableObject
    {
        [SerializeField] private int waveNumber = 1;
        [SerializeField] private string displayName;
        [SerializeField] private string objectiveText;
        [SerializeField] private List<WaveEnemyEntry> enemies = new();
        [SerializeField] private int maxAliveAtOnce = 1;
        [SerializeField] private float spawnIntervalSeconds = 4f;
        [SerializeField] private float startDelaySeconds = 2f;
        [SerializeField] private float intermissionAfterWaveSeconds = 8f;
        [SerializeField] private bool unlockExtractionAfterWave;
        [TextArea(2, 5)]
        [SerializeField] private string difficultyNotes;
        [SerializeField] private bool validatedForRuntime;

        public int WaveNumber => waveNumber;
        public string DisplayName => displayName;
        public string ObjectiveText => objectiveText;
        public IReadOnlyList<WaveEnemyEntry> Enemies => enemies;
        public int MaxAliveAtOnce => maxAliveAtOnce;
        public float SpawnIntervalSeconds => spawnIntervalSeconds;
        public float StartDelaySeconds => startDelaySeconds;
        public float IntermissionAfterWaveSeconds => intermissionAfterWaveSeconds;
        public bool UnlockExtractionAfterWave => unlockExtractionAfterWave;
        public string DifficultyNotes => difficultyNotes;
        public bool ValidatedForRuntime => validatedForRuntime;

        public int TotalEnemyCount
        {
            get
            {
                int total = 0;
                foreach (WaveEnemyEntry entry in enemies)
                {
                    if (entry != null)
                    {
                        total += entry.Count;
                    }
                }

                return total;
            }
        }

        public bool HasRangedEnemy => HasRole(EnemyCombatRole.Ranged);
        public bool HasKnifeMeleeEnemy => HasRole(EnemyCombatRole.KnifeMelee);
        public bool HasFistMeleeEnemy => HasRole(EnemyCombatRole.FistMelee);

        private void OnValidate()
        {
            waveNumber = Mathf.Max(1, waveNumber);
            maxAliveAtOnce = Mathf.Max(1, maxAliveAtOnce);
            spawnIntervalSeconds = Mathf.Max(0.25f, spawnIntervalSeconds);
            startDelaySeconds = Mathf.Max(0f, startDelaySeconds);
            intermissionAfterWaveSeconds = Mathf.Max(0f, intermissionAfterWaveSeconds);

            foreach (WaveEnemyEntry entry in enemies)
            {
                entry?.Validate();
            }
        }

        private bool HasRole(EnemyCombatRole role)
        {
            foreach (WaveEnemyEntry entry in enemies)
            {
                EnemyDefinition definition = entry?.EnemyDefinition;
                if (definition != null && definition.Role == role && entry.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
