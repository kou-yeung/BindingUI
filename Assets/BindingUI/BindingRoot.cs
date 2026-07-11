using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed class BindingRoot<TState>
    {
        readonly IBindingNodeResolver resolver;
        readonly Dictionary<string, BindingNode<TState>> nodes = new();
        readonly List<IBinding<TState>> bindings = new();

        public BindingRoot(GameObject root)
        {
            resolver = new HierarchyBindingNodeResolver(root);
        }

        public BindingRoot(IBindingNodeResolver bindingNodeResolver)
        {
            resolver = bindingNodeResolver ?? throw new ArgumentNullException(nameof(bindingNodeResolver));
        }
        public BindingNode<TState> Bind(string id)
        {
            if (nodes.TryGetValue(id, out var cache))
            {
                return cache;
            }

            if (resolver.TryResolve(id, out var target) == false)
            {
                throw new KeyNotFoundException($"BindingId not found: {id}");
            }

            var node = new BindingNode<TState>(id, target.gameObject, bindings);

            nodes.Add(id, node);

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
