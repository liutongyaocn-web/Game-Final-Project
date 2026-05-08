// Initially generated with Codex assistance and intended for student review/modification.
using UnityEngine;

namespace LastStand.UI
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private float updateInterval = 0.5f;

        private int frameCount;
        private float accumulatedTime;
        private float nextUpdateTime;

        public float CurrentFps { get; private set; }

        private void Update()
        {
            float deltaTime = Time.unscaledDeltaTime;
            frameCount++;
            accumulatedTime += deltaTime;

            if (Time.unscaledTime < nextUpdateTime && accumulatedTime < updateInterval)
            {
                return;
            }

            CurrentFps = accumulatedTime > 0f ? frameCount / accumulatedTime : 0f;
            frameCount = 0;
            accumulatedTime = 0f;
            nextUpdateTime = Time.unscaledTime + updateInterval;
        }

        private void OnValidate()
        {
            updateInterval = Mathf.Max(0.1f, updateInterval);
        }
    }
}
