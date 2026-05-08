// Initially generated with Codex assistance and intended for student review/modification.
using System;
using System.Reflection;
using UnityEngine;

namespace LastStand.UI
{
    public class PlayerHealthReader : MonoBehaviour
    {
        private static readonly string[] CurrentHealthNames =
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

        private static readonly string[] MaxHealthNames =
        {
            "MaxHealth",
            "maxHealth",
            "MaxHP",
            "maxHP",
            "MaxLife",
            "maxLife"
        };

        [SerializeField] private GameObject player;
        [SerializeField] private string playerObjectName = "Player_JUTPS";
        [SerializeField] private bool autoFindPlayer = true;

        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool HasHealthValue { get; private set; }

        private void Start()
        {
            ResolvePlayer();
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        public void SetPlayer(GameObject targetPlayer)
        {
            player = targetPlayer;
        }

        public void Refresh()
        {
            if (player == null && autoFindPlayer)
            {
                ResolvePlayer();
            }

            HasHealthValue = false;
            if (player == null)
            {
                return;
            }

            MonoBehaviour[] behaviours = player.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null || !LooksLikeHealthComponent(behaviour.GetType()))
                {
                    continue;
                }

                Type type = behaviour.GetType();
                if (TryReadFirstNumericMember(behaviour, type, CurrentHealthNames, out float currentHealth))
                {
                    CurrentHealth = currentHealth;
                    MaxHealth = TryReadFirstNumericMember(behaviour, type, MaxHealthNames, out float maxHealth)
                        ? maxHealth
                        : Mathf.Max(MaxHealth, currentHealth);
                    HasHealthValue = true;
                    return;
                }
            }
        }

        private void ResolvePlayer()
        {
            if (!string.IsNullOrWhiteSpace(playerObjectName))
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

        private static bool TryReadFirstNumericMember(object owner, Type ownerType, string[] memberNames, out float value)
        {
            foreach (string memberName in memberNames)
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
    }
}
