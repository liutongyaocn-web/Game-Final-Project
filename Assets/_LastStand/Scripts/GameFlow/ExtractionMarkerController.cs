// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;
using UnityEngine.UI;

namespace LastStand.GameFlow
{
    public class ExtractionMarkerController : MonoBehaviour
    {
        [SerializeField] private ExtractionObjective extractionObjective;
        [SerializeField] private GameFlowManager gameFlowManager;
        [SerializeField] private Transform player;
        [SerializeField] private GameObject markerRoot;
        [SerializeField] private Text worldLabelText;
        [SerializeField] private TextMesh worldLabelMesh;
        [SerializeField] private Text hudDistanceText;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private bool autoFindReferences = true;
        [SerializeField] private bool showOnlyWhenUnlocked = true;
        [SerializeField] private bool showDistance = true;
        [SerializeField] private string lockedText = "";
        [SerializeField] private string unlockedText = "EXTRACTION";
        [SerializeField] private string completedText = "EXTRACTION COMPLETE";
        [SerializeField] private bool hideWhenCompleted = true;
        [SerializeField] private bool faceLabelToCamera = true;
        [SerializeField] private bool logMarkerEvents;

        private bool lastMarkerActive;

        private void Start()
        {
            ResolveReferences();
            RefreshMarker(true);
        }

        private void Update()
        {
            RefreshMarker(false);
            FaceLabelToCamera();
        }

        private void RefreshMarker(bool forceLog)
        {
            bool isUnlocked = IsUnlocked();
            bool isCompleted = IsCompleted();
            bool shouldShow = showOnlyWhenUnlocked ? isUnlocked && !isCompleted : !isCompleted;

            if (isCompleted && !hideWhenCompleted)
            {
                shouldShow = true;
            }

            if (markerRoot != null && markerRoot.activeSelf != shouldShow)
            {
                markerRoot.SetActive(shouldShow);
            }

            SetLabelText(isCompleted, isUnlocked);
            SetHudDistance(shouldShow && isUnlocked && !isCompleted);

            if ((forceLog || shouldShow != lastMarkerActive) && logMarkerEvents)
            {
                Debug.Log($"[ExtractionMarkerController] Marker visible: {shouldShow}", this);
            }

            lastMarkerActive = shouldShow;
        }

        private void SetLabelText(bool isCompleted, bool isUnlocked)
        {
            if (worldLabelText == null && worldLabelMesh == null)
            {
                return;
            }

            string label = isCompleted ? completedText : isUnlocked ? unlockedText : lockedText;
            string displayText = showDistance && isUnlocked && !isCompleted ? FormatWithDistance(label) : label;

            if (worldLabelText != null)
            {
                worldLabelText.text = displayText;
            }

            if (worldLabelMesh != null)
            {
                worldLabelMesh.text = displayText;
            }
        }

        private void SetHudDistance(bool shouldShow)
        {
            if (hudDistanceText == null)
            {
                return;
            }

            hudDistanceText.gameObject.SetActive(shouldShow && showDistance);
            hudDistanceText.text = shouldShow && showDistance ? FormatWithDistance("Extraction") : string.Empty;
        }

        private string FormatWithDistance(string label)
        {
            if (player == null)
            {
                return label;
            }

            float distance = Vector3.Distance(player.position, transform.position);
            return $"{label}: {distance:0}m";
        }

        private bool IsUnlocked()
        {
            return (extractionObjective != null && extractionObjective.IsUnlocked)
                || (gameFlowManager != null && gameFlowManager.ExtractionUnlocked);
        }

        private bool IsCompleted()
        {
            return (extractionObjective != null && extractionObjective.IsCompleted)
                || (gameFlowManager != null && gameFlowManager.VictoryReached);
        }

        private void FaceLabelToCamera()
        {
            if (!faceLabelToCamera || mainCamera == null)
            {
                return;
            }

            FaceTransformToCamera(worldLabelText != null ? worldLabelText.transform : null, mainCamera);
            FaceTransformToCamera(worldLabelMesh != null ? worldLabelMesh.transform : null, mainCamera);
        }

        private static void FaceTransformToCamera(Transform labelTransform, Camera targetCamera)
        {
            if (labelTransform == null || targetCamera == null)
            {
                return;
            }

            Vector3 direction = labelTransform.position - targetCamera.transform.position;
            if (direction.sqrMagnitude > 0.01f)
            {
                labelTransform.rotation = Quaternion.LookRotation(direction);
            }
        }

        private void ResolveReferences()
        {
            if (!autoFindReferences)
            {
                return;
            }

            if (extractionObjective == null)
            {
                extractionObjective = FindFirstObjectByType<ExtractionObjective>(FindObjectsInactive.Include);
            }

            if (gameFlowManager == null)
            {
                gameFlowManager = FindFirstObjectByType<GameFlowManager>(FindObjectsInactive.Include);
            }

            if (player == null)
            {
                GameObject playerObject = GameObject.Find("Player_JUTPS");
                if (playerObject != null)
                {
                    player = playerObject.transform;
                }
            }

            if (markerRoot == null)
            {
                markerRoot = gameObject;
            }

            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        private void OnValidate()
        {
            if (markerRoot == null)
            {
                markerRoot = gameObject;
            }
        }
    }
}
