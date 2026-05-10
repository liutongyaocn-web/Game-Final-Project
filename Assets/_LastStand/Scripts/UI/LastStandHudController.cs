// Initially generated with Codex assistance and intended for student review/modification.
using LastStand.Stats;
using LastStand.Waves;
using LastStand.GameFlow;
using UnityEngine;
using UnityEngine.UI;

namespace LastStand.UI
{
    public class LastStandHudController : MonoBehaviour
    {
        [SerializeField] private LastStandStatsManager statsManager;
        [SerializeField] private FpsCounter fpsCounter;
        [SerializeField] private PlayerHealthReader healthReader;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private GameFlowManager gameFlowManager;
        [SerializeField] private bool autoFindReferences = true;
        [SerializeField] private bool showDebugFallbackText = true;
        [SerializeField] private float updateInterval = 0.1f;

        [Header("HUD Text")]
        [SerializeField] private Text waveText;
        [SerializeField] private Text enemiesText;
        [SerializeField] private Text killsText;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text timeText;
        [SerializeField] private Text fpsText;
        [SerializeField] private Text healthText;
        [SerializeField] private Text objectiveText;

        private float nextUpdateTime;

        private void Awake()
        {
            ResolveReferences();
        }

        private void Update()
        {
            if (Time.unscaledTime < nextUpdateTime)
            {
                return;
            }

            nextUpdateTime = Time.unscaledTime + updateInterval;
            ResolveReferences();
            RefreshHud();
        }

        private void ResolveReferences()
        {
            if (!autoFindReferences)
            {
                return;
            }

            if (statsManager == null)
            {
                statsManager = FindFirstObjectByType<LastStandStatsManager>();
            }

            if (waveManager == null)
            {
                waveManager = FindFirstObjectByType<WaveManager>();
            }

            if (gameFlowManager == null)
            {
                gameFlowManager = FindFirstObjectByType<GameFlowManager>();
            }

            if (fpsCounter == null)
            {
                fpsCounter = GetComponent<FpsCounter>();
            }

            if (healthReader == null)
            {
                healthReader = GetComponent<PlayerHealthReader>();
            }
        }

        private void RefreshHud()
        {
            if (statsManager == null)
            {
                SetText(waveText, showDebugFallbackText ? "Wave -/-    Enemies -/-" : string.Empty);
                SetText(healthText, showDebugFallbackText ? "HP -    Kills 0" : string.Empty);
                SetText(scoreText, showDebugFallbackText ? "Score 0    Time 00:00" : string.Empty);
                SetText(enemiesText, string.Empty);
                SetText(killsText, string.Empty);
                SetText(timeText, string.Empty);
            }
            else
            {
                SetText(waveText, $"Wave {statsManager.CurrentWaveNumber}/{statsManager.TotalWaves}    Enemies {statsManager.EnemiesRemainingThisWave}/{statsManager.EnemiesTotalThisWave}");
                SetText(healthText, $"{FormatHealthCompact()}    Kills {statsManager.Kills}");
                SetText(scoreText, $"Score {statsManager.Score}    Time {statsManager.FormattedSurvivalTime}");
                SetText(enemiesText, string.Empty);
                SetText(killsText, string.Empty);
                SetText(timeText, string.Empty);
            }

            SetText(fpsText, string.Empty);
            SetText(objectiveText, $"Objective: {GetObjectiveText()}");
        }

        private string FormatHealthCompact()
        {
            if (healthReader == null || !healthReader.HasHealthValue)
            {
                return "HP unavailable";
            }

            int current = Mathf.RoundToInt(healthReader.CurrentHealth);
            int max = Mathf.RoundToInt(healthReader.MaxHealth);
            return max > 0 ? $"HP {current}/{max}" : $"HP {current}";
        }

        private string GetObjectiveText()
        {
            if (gameFlowManager != null && !string.IsNullOrWhiteSpace(gameFlowManager.CurrentObjectiveText))
            {
                return gameFlowManager.CurrentObjectiveText;
            }

            if (waveManager != null && waveManager.ExtractionShouldUnlock)
            {
                return "Reach extraction";
            }

            if (statsManager == null || statsManager.CurrentWaveNumber <= 0)
            {
                return "Prepare for the first wave";
            }

            return "Survive the infected attack";
        }

        private static void SetText(Text target, string value)
        {
            if (target != null)
            {
                target.text = value;
            }
        }

        private void OnValidate()
        {
            updateInterval = Mathf.Max(0.02f, updateInterval);
        }
    }
}
