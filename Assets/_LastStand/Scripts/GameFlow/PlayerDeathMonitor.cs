// Initially generated with Codex assistance and intended for student review/modification.
using System;
using System.Reflection;
using LastStand.UI;
using UnityEngine;

namespace LastStand.GameFlow
{
    public class PlayerDeathMonitor : MonoBehaviour
    {
        private static readonly string[] HealthNames =
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

        [SerializeField] private GameFlowManager gameFlowManager;
        [SerializeField] private PlayerHealthReader healthReader;
        [SerializeField] private GameObject player;
        [SerializeField] private string playerObjectName = "Player_JUTPS";
        [SerializeField] private bool autoFindReferences = true;
        [SerializeField] private bool monitorOnUpdate = true;
        [SerializeField] private bool logDeathEvents;
        [Header("Debug Validation")]
        [SerializeField] private bool debugReportDeathOnStart;

        private bool hasReportedDeath;

        private void Start()
        {
            ResolveReferences();

            if (debugReportDeathOnStart)
            {
                DebugReportPlayerDeath();
            }
        }

        private void Update()
        {
            if (!monitorOnUpdate || hasReportedDeath)
            {
                return;
            }

            ResolveReferences();
            if (TryReadPlayerHealth(out float currentHealth) && currentHealth <= 0f)
            {
                ReportPlayerDeath();
            }
        }

        public void DebugReportPlayerDeath()
        {
            ReportPlayerDeath();
        }

        private void ReportPlayerDeath()
        {
            if (hasReportedDeath)
            {
                return;
            }

            hasReportedDeath = true;
            if (gameFlowManager != null)
            {
                gameFlowManager.FailRun();
            }

            Log("Player death reported.");
        }

        private bool TryReadPlayerHealth(out float currentHealth)
        {
            if (healthReader != null)
            {
                healthReader.Refresh();
                if (healthReader.HasHealthValue)
                {
                    currentHealth = healthReader.CurrentHealth;
                    return true;
                }
            }

            currentHealth = 0f;
            if (player == null)
            {
                return false;
            }

            MonoBehaviour[] behaviours = player.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null || !LooksLikeHealthComponent(behaviour.GetType()))
                {
                    continue;
                }

                if (TryReadFirstNumericMember(behaviour, behaviour.GetType(), out currentHealth))
                {
                    return true;
                }
            }

            return false;
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

            if (healthReader == null)
            {
                healthReader = FindFirstObjectByType<PlayerHealthReader>();
            }

            if (player == null && !string.IsNullOrWhiteSpace(playerObjectName))
            {
                player = GameObject.Find(playerObjectName);
            }
        }

        private static bool LooksLikeHealthComponent(Type type)
        {
            string typeName = type.Name;
            string fullName = type.FullName ?? typeName;

            return typeName.IndexOf("Health", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("Damage", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("Character", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("JU", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("JUTPS", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Health", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Damage", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("Character", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("JU", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("JUTPS", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool TryReadFirstNumericMember(object owner, Type ownerType, out float value)
        {
            foreach (string memberName in HealthNames)
            {
                if (TryReadNumericMember(owner, ownerType, memberName, out value))
                {
                    return true;
                }
            }

            value = 0f;
            return false;
        }

        private static bool TryReadNumericMember(object owner, Type ownerType, string memberName, out float value)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            try
            {
                FieldInfo field = ownerType.GetField(memberName, flags);
                if (field != null && TryConvertToFloat(field.GetValue(owner), out value))
                {
                    return true;
                }

                PropertyInfo property = ownerType.GetProperty(memberName, flags);
                if (property != null
                    && property.CanRead
                    && property.GetIndexParameters().Length == 0
                    && TryConvertToFloat(property.GetValue(owner), out value))
                {
                    return true;
                }
            }
            catch
            {
                value = 0f;
                return false;
            }

            value = 0f;
            return false;
        }

        private static bool TryConvertToFloat(object rawValue, out float value)
        {
            if (rawValue == null)
            {
                value = 0f;
                return false;
            }

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

            value = 0f;
            return false;
        }

        private void Log(string message)
        {
            if (logDeathEvents)
            {
                Debug.Log($"[PlayerDeathMonitor] {message}", this);
            }
        }
    }
}
