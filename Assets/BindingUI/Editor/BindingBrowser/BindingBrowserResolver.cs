#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BindingUI.Editor
{
    public sealed class BindingBrowserEntry
    {
        public string Id { get; }
        public GameObject GameObject { get; }
        public string HierarchyPath { get; }
        public IReadOnlyList<string> ComponentNames { get; }
        public bool IsDuplicate { get; internal set; }

        public string BindCode => $"root.Bind(\"{Escape(Id)}\");";

        public string ComponentsText =>
            ComponentNames.Count == 0
                ? "-"
                : string.Join(", ", ComponentNames);

        public BindingBrowserEntry(
            string id,
            GameObject gameObject,
            string hierarchyPath,
            IReadOnlyList<string> componentNames)
        {
            Id = id;
            GameObject = gameObject;
            HierarchyPath = hierarchyPath;
            ComponentNames = componentNames;
        }

        static string Escape(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
        }
    }

    /// <summary>
    /// Binding Browser向けの列挙処理です。
    ///
    /// Runtime Resolverと異なり、重複IDがあっても例外を投げず、
    /// 重複した項目をすべて返します。
    /// </summary>
    public static class BindingBrowserResolver
    {
        public static IReadOnlyList<BindingBrowserEntry> Resolve(
            GameObject rootObject)
        {
            if (rootObject == null)
            {
                return Array.Empty<BindingBrowserEntry>();
            }

            var rootMarker = FindMarkerOn(rootObject.transform);

            var entries = rootObject
                .GetComponentsInChildren<BindingId>(includeInactive: true)
                .Where(bindingId =>
                    BelongsToRoot(
                        bindingId,
                        rootMarker))
                .Select(bindingId =>
                    CreateEntry(
                        rootObject.transform,
                        bindingId))
                .ToList();

            MarkDuplicates(entries);

            return entries;
        }

        static bool BelongsToRoot(
            BindingId bindingId,
            IBindingMarker rootMarker)
        {
            /*
             * BindingId自身がBindingViewのルートに付いている場合、
             * 親View側からそのNodeをBindできるように、
             * Marker検索はbindingId.transform.parentから開始します。
             */
            var ownerMarker =
                FindNearestMarker(bindingId.transform.parent);

            if (rootMarker == null)
            {
                return ownerMarker == null;
            }

            return ReferenceEquals(ownerMarker, rootMarker);
        }

        static BindingBrowserEntry CreateEntry(
            Transform root,
            BindingId bindingId)
        {
            var id = string.IsNullOrWhiteSpace(bindingId.Id)
                ? bindingId.gameObject.name
                : bindingId.Id;

            var componentNames = bindingId
                .GetComponents<Component>()
                .Where(IsVisibleComponent)
                .Select(GetDisplayTypeName)
                .Distinct()
                .ToArray();

            return new BindingBrowserEntry(
                id,
                bindingId.gameObject,
                GetHierarchyPath(root, bindingId.transform),
                componentNames);
        }

        static bool IsVisibleComponent(Component component)
        {
            return component != null
                && component is not Transform
                && component is not BindingId;
        }

        static string GetDisplayTypeName(Component component)
        {
            var type = component.GetType();

            // TMPro.TMP_Textなどは短い名前だけで十分です。
            return type.Name;
        }

        static void MarkDuplicates(
            IReadOnlyList<BindingBrowserEntry> entries)
        {
            var duplicateIds = entries
                .GroupBy(entry => entry.Id, StringComparer.Ordinal)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var entry in entries)
            {
                entry.IsDuplicate =
                    duplicateIds.Contains(entry.Id);
            }
        }

        static IBindingMarker FindMarkerOn(Transform transform)
        {
            if (transform == null)
            {
                return null;
            }

            return transform
                .GetComponents<MonoBehaviour>()
                .OfType<IBindingMarker>()
                .FirstOrDefault();
        }

        static IBindingMarker FindNearestMarker(Transform current)
        {
            while (current != null)
            {
                var marker = FindMarkerOn(current);

                if (marker != null)
                {
                    return marker;
                }

                current = current.parent;
            }

            return null;
        }

        static string GetHierarchyPath(
            Transform root,
            Transform target)
        {
            var names = new Stack<string>();
            var current = target;

            while (current != null)
            {
                names.Push(current.name);

                if (current == root)
                {
                    break;
                }

                current = current.parent;
            }

            return string.Join("/", names);
        }
    }
}

#endif