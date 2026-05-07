// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;

namespace LastStand.AI
{
    [CreateAssetMenu(fileName = "EnemyDefinition_New", menuName = "Last Stand/AI/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
    {
        [SerializeField] private string enemyId;
        [SerializeField] private string displayName;
        [SerializeField] private EnemyCombatRole role;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int scoreValue = 100;
        [SerializeField] private int minWave = 1;
        [SerializeField] private float spawnWeight = 1f;
        [SerializeField] private int recommendedMaxAlive = 1;
        [TextArea(2, 5)]
        [SerializeField] private string description;
        [SerializeField] private bool validatedInScene;
        [TextArea(2, 5)]
        [SerializeField] private string validationNotes;

        public string EnemyId => enemyId;
        public string DisplayName => displayName;
        public EnemyCombatRole Role => role;
        public GameObject Prefab => prefab;
        public int ScoreValue => scoreValue;
        public int MinWave => minWave;
        public float SpawnWeight => spawnWeight;
        public int RecommendedMaxAlive => recommendedMaxAlive;
        public string Description => description;
        public bool ValidatedInScene => validatedInScene;
        public string ValidationNotes => validationNotes;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(enemyId))
            {
                enemyId = name;
            }

            scoreValue = Mathf.Max(0, scoreValue);
            minWave = Mathf.Max(1, minWave);
            spawnWeight = Mathf.Max(0f, spawnWeight);
            recommendedMaxAlive = Mathf.Max(1, recommendedMaxAlive);
        }
    }
}
