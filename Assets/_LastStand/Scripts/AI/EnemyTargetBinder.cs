// Initially generated with Codex assistance and intended for student review/modification.
using System;
using System.Reflection;
using UnityEngine;

namespace LastStand.AI
{
    public class EnemyTargetBinder : MonoBehaviour
    {
        private static readonly string[] TargetMemberNames =
        {
            "Target",
            "target",
            "CurrentTarget",
            "currentTarget",
            "TargetToAttack",
            "targetToAttack"
        };

        [SerializeField] private GameObject explicitTarget;
        [SerializeField] private string playerObjectName = "Player_JUTPS";
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private bool bindOnStart = true;
        [SerializeField] private bool includeChildren = true;
        [SerializeField] private bool logBindingResult;

        private void Start()
        {
            if (bindOnStart)
            {
                BindTarget();
            }
        }

        public bool BindTarget(GameObject targetOverride = null)
        {
            GameObject target = targetOverride != null ? targetOverride : ResolveTarget();
            if (target == null)
            {
                Log("No target found for enemy target binding.");
                return false;
            }

            MonoBehaviour[] behaviours = includeChildren
                ? GetComponentsInChildren<MonoBehaviour>(true)
                : GetComponents<MonoBehaviour>();

            int bindCount = 0;
            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null || behaviour == this || !LooksLikeAiComponent(behaviour.GetType()))
                {
                    continue;
                }

                bindCount += TryBindMembers(behaviour, target);
            }

            Log(bindCount > 0
                ? $"Bound {bindCount} target member(s) on {name} to {target.name}."
                : $"Found target {target.name}, but no empty compatible AI target members were bound on {name}.");

            return bindCount > 0;
        }

        private GameObject ResolveTarget()
        {
            if (explicitTarget != null)
            {
                return explicitTarget;
            }

            if (!string.IsNullOrWhiteSpace(playerObjectName))
            {
                GameObject namedTarget = GameObject.Find(playerObjectName);
                if (namedTarget != null)
                {
                    return namedTarget;
                }
            }

            if (!string.IsNullOrWhiteSpace(playerTag))
            {
                try
                {
                    return GameObject.FindWithTag(playerTag);
                }
                catch (UnityException)
                {
                    return null;
                }
            }

            return null;
        }

        private static bool LooksLikeAiComponent(Type type)
        {
            string typeName = type.Name;
            string fullName = type.FullName ?? typeName;

            return typeName.IndexOf("JU_AI", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("JUAI", StringComparison.OrdinalIgnoreCase) >= 0
                   || typeName.IndexOf("AI", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("JU_AI", StringComparison.OrdinalIgnoreCase) >= 0
                   || fullName.IndexOf("JUAI", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static int TryBindMembers(MonoBehaviour behaviour, GameObject target)
        {
            Type type = behaviour.GetType();
            int bindCount = 0;

            foreach (string memberName in TargetMemberNames)
            {
                if (TryBindField(behaviour, type, memberName, target))
                {
                    bindCount++;
                }

                if (TryBindProperty(behaviour, type, memberName, target))
                {
                    bindCount++;
                }
            }

            return bindCount;
        }

        private static bool TryBindField(object owner, Type ownerType, string fieldName, GameObject target)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo field = ownerType.GetField(fieldName, flags);
            if (field == null || field.IsInitOnly)
            {
                return false;
            }

            try
            {
                if (IsAssigned(field.GetValue(owner)) || !TryResolveValue(field.FieldType, target, out object value))
                {
                    return false;
                }

                field.SetValue(owner, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryBindProperty(object owner, Type ownerType, string propertyName, GameObject target)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo property = ownerType.GetProperty(propertyName, flags);
            if (property == null || !property.CanWrite || property.GetIndexParameters().Length > 0)
            {
                return false;
            }

            try
            {
                if ((property.CanRead && IsAssigned(property.GetValue(owner)))
                    || !TryResolveValue(property.PropertyType, target, out object value))
                {
                    return false;
                }

                property.SetValue(owner, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryResolveValue(Type memberType, GameObject target, out object value)
        {
            if (memberType == typeof(GameObject))
            {
                value = target;
                return true;
            }

            if (memberType == typeof(Transform))
            {
                value = target.transform;
                return true;
            }

            if (typeof(Component).IsAssignableFrom(memberType))
            {
                value = target.GetComponent(memberType);
                return value != null;
            }

            value = null;
            return false;
        }

        private static bool IsAssigned(object value)
        {
            return value is UnityEngine.Object unityObject ? unityObject != null : value != null;
        }

        private void Log(string message)
        {
            if (logBindingResult)
            {
                Debug.Log($"[EnemyTargetBinder] {message}", this);
            }
        }
    }
}
