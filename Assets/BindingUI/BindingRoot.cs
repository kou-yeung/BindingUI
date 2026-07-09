using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed class BindingRoot<TState>
    {
        readonly Dictionary<string, BindingNode<TState>> nodes = new();
        readonly List<IBinding<TState>> bindings = new();

        public BindingRoot(GameObject root)
        {
            foreach (var bindingId in root.GetComponentsInChildren<BindingId>(true))
            {
                var node = new BindingNode<TState>(bindingId.Id, bindingId.gameObject, bindings);

                if (nodes.TryAdd(bindingId.Id, node) == false)
                {
                    throw new Exception($"Duplicate BindingId '{bindingId.Id}'");
                }
            }
        }

        public BindingNode<TState> Bind(string id)
        {
            if (!nodes.TryGetValue(id, out var node))
            {
                throw new KeyNotFoundException($"BindingId not found: {id}");
            }
            return node;
        }

        public void Apply(TState state)
        {
            foreach (var binding in bindings)
            {
                binding.Apply(state);
            }
        }
    }
}
