// Initially generated with Codex assistance and intended for student review/modification.
using LastStand.Stats;
using LastStand.Waves;
using UnityEngine;

namespace LastStand.GameFlow
{
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private LastStandStatsManager statsManager;
        [SerializeField] private ExtractionObjective extractionObjective;
        [SerializeField] private bool autoFindReferences = true;
        [SerializeField] private bool logGameFlowEvents;
        [Header("Debug Validation")]
        [SerializeField] private bool debugUnlockExtractionOnStart;
        [SerializeField] private bool debugCompleteExtractionOnStart;

        private GameFlowState currentState = GameFlowState.Idle;
        private bool extractionUnlocked;
        private bool victoryReached;
        private float victoryTimeSeconds;

        public GameFlowState CurrentState => currentState;
        public bool ExtractionUnlocked => extractionUnlocked;
        public bool VictoryReached => victoryReached;
        public string CurrentObjectiveText => GetObjectiveText();
        public float VictoryTimeSeconds => victoryTimeSeconds;

        private void Awake()
        {
            ResolveReferences();
            SubscribeToWaveManager();
        }

        private void OnEnable()
        {
            SubscribeToWaveManager();
        }

        private void Start()
        {
            ResolveReferences();
            if (extractionObjective != null)
            {
                extractionObjective.ResetObjective();
            }

            if (debugCompleteExtractionOnStart)
            {
                UnlockExtraction();
                if (extractionObjective != null)
                {
                    extractionObjective.CompleteObjective(GameObject.Find("Player_JUTPS"));
                }
                else
                {
                    CompleteExtraction();
                }
            }
            else if (debugUnlockExtractionOnStart)
            {
                UnlockExtraction();
            }
        }

        private void Update()
        {
            if (currentState == GameFlowState.Idle
                && waveManager != null
                && waveManager.State != WaveState.Idle
                && waveManager.State != WaveState.Completed
                && waveManager.State != WaveState.Failed)
            {
                BeginRun();
            }

            if (!extractionUnlocked && waveManager != null && waveManager.HasCompletedAllWaves)
            {
                UnlockExtraction();
            }
        }

        private void OnDisable()
        {
            if (waveManager != null)
            {
                waveManager.FinalWaveCompleted -= HandleFinalWaveCompleted;
            }
        }

        public void BeginRun()
        {
            if (currentState is GameFlowState.Victory or GameFlowState.Failed)
            {
                return;
            }

            currentState = GameFlowState.Running;
            Log("Run state set to Running.");
        }

        public void UnlockExtraction()
        {
            if (currentState == GameFlowState.Failed || victoryReached || extractionUnlocked)
            {
                return;
            }

            extractionUnlocked = true;
            currentState = GameFlowState.ExtractionUnlocked;
            if (extractionObjective != null)
            {
                extractionObjective.UnlockExtraction();
            }

            Log("Extraction unlocked.");
        }

        public void CompleteExtraction()
        {
            if (currentState == GameFlowState.Failed || !extractionUnlocked || victoryReached)
            {
                return;
            }

            currentState = GameFlowState.Victory;
            victoryReached = true;
            victoryTimeSeconds = statsManager != null ? statsManager.SurvivalTimeSeconds : Time.timeSinceLevelLoad;

            if (statsManager != null)
            {
                statsManager.EndRun();
            }

            Log($"Extraction completed at {victoryTimeSeconds:0.0}s.");
        }

        public void FailRun()
        {
            if (victoryReached)
            {
                return;
            }

            currentState = GameFlowState.Failed;
            if (statsManager != null)
            {
                statsManager.EndRun();
            }

            Log("Run failed.");
        }

        public void ResetFlowForNewRun()
        {
            currentState = GameFlowState.Idle;
            extractionUnlocked = false;
            victoryReached = false;
            victoryTimeSeconds = 0f;

            if (extractionObjective != null)
            {
                extractionObjective.ResetObjective();
            }
        }

        [ContextMenu("Debug Unlock Extraction")]
        private void DebugUnlockExtraction()
        {
            UnlockExtraction();
        }

        [ContextMenu("Debug Complete Extraction")]
        private void DebugCompleteExtraction()
        {
            UnlockExtraction();
            if (extractionObjective != null)
            {
                extractionObjective.CompleteObjective(GameObject.Find("Player_JUTPS"));
            }
            else
            {
                CompleteExtraction();
            }
        }

        private void HandleFinalWaveCompleted(WaveDefinition completedWave)
        {
            UnlockExtraction();
        }

        private string GetObjectiveText()
        {
            return currentState switch
            {
                GameFlowState.Running => "Survive the infected attack",
                GameFlowState.ExtractionUnlocked => "Reach extraction",
                GameFlowState.Victory => "Extraction complete",
                GameFlowState.Failed => "You died",
                _ => "Prepare for the first wave"
            };
        }

        private void ResolveReferences()
        {
            if (!autoFindReferences)
            {
                return;
            }

            if (waveManager == null)
            {
                waveManager = FindFirstObjectByType<WaveManager>();
            }

            if (statsManager == null)
            {
                statsManager = FindFirstObjectByType<LastStandStatsManager>();
            }

            if (extractionObjective == null)
            {
                extractionObjective = FindFirstObjectByType<ExtractionObjective>();
            }
        }

        private void SubscribeToWaveManager()
        {
            ResolveReferences();
            if (waveManager == null)
            {
                return;
            }

            waveManager.FinalWaveCompleted -= HandleFinalWaveCompleted;
            waveManager.FinalWaveCompleted += HandleFinalWaveCompleted;
        }

        private void Log(string message)
        {
            if (logGameFlowEvents)
            {
                Debug.Log($"[GameFlowManager] {message}", this);
            }
        }
    }
}
