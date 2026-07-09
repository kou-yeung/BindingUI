using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public string Id { get; }
        public GameObject GameObject { get; }

        readonly List<IBinding<TState>> bindings;

        public BindingNode(
            string id,
            GameObject gameObject,
            List<IBinding<TState>> bindings)
        {
            Id = id;
            GameObject = gameObject;
            this.bindings = bindings;
        }

        internal void Add(IBinding<TState> binding)
        {
            bindings.Add(binding);
        }

        public T Get<T>() where T : Component
        {
            var component = GameObject.GetComponent<T>();
            if (component == null)
                throw new MissingComponentException(
                    $"{Id} does not have component: {typeof(T).Name}");

            return component;
        }
    }
}