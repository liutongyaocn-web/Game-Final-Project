// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;

namespace LastStand.Spawning
{
    public class LastStandSpawnPoint : MonoBehaviour
    {
        [SerializeField] private string spawnId;
        [SerializeField] private SpawnPointRole role;
        [SerializeField] private float minDistanceFromPlayer = 12f;
        [SerializeField] private float preferredDistanceFromPlayer = 25f;
        [SerializeField] private bool requireLineOfSightCheck;
        [SerializeField] private bool allowDuringEarlyWaves = true;
        [SerializeField] private int minimumWave = 1;
        [TextArea(1, 3)]
        [SerializeField] private string notes;

        public string SpawnId => spawnId;
        public SpawnPointRole Role => role;
        public float MinDistanceFromPlayer => minDistanceFromPlayer;
        public float PreferredDistanceFromPlayer => preferredDistanceFromPlayer;
        public bool RequireLineOfSightCheck => requireLineOfSightCheck;
        public bool AllowDuringEarlyWaves => allowDuringEarlyWaves;
        public int MinimumWave => minimumWave;
        public string Notes => notes;
        public Vector3 Position => transform.position;

        public bool IsEligibleForWave(int waveNumber)
        {
            return waveNumber >= minimumWave;
        }

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(spawnId))
            {
                spawnId = gameObject.name;
            }

            minDistanceFromPlayer = Mathf.Max(0f, minDistanceFromPlayer);
            preferredDistanceFromPlayer = Mathf.Max(minDistanceFromPlayer, preferredDistanceFromPlayer);
            minimumWave = Mathf.Max(1, minimumWave);
        }
    }
}
