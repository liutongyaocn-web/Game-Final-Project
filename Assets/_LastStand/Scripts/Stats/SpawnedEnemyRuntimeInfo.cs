// Initially generated with Codex assistance and intended for student review/modification.
using LastStand.AI;
using UnityEngine;

namespace LastStand.Stats
{
    public class SpawnedEnemyRuntimeInfo : MonoBehaviour
    {
        [SerializeField] private EnemyDefinition enemyDefinition;
        [SerializeField] private int waveNumber = 1;
        [SerializeField] private int scoreValue;
        [SerializeField] private float spawnedTime;
        [SerializeField] private bool countedAsDefeated;

        public EnemyDefinition EnemyDefinition => enemyDefinition;
        public int WaveNumber => waveNumber;
        public int ScoreValue => scoreValue;
        public float SpawnedTime => spawnedTime;
        public bool CountedAsDefeated => countedAsDefeated;
        public bool HasDefinition => enemyDefinition != null;

        public void Configure(EnemyDefinition definition, int spawnedWaveNumber)
        {
            enemyDefinition = definition;
            waveNumber = Mathf.Max(1, spawnedWaveNumber);
            scoreValue = definition != null ? definition.ScoreValue : 0;
            spawnedTime = Time.time;
            countedAsDefeated = false;
        }

        public void MarkDefeated()
        {
            countedAsDefeated = true;
        }

        private void OnValidate()
        {
            waveNumber = Mathf.Max(1, waveNumber);
            scoreValue = Mathf.Max(0, scoreValue);
        }
    }
}
