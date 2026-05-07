// Initially generated with Codex assistance and intended for student review/modification.
using System.Collections.Generic;
using LastStand.AI;
using LastStand.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace LastStand.Spawning
{
    public class SpawnDirector : MonoBehaviour
    {
        [SerializeField] private SpawnPointGroup spawnPointGroup;
        [SerializeField] private Transform spawnedEnemyParent;
        [SerializeField] private GameObject player;
        [SerializeField] private int currentWaveNumber = 1;
        [SerializeField] private bool useNavMeshValidation = true;
        [SerializeField] private float navMeshSampleRadius = 3f;
        [SerializeField] private bool avoidSpawningTooCloseToPlayer = true;
        [SerializeField] private bool logSpawnEvents;

        [Header("Debug Test Only")]
        [SerializeField] private bool debugSpawnOnStart;
        [SerializeField] private EnemyDefinition debugEnemyDefinition;
        [SerializeField] private int debugSpawnWaveNumber = 1;

        private void Start()
        {
            if (debugSpawnOnStart && debugEnemyDefinition != null)
            {
                SpawnEnemy(debugEnemyDefinition, debugSpawnWaveNumber);
            }
        }

        public GameObject SpawnEnemy(EnemyDefinition enemyDefinition, int waveNumber)
        {
            LastStandSpawnPoint spawnPoint = SelectSpawnPointForEnemy(enemyDefinition, waveNumber);
            return spawnPoint != null ? SpawnEnemyAtPoint(enemyDefinition, spawnPoint, waveNumber) : null;
        }

        public GameObject SpawnEnemyAtPoint(EnemyDefinition enemyDefinition, LastStandSpawnPoint spawnPoint)
        {
            return SpawnEnemyAtPoint(enemyDefinition, spawnPoint, currentWaveNumber);
        }

        public GameObject SpawnEnemyAtPoint(EnemyDefinition enemyDefinition, LastStandSpawnPoint spawnPoint, int waveNumber)
        {
            if (enemyDefinition == null || enemyDefinition.Prefab == null || spawnPoint == null)
            {
                Log("Spawn failed because enemy definition, prefab, or spawn point was missing.");
                return null;
            }

            Vector3 spawnPosition = GetSpawnPosition(spawnPoint, out bool navMeshAdjusted);
            GameObject enemy = Instantiate(
                enemyDefinition.Prefab,
                spawnPosition,
                spawnPoint.transform.rotation,
                ResolveSpawnedEnemyParent());

            enemy.name = $"{enemyDefinition.Prefab.name}_Runtime";
            ConfigureRuntimeInfo(enemy, enemyDefinition, waveNumber);
            Log($"Spawned {enemyDefinition.DisplayName} at {spawnPoint.SpawnId}" +
                (navMeshAdjusted ? " using sampled NavMesh position." : "."));

            EnemyTargetBinder binder = enemy.GetComponent<EnemyTargetBinder>();
            if (binder != null && player != null)
            {
                binder.BindTarget(player);
            }

            return enemy;
        }

        private static void ConfigureRuntimeInfo(GameObject enemy, EnemyDefinition enemyDefinition, int waveNumber)
        {
            if (enemy == null)
            {
                return;
            }

            SpawnedEnemyRuntimeInfo runtimeInfo = enemy.GetComponent<SpawnedEnemyRuntimeInfo>();
            if (runtimeInfo == null)
            {
                runtimeInfo = enemy.AddComponent<SpawnedEnemyRuntimeInfo>();
            }

            runtimeInfo.Configure(enemyDefinition, waveNumber);
        }

        public LastStandSpawnPoint SelectSpawnPointForEnemy(EnemyDefinition enemyDefinition, int waveNumber)
        {
            if (enemyDefinition == null || spawnPointGroup == null)
            {
                Log("No enemy definition or spawn point group assigned.");
                return null;
            }

            SpawnPointRole spawnRole = GetSpawnRoleForEnemy(enemyDefinition);
            List<LastStandSpawnPoint> rolePoints = spawnPointGroup.GetEligiblePoints(spawnRole, waveNumber);
            if (rolePoints.Count == 0)
            {
                Log($"No eligible {spawnRole} spawn points for wave {waveNumber}.");
                return null;
            }

            List<LastStandSpawnPoint> filteredPoints = new();
            foreach (LastStandSpawnPoint point in rolePoints)
            {
                if (point == null)
                {
                    continue;
                }

                if (avoidSpawningTooCloseToPlayer && player != null)
                {
                    float distance = Vector3.Distance(player.transform.position, point.Position);
                    if (distance < point.MinDistanceFromPlayer)
                    {
                        continue;
                    }
                }

                if (useNavMeshValidation && !TrySampleNavMesh(point.Position, out _))
                {
                    continue;
                }

                filteredPoints.Add(point);
            }

            List<LastStandSpawnPoint> candidates = filteredPoints.Count > 0 ? filteredPoints : rolePoints;
            if (filteredPoints.Count == 0)
            {
                Log($"Falling back to unfiltered {spawnRole} spawn points for wave {waveNumber}.");
            }

            int index = Random.Range(0, candidates.Count);
            return candidates[index];
        }

        public SpawnPointRole GetSpawnRoleForEnemy(EnemyDefinition enemyDefinition)
        {
            if (enemyDefinition == null)
            {
                return SpawnPointRole.Mixed;
            }

            return enemyDefinition.Role switch
            {
                EnemyCombatRole.FistMelee => SpawnPointRole.Melee,
                EnemyCombatRole.KnifeMelee => SpawnPointRole.Melee,
                EnemyCombatRole.Ranged => SpawnPointRole.Ranged,
                _ => SpawnPointRole.Mixed
            };
        }

        private Vector3 GetSpawnPosition(LastStandSpawnPoint spawnPoint, out bool navMeshAdjusted)
        {
            if (useNavMeshValidation && TrySampleNavMesh(spawnPoint.Position, out Vector3 sampledPosition))
            {
                navMeshAdjusted = true;
                return sampledPosition;
            }

            navMeshAdjusted = false;
            return spawnPoint.Position;
        }

        private bool TrySampleNavMesh(Vector3 position, out Vector3 sampledPosition)
        {
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, navMeshSampleRadius, NavMesh.AllAreas))
            {
                sampledPosition = hit.position;
                return true;
            }

            sampledPosition = position;
            return false;
        }

        private Transform ResolveSpawnedEnemyParent()
        {
            if (spawnedEnemyParent != null)
            {
                return spawnedEnemyParent;
            }

            GameObject existing = GameObject.Find("Spawned_Enemies");
            if (existing != null)
            {
                spawnedEnemyParent = existing.transform;
                return spawnedEnemyParent;
            }

            GameObject created = new("Spawned_Enemies");
            spawnedEnemyParent = created.transform;
            return spawnedEnemyParent;
        }

        private void OnValidate()
        {
            currentWaveNumber = Mathf.Max(1, currentWaveNumber);
            debugSpawnWaveNumber = Mathf.Max(1, debugSpawnWaveNumber);
            navMeshSampleRadius = Mathf.Max(0.1f, navMeshSampleRadius);
        }

        private void Log(string message)
        {
            if (logSpawnEvents)
            {
                Debug.Log($"[SpawnDirector] {message}", this);
            }
        }
    }
}
