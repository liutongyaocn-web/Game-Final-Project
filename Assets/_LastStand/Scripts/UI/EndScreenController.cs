// Initially generated with Codex assistance and intended for student review/modification.
using LastStand.GameFlow;
using LastStand.Stats;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace LastStand.UI
{
    public class EndScreenController : MonoBehaviour
    {
        [SerializeField] private GameFlowManager gameFlowManager;
        [SerializeField] private LastStandStatsManager statsManager;
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private Text titleText;
        [SerializeField] private Text subtitleText;
        [SerializeField] private Text statsText;
        [SerializeField] private Button restartButton;
        [SerializeField] private bool autoFindReferences = true;
        [SerializeField] private KeyCode restartKey = KeyCode.R;
        [SerializeField] private bool unlockCursorOnEnd = true;
        [SerializeField] private bool logEndScreenEvents;

        private bool isPanelVisible;
        private GameFlowState lastDisplayedState = GameFlowState.Idle;

        private void Awake()
        {
            ResolveReferences();
            HidePanel();
            HookRestartButton();
        }

        private void OnEnable()
        {
            HookRestartButton();
        }

        private void OnDisable()
        {
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(RestartScene);
            }
        }

        private void Update()
        {
            ResolveReferences();

            GameFlowState state = gameFlowManager != null ? gameFlowManager.CurrentState : GameFlowState.Idle;
            if (state == GameFlowState.Failed || state == GameFlowState.Victory)
            {
                ShowPanel(state);

                if (WasRestartPressed())
                {
                    RestartScene();
                }
            }
            else if (isPanelVisible)
            {
                HidePanel();
            }
        }

        public void RestartScene()
        {
            Time.timeScale = 1f;
            Scene activeScene = SceneManager.GetActiveScene();
            Log($"Restarting scene {activeScene.name}.");
            SceneManager.LoadScene(activeScene.name);
        }

        private void ShowPanel(GameFlowState state)
        {
            if (panelRoot != null && !panelRoot.activeSelf)
            {
                panelRoot.SetActive(true);
            }

            isPanelVisible = true;
            if (unlockCursorOnEnd)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (lastDisplayedState != state)
            {
                Log($"Showing end screen for {state}.");
            }

            lastDisplayedState = state;
            SetText(titleText, state == GameFlowState.Victory ? "Extraction Complete" : "You Died");
            SetText(subtitleText, state == GameFlowState.Victory ? "You survived the final wave." : "The infected overwhelmed you.");
            SetText(statsText, BuildStatsText());
        }

        private void HidePanel()
        {
            if (panelRoot != null)
            {
                panelRoot.SetActive(false);
            }

            isPanelVisible = false;
            lastDisplayedState = GameFlowState.Idle;
        }

        private string BuildStatsText()
        {
            if (statsManager == null)
            {
                return "Kills: -\nScore: -\nTime: --:--\nWave: - / -";
            }

            return $"Kills: {statsManager.Kills}\n"
                + $"Score: {statsManager.Score}\n"
                + $"Time: {statsManager.FormattedSurvivalTime}\n"
                + $"Wave: {statsManager.CurrentWaveNumber} / {statsManager.TotalWaves}";
        }

        private void HookRestartButton()
        {
            if (restartButton == null)
            {
                return;
            }

            restartButton.onClick.RemoveListener(RestartScene);
            restartButton.onClick.AddListener(RestartScene);
        }

        private bool WasRestartPressed()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                return true;
            }
#endif

#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKeyDown(restartKey);
#else
            return false;
#endif
        }

        private void ResolveReferences()
        {
            if (!autoFindReferences)
            {
                return;
            }

            if (gameFlowManager == null)
            {
                gameFlowManager = FindFirstObjectByType<GameFlowManager>();
            }

            if (statsManager == null)
            {
                statsManager = FindFirstObjectByType<LastStandStatsManager>();
            }
        }

        private static void SetText(Text target, string value)
        {
            if (target != null)
            {
                target.text = value;
            }
        }

        private void Log(string message)
        {
            if (logEndScreenEvents)
            {
                Debug.Log($"[EndScreenController] {message}", this);
            }
        }
    }
}
