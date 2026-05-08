// Initially generated with Codex assistance and intended for student review/modification.
using System;
using UnityEngine;

namespace LastStand.Pickups
{
    [Serializable]
    public class DropItemEntry
    {
        [SerializeField] private GameObject pickupPrefab;
        [Range(0f, 1f)]
        [SerializeField] private float dropChance = 0.25f;
        [SerializeField] private int minWave = 1;
        [SerializeField] private bool allowMultiple;
        [TextArea(2, 4)]
        [SerializeField] private string notes;

        public GameObject PickupPrefab => pickupPrefab;
        public float DropChance => dropChance;
        public int MinWave => minWave;
        public bool AllowMultiple => allowMultiple;
        public string Notes => notes;

        public bool IsEligible(int waveNumber)
        {
            return pickupPrefab != null && waveNumber >= minWave;
        }

        public void Validate()
        {
            dropChance = Mathf.Clamp01(dropChance);
            minWave = Mathf.Max(1, minWave);
        }
    }
}
