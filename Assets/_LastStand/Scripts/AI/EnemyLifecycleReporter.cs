// Initially generated with Codex assistance and intended for student review/modification.
using System;
using System.Reflection;
using LastStand.Waves;
using UnityEngine;

namespace LastStand.AI
{
    public class EnemyLifecycleReporter : MonoBehaviour
    {
        private static readonly string[] DeadMemberNames =
        {
            "IsDead",
            "isDead",
            "Dead",
            "dead"
        };

        private static readonly string[] AliveMemberNames =
        {
            "IsAlive",
            "isAlive",
            "Alive",
            "alive"
        };

        private static readonly string[] HealthMemberNames =
        {
            "Health",
            "health",
            "CurrentHealth",
            "currentHealth",
            "HP",
            "hp",
            "Life",
            "life"
        };

        [SerializeField] private WaveManager waveManager;
        [SerializeField] private bool autoFindWaveManager = true;
        [SerializeField] private bool pollHealthState = true;
        [SerializeField] private float pollIntervalSeconds = 0.2f;
        [SerializeField] private bool reportOnDestroy = true;
        [SerializeField] private bool reportOnDisable;
        [SerializeField] private bool logLifecycleEvents;
        [SerializeField] private string lastReportReason;

        private bool hasReported;
        private bool isQuitting;
        private bool hasStarted;
        private float nextPollTime;

        public string LastReportReason => lastReportReason;
        public bool HasReported => hasReported;
        public event Action<EnemyLifecycleReporter, GameObject, string> Defeated;

        private void Awake()
        {
            Application.quitting += HandleApplicationQuitting;
        }

        private void Start()
        {
            hasStarted = true;

            if (waveManager == null && autoFindWaveManager)
            {
                waveManager = FindFirstObjectByType<WaveManager>();
            }
        }

        private void Update()
        {
            if (!pollHealthState || hasReported || !hasStarted || Time.time < nextPollTime)
            {
                return;
            }

            nextPollTime = Time.time + pollIntervalSeconds;

            if (TryDetectDefeated(out string reason))
            {
                ReportDefeated(reason);
            }
        }

        public void Configure(WaveManager manager)
        {
            if (manager != null)
            {
                waveManager = manager;
            }
        }

        public void ReportDefeated(string reason)
        {
            if (hasReported)
            {
                return;
            }

            hasReported = true;
            lastReportReason = string.IsNullOrWhiteSpace(reason) ? "Unknown" : reason;

            Defeated?.Invoke(this, gameObject, lastReportReason);

            if (waveManager == null && autoFindWaveManager)
            {
                waveManager = FindFirstObjectByType<WaveManager>();
            }

            if (waveManager != null)
            {
                waveManager.NotifyEnemyDefeated(gameObject);
            }

            Log($"Reported defeated: {lastReportReason}");
        }

        [ContextMenu("Force Report Defeated For Debug")]
        public void ForceReportDefeatedForDebug()
        {
            ReportDefeated("Debug force-report");
        }

        private bool TryDetectDefeated(out string reason)
        {
            MonoBehaviour[] behaviours = GetComponentsInChildren<MonoBehaviour>(true);
            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null || behaviour == this || !LooksLikeLifecycleComponent(behaviour.GetType()))
                {
                    continue;
                }

                Type type = behaviour.GetType();

                foreach (string memberName in DeadMemberNames)
                {
                    if (TryReadBoolMember(behaviour, type, memberName, out bool isDead) && isDead)
                    {
                        reason = $"{type.Name}.{memberName} == true";
                        return true;
                    }
                }

                foreach (string memberName in AliveMemberNames)
                {
                    if (TryReadBoolMember(behaviour, type, memberName, out bool isAlive) && !isAlive)
                    {
                        reason = $"{type.Name}.{memberName} == false";
                        return true;
                    }
                }

                foreach (string memberName in HealthMemberNames)
                {
                    if (TryReadNumericMember(behaviour, type, memberName, out float health) && health <= 0f)
                    {
                        reason = $"{type.Name}.{memberName} <= 0";
                        return true;
                    }
                }
            }

            reason = null;
            return false;
        }

        private static bool LooksLikeLifecycleComponent(Type type)
        {
            string typeName = type.Name;
            string fullName = type.FullName ?? typeName;

            return typeName.IndexOf("Health", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("Damage", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("AI", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("Character", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("JU", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Health", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Damage", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("AI", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Character", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("JU", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool TryReadBoolMember(object owner, Type ownerType, string memberName, out bool value)
        {
            if (TryReadMember(owner, ownerType, memberName, out object rawValue) && rawValue is bool boolValue)
            {
                value = boolValue;
                return true;
            }

            value = false;
            return false;
        }

        private static bool TryReadNumericMember(object owner, Type ownerType, string memberName, out float value)
        {
            if (TryReadMember(owner, ownerType, memberName, out object rawValue) && rawValue != null)
            {
                TypeCode typeCode = Type.GetTypeCode(rawValue.GetType());
                if (typeCode is TypeCode.Byte or TypeCode.SByte or TypeCode.Int16 or TypeCode.UInt16
                    or TypeCode.Int32 or TypeCode.UInt32 or TypeCode.Int64 or TypeCode.UInt64
                    or TypeCode.Single or TypeCode.Double or TypeCode.Decimal)
                {
                    try
                    {
                        value = Convert.ToSingle(rawValue);
                        return true;
                    }
                    catch
                    {
                        value = 0f;
                        return false;
                    }
                }
            }

            value = 0f;
            return false;
        }

        private static bool TryReadMember(object owner, Type ownerType, string memberName, out object value)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            try
            {
                FieldInfo field = ownerType.GetField(memberName, flags);
                if (field != null)
                {
                    value = field.GetValue(owner);
                    return true;
                }

                PropertyInfo property = ownerType.GetProperty(memberName, flags);
                if (property != null && property.CanRead && property.GetIndexParameters().Length == 0)
                {
                    value = property.GetValue(owner);
                    return true;
                }
            }
            catch
            {
                value = null;
                return false;
            }

            value = null;
            return false;
        }

        private void OnDisable()
        {
            if (reportOnDisable && Application.isPlaying && hasStarted && !isQuitting)
            {
                ReportDefeated("GameObject disabled");
            }
        }

        private void OnDestroy()
        {
            Application.quitting -= HandleApplicationQuitting;

            if (reportOnDestroy && Application.isPlaying && hasStarted && !isQuitting)
            {
                ReportDefeated("GameObject destroyed");
            }
        }

        private void HandleApplicationQuitting()
        {
            isQuitting = true;
        }

        private void OnValidate()
        {
            pollIntervalSeconds = Mathf.Max(0.05f, pollIntervalSeconds);
        }

        private void Log(string message)
        {
            if (logLifecycleEvents)
            {
                Debug.Log($"[EnemyLifecycleReporter] {message}", this);
            }
        }
    }
}
