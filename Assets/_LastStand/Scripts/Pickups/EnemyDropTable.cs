// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections.Generic;
using UnityEngine;

namespace LastStand.Pickups
{
    [CreateAssetMenu(fileName = "EnemyDropTable_New", menuName = "Last Stand/Pickups/Enemy Drop Table")]
    public class EnemyDropTable : ScriptableObject
    {
        [SerializeField] private string tableId;
        [SerializeField] private List<DropItemEntry> drops = new();
        [SerializeField] private bool dropAtMostOneItem = true;
        [SerializeField] private float verticalOffset = 0.25f;
        [SerializeField] private float scatterRadius = 0.75f;
        [SerializeField] private bool useWaveRestrictions = true;
        [TextArea(2, 5)]
        [SerializeField] private string notes;

        public IReadOnlyList<DropItemEntry> Drops => drops;
        public bool DropAtMostOneItem => dropAtMostOneItem;
        public float VerticalOffset => verticalOffset;
        public float ScatterRadius => scatterRadius;
        public bool UseWaveRestrictions => useWaveRestrictions;
        public string Notes => notes;

        public List<DropItemEntry> GetEligibleDrops(int waveNumber)
        {
            int effectiveWave = Mathf.Max(1, waveNumber);
            List<DropItemEntry> eligibleDrops = new();

            foreach (DropItemEntry entry in drops)
            {
                if (entry == null || entry.PickupPrefab == null)
                {
                    continue;
                }

                if (!useWaveRestrictions || entry.IsEligible(effectiveWave))
                {
                    eligibleDrops.Add(entry);
                }
            }

            return eligibleDrops;
        }

        public GameObject SelectDropPrefab(int waveNumber)
        {
            List<DropItemEntry> eligibleDrops = GetEligibleDrops(waveNumber);
            if (eligibleDrops.Count == 0)
            {
                return null;
            }

            List<GameObject> successfulRolls = new();
            foreach (DropItemEntry entry in eligibleDrops)
            {
                if (Random.value <= entry.DropChance)
                {
                    successfulRolls.Add(entry.PickupPrefab);

                    if (dropAtMostOneItem)
                    {
                        break;
                    }
                }
            }

            if (successfulRolls.Count == 0)
            {
                return null;
            }

            return successfulRolls[Random.Range(0, successfulRolls.Count)];
        }

        public Vector3 GetDropPosition(Vector3 basePosition)
        {
            Vector2 scatter = Random.insideUnitCircle * scatterRadius;
            return basePosition + new Vector3(scatter.x, verticalOffset, scatter.y);
        }

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(tableId))
            {
                tableId = name;
            }

            verticalOffset = Mathf.Max(0f, verticalOffset);
            scatterRadius = Mathf.Max(0f, scatterRadius);

            foreach (DropItemEntry entry in drops)
            {
                entry?.Validate();
            }
        }
    }
}
