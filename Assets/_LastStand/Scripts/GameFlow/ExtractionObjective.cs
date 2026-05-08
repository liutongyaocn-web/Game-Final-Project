// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;

namespace LastStand.GameFlow
{
    public class ExtractionObjective : MonoBehaviour
    {
        [SerializeField] private GameFlowManager gameFlowManager;
        [SerializeField] private bool unlocked;
        [SerializeField] private bool completed;
        [SerializeField] private string playerObjectName = "Player_JUTPS";
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private bool logObjectiveEvents;
        [SerializeField] private GameObject visualRoot;
        [SerializeField] private Collider triggerCollider;

        public bool IsUnlocked => unlocked;
        public bool IsCompleted => completed;

        private void Awake()
        {
            ResolveReferences();
            ConfigureTrigger();
        }

        private void OnValidate()
        {
            ConfigureTrigger();
        }

        public void UnlockExtraction()
        {
            if (completed)
            {
                return;
            }

            unlocked = true;
            Log("Extraction objective unlocked.");
        }

        public void ResetObjective()
        {
            unlocked = false;
            completed = false;
        }

        public void CompleteObjective(GameObject player)
        {
            if (!unlocked || completed)
            {
                return;
            }

            completed = true;
            Log($"Extraction completed by {player?.name ?? "unknown"}.");

            if (gameFlowManager != null)
            {
                gameFlowManager.CompleteExtraction();
            }
        }

        [ContextMenu("Debug Unlock Extraction")]
        public void DebugUnlockExtraction()
        {
            UnlockExtraction();
        }

        [ContextMenu("Debug Complete Extraction")]
        public void DebugCompleteExtraction()
        {
            UnlockExtraction();
            CompleteObjective(GameObject.Find(playerObjectName));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!unlocked || completed)
            {
                return;
            }

            GameObject candidate = other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : other.gameObject;
            if (!IsPlayer(candidate) && !IsPlayer(other.gameObject))
            {
                return;
            }

            CompleteObjective(candidate);
        }

        private bool IsPlayer(GameObject candidate)
        {
            if (candidate == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(playerObjectName) && candidate.name == playerObjectName)
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(playerTag) && HasTag(candidate, playerTag))
            {
                return true;
            }

            Transform parent = candidate.transform.parent;
            while (parent != null)
            {
                if (!string.IsNullOrWhiteSpace(playerObjectName) && parent.name == playerObjectName)
                {
                    return true;
                }

                parent = parent.parent;
            }

            return false;
        }

        private static bool HasTag(GameObject candidate, string tagName)
        {
            try
            {
                return candidate.CompareTag(tagName);
            }
            catch (UnityException)
            {
                return false;
            }
        }

        private void ResolveReferences()
        {
            if (gameFlowManager == null)
            {
                gameFlowManager = FindFirstObjectByType<GameFlowManager>();
            }

            if (triggerCollider == null)
            {
                triggerCollider = GetComponent<Collider>();
            }
        }

        private void ConfigureTrigger()
        {
            if (triggerCollider != null)
            {
                triggerCollider.isTrigger = true;
            }
        }

        private void Log(string message)
        {
            if (logObjectiveEvents)
            {
                Debug.Log($"[ExtractionObjective] {message}", this);
            }
        }
    }
}
