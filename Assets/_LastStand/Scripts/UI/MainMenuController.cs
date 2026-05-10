// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LastStand.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string gameSceneName = "LS_Arena_01";
        [SerializeField] private GameObject controlsPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private Button controlsButton;
        [SerializeField] private Button closeControlsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private bool resetCursorOnStart = true;
        [SerializeField] private bool resetTimeScaleOnStart = true;
        [SerializeField] private bool clearSelectedUiOnStart = true;
        [SerializeField] private bool logMenuEvents;

        private void Awake()
        {
            ResetMenuState();
            HookButtons();
            SetControlsVisible(false);
        }

        private void OnEnable()
        {
            HookButtons();
        }

        private void OnDisable()
        {
            if (startButton != null)
            {
                startButton.onClick.RemoveListener(StartGame);
            }

            if (controlsButton != null)
            {
                controlsButton.onClick.RemoveListener(ToggleControls);
            }

            if (closeControlsButton != null)
            {
                closeControlsButton.onClick.RemoveListener(HideControls);
            }

            if (quitButton != null)
            {
                quitButton.onClick.RemoveListener(QuitGame);
            }
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            Log($"Loading scene {gameSceneName}.");
            SceneManager.LoadScene(gameSceneName);
        }

        public void ToggleControls()
        {
            SetControlsVisible(controlsPanel == null || !controlsPanel.activeSelf);
        }

        public void HideControls()
        {
            SetControlsVisible(false);
        }

        public void QuitGame()
        {
            Log("Quit requested.");
#if UNITY_EDITOR
            Debug.Log("[MainMenuController] Quit button pressed in Editor; Application.Quit is skipped.", this);
#else
            Application.Quit();
#endif
        }

        private void HookButtons()
        {
            if (startButton != null)
            {
                startButton.onClick.RemoveListener(StartGame);
                startButton.onClick.AddListener(StartGame);
            }

            if (controlsButton != null)
            {
                controlsButton.onClick.RemoveListener(ToggleControls);
                controlsButton.onClick.AddListener(ToggleControls);
            }

            if (closeControlsButton != null)
            {
                closeControlsButton.onClick.RemoveListener(HideControls);
                closeControlsButton.onClick.AddListener(HideControls);
            }

            if (quitButton != null)
            {
                quitButton.onClick.RemoveListener(QuitGame);
                quitButton.onClick.AddListener(QuitGame);
            }
        }

        private void ResetMenuState()
        {
            if (resetTimeScaleOnStart)
            {
                Time.timeScale = 1f;
            }

            if (resetCursorOnStart)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (clearSelectedUiOnStart && EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void SetControlsVisible(bool visible)
        {
            if (controlsPanel != null)
            {
                controlsPanel.SetActive(visible);
            }
        }

        private void Log(string message)
        {
            if (logMenuEvents)
            {
                Debug.Log($"[MainMenuController] {message}", this);
            }
        }
    }
}
