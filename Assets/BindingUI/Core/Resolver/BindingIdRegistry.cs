using BindingUI.Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    /// <summary>
    /// Prefab内のBindingId参照を事前保存するResolver。
    /// 実行時にはHierarchy検索を行わない。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BindingIdRegistry : MonoBehaviour, IBindingNodeResolver
    {
        [SerializeField]
        List<BindingId> targets = new();

        Dictionary<string, BindingId> lookup;

        public GameObject GameObject => gameObject;
        public bool TryResolve(string bindingId, out BindingId target)
        {
            EnsureLookup();

            return lookup.TryGetValue(bindingId, out target);
        }

        void EnsureLookup()
        {
            if (lookup != null)
            {
                return;
            }

            lookup = new Dictionary<string, BindingId>(targets.Count, StringComparer.Ordinal);

            foreach (var target in targets)
            {
                if (target == null)
                {
                    throw new InvalidOperationException(
                        $"BindingTargetRegistry '{name}' contains a null target.");
                }

                if (string.IsNullOrEmpty(target.Id))
                {
                    throw new InvalidOperationException(
                        $"BindingId is empty on GameObject '{target.name}'.");
                }

                if (!lookup.TryAdd(target.Id, target))
                {
                    throw new InvalidOperationException(
                        $"Duplicate BindingId '{target.Id}' " +
                        $"in BindingTargetRegistry '{name}'.");
                }
            }
        }

        /// <summary>
        /// 保存済みDictionaryを破棄する。
        /// Editor上で一覧を更新した時などに使用する。
        /// </summary>
        internal void InvalidateLookup()
        {
            lookup = null;
        }

#if UNITY_EDITOR
        internal void RefreshTargets()
        {
            var rootMarker =
                BindingUIHelper.GetInterface<IBindingMarker>(
                    gameObject);

            var foundTargets =
                GetComponentsInChildren<BindingId>(true);

            var refreshedTargets =
                new List<BindingId>(foundTargets.Length);

            var usedIds =
                new HashSet<string>(StringComparer.Ordinal);

            foreach (var target in foundTargets)
            {
                var ownerMarker =
                    BindingUIHelper
                        .GetInterfaceInParents<IBindingMarker>(
                            target.gameObject);

                if (!IsOwnedByRoot(
                        rootMarker,
                        ownerMarker))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(target.Id))
                {
                    throw new InvalidOperationException(
                        $"BindingId is empty on GameObject " +
                        $"'{GetHierarchyPath(target.transform)}'.");
                }

                if (!usedIds.Add(target.Id))
                {
                    throw new InvalidOperationException(
                        $"Duplicate BindingId '{target.Id}' " +
                        $"under '{name}'.");
                }

                refreshedTargets.Add(target);
            }

            targets = refreshedTargets;
            InvalidateLookup();
        }

        static bool IsOwnedByRoot(
            IBindingMarker rootMarker,
            IBindingMarker ownerMarker)
        {
            if (ownerMarker == null)
            {
                return true;
            }

            return ReferenceEquals(
                rootMarker,
                ownerMarker);
        }

        static string GetHierarchyPath(
            Transform target)
        {
            var path = target.name;
            var current = target.parent;

            while (current != null)
            {
                path = $"{current.name}/{path}";
                current = current.parent;
            }

            return path;
        }
#endif
    }
}