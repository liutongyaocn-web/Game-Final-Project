// Initially generated with Codex assistance and intended for student review/modification.
using LastStand.AI;
using LastStand.Stats;
using UnityEngine;

namespace LastStand.Pickups
{
    public class EnemyDeathDropper : MonoBehaviour
    {
        [SerializeField] private EnemyDropTable dropTable;
        [SerializeField] private bool autoFindLifecycleReporter = true;
        [SerializeField] private bool dropOnlyOnce = true;
        [SerializeField] private bool logDropEvents;
        [SerializeField] private Transform explicitDropPoint;

        private EnemyLifecycleReporter lifecycleReporter;
        private SpawnedEnemyRuntimeInfo runtimeInfo;
        private bool hasDropped;

        private void Awake()
        {
            ResolveReferences();
            SubscribeToLifecycleReporter();
        }

        private void OnDestroy()
        {
            UnsubscribeFromLifecycleReporter();
        }

        private void ResolveReferences()
        {
            if (lifecycleReporter == null && autoFindLifecycleReporter)
            {
                lifecycleReporter = GetComponent<EnemyLifecycleReporter>();
                if (lifecycleReporter == null)
                {
                    lifecycleReporter = GetComponentInChildren<EnemyLifecycleReporter>(true);
                }
            }

            if (runtimeInfo == null)
            {
                runtimeInfo = GetComponent<SpawnedEnemyRuntimeInfo>();
            }
        }

        private void SubscribeToLifecycleReporter()
        {
            if (lifecycleReporter != null)
            {
                lifecycleReporter.Defeated -= HandleEnemyDefeated;
                lifecycleReporter.Defeated += HandleEnemyDefeated;
            }
        }

        private void UnsubscribeFromLifecycleReporter()
        {
            if (lifecycleReporter != null)
            {
                lifecycleReporter.Defeated -= HandleEnemyDefeated;
            }
        }

        private void HandleEnemyDefeated(EnemyLifecycleReporter reporter, GameObject enemy, string reason)
        {
            TryDropPickup(reason);
        }

        private void TryDropPickup(string reason)
        {
            if (dropTable == null)
            {
                Log("No drop table assigned.");
                return;
            }

            if (hasDropped && dropOnlyOnce)
            {
                return;
            }

            ResolveReferences();
            int waveNumber = runtimeInfo != null ? runtimeInfo.WaveNumber : 1;
            GameObject selectedPrefab = dropTable.SelectDropPrefab(waveNumber);
            if (selectedPrefab == null)
            {
                Log($"No pickup selected for wave {waveNumber}.");
                return;
            }

            Vector3 basePosition = explicitDropPoint != null ? explicitDropPoint.position : transform.position;
            Vector3 dropPosition = dropTable.GetDropPosition(basePosition);
            Instantiate(selectedPrefab, dropPosition, Quaternion.identity);

            hasDropped = true;
            Log($"Dropped {selectedPrefab.name} for wave {waveNumber}. Reason: {reason}");
        }

        private void Log(string message)
        {
            if (logDropEvents)
            {
                Debug.Log($"[EnemyDeathDropper] {message}", this);
            }
        }
    }
}
