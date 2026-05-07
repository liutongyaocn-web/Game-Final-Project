// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections.Generic;
using UnityEngine;

namespace LastStand.Spawning
{
    public class SpawnPointGroup : MonoBehaviour
    {
        [SerializeField] private string groupId = "LS_Arena_01_SpawnPoints";
        [SerializeField] private List<LastStandSpawnPoint> spawnPoints = new();
        [SerializeField] private bool autoCollectChildren = true;

        public string GroupId => groupId;
        public IReadOnlyList<LastStandSpawnPoint> AllPoints => spawnPoints;

        public List<LastStandSpawnPoint> GetEligiblePoints(SpawnPointRole role, int waveNumber)
        {
            List<LastStandSpawnPoint> results = new();
            foreach (LastStandSpawnPoint point in spawnPoints)
            {
                if (point == null || !point.IsEligibleForWave(waveNumber) || !RoleMatches(point.Role, role))
                {
                    continue;
                }

                results.Add(point);
            }

            return results;
        }

        [ContextMenu("Collect Child Spawn Points")]
        public void CollectChildSpawnPoints()
        {
            spawnPoints.RemoveAll(point => point == null);

            foreach (LastStandSpawnPoint point in GetComponentsInChildren<LastStandSpawnPoint>(true))
            {
                if (!spawnPoints.Contains(point))
                {
                    spawnPoints.Add(point);
                }
            }
        }

        private void OnValidate()
        {
            spawnPoints.RemoveAll(point => point == null);

            if (autoCollectChildren)
            {
                CollectChildSpawnPoints();
            }
        }

        private static bool RoleMatches(SpawnPointRole pointRole, SpawnPointRole requestedRole)
        {
            return pointRole == SpawnPointRole.Mixed
                   || requestedRole == SpawnPointRole.Mixed
                   || pointRole == requestedRole;
        }
    }
}
