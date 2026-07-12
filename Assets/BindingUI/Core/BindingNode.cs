using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public string Id { get; }
        public GameObject GameObject { get; }

        readonly List<IBinding<TState>> bindings;

        Dictionary<Type, Component> cache;

        public BindingNode(
            string id,
            GameObject gameObject,
            List<IBinding<TState>> bindings)
        {
            Id = id;
            GameObject = gameObject;
            this.bindings = bindings;
        }

        public void Add(IBinding<TState> binding)
        {
            bindings.Add(binding);
        }

        public T Get<T>() where T : Component
        {
            cache ??= new Dictionary<Type, Component>();

            var type = typeof(T);

            if (cache.TryGetValue(type, out var component))
            {
                return (T)component;
            }

            component = GameObject.GetComponent<T>();

            if (component == null)
            {
                throw new MissingComponentException($"BindingNode '{Id}' on GameObject '{GameObject.name}' " + $"does not have component '{type.FullName}'.");
            }

            cache.Add(type, component);

            return (T)component;
        }
    }
}