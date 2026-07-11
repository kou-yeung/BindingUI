using BindingUI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed class HierarchyBindingNodeResolver : IBindingNodeResolver
    {
        public GameObject GameObject { get; private set; }

        readonly Dictionary<string, BindingId> targets;

        public HierarchyBindingNodeResolver(GameObject root)
        {
            GameObject = root != null
                    ? root
                    : throw new ArgumentNullException(nameof(root));

            targets = BuildLookup(root); targets = BuildLookup(root);
        }

        public bool TryResolve(string bindingId, out BindingId target)
        {
            return targets.TryGetValue(bindingId, out target);
        }

        private static Dictionary<string, BindingId> BuildLookup(GameObject root)
        {
            Dictionary<string, BindingId> res = new Dictionary<string, BindingId>();

            var rootMarker = BindingUICore.GetInterface<IBindingMarker>(root);

            foreach (var bindingId in root.GetComponentsInChildren<BindingId>(true))
            {
                var ownerMarker = BindingUICore.GetInterfaceInParents<IBindingMarker>(bindingId.gameObject);

                if (IsOwnedByRoot(rootMarker, ownerMarker) == false)
                {
                    continue;
                }

                if (res.TryAdd(bindingId.Id, bindingId) == false)
                {
                    throw new Exception($"Duplicate BindingId '{bindingId.Id}'");
                }
            }
            return res;
        }
        private static bool IsOwnedByRoot(IBindingMarker rootMarker, IBindingMarker ownerMarker)
        {
            if (ownerMarker == null)
            {
                return true;
            }
            return ReferenceEquals(rootMarker, ownerMarker);
        }

    }
}
