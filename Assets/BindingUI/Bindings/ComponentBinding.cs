using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class ComponentBinding<TState, TComponent, TValue> : IBinding<TState> where TComponent : Component
    {
        protected TComponent Target { get; }
        protected Func<TState, TValue> Getter { get; }

        protected ComponentBinding(TComponent target, Func<TState, TValue> getter)
        {
            Target = target;
            Getter = getter;
        }

        public abstract void Apply(TState state);
    }
}
