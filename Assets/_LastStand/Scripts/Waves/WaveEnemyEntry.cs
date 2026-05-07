// Initially generated with Codex assistance and intended for student review/modification.
using System;
using LastStand.AI;
using UnityEngine;

namespace LastStand.Waves
{
    [Serializable]
    public class WaveEnemyEntry
    {
        [SerializeField] private EnemyDefinition enemyDefinition;
        [SerializeField] private int count = 1;
        [SerializeField] private float spawnWeight = 1f;
        [SerializeField] private int minSpawnGroupSize = 1;
        [SerializeField] private int maxSpawnGroupSize = 1;
        [TextArea(1, 3)]
        [SerializeField] private string notes;

        public EnemyDefinition EnemyDefinition => enemyDefinition;
        public int Count => count;
        public float SpawnWeight => spawnWeight;
        public int MinSpawnGroupSize => minSpawnGroupSize;
        public int MaxSpawnGroupSize => maxSpawnGroupSize;
        public string Notes => notes;

        public void Validate()
        {
            count = Mathf.Max(0, count);
            spawnWeight = Mathf.Max(0f, spawnWeight);
            minSpawnGroupSize = Mathf.Max(1, minSpawnGroupSize);
            maxSpawnGroupSize = Mathf.Max(minSpawnGroupSize, maxSpawnGroupSize);
        }
    }
}
