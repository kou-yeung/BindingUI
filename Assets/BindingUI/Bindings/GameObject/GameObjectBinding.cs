using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class GameObjectBinding<TState, TValue> : IBinding<TState>
    {
        protected GameObject Target { get; }
        protected Func<TState, TValue> Getter { get; }

        protected GameObjectBinding(GameObject target, Func<TState, TValue> getter)
        {
            Target = target;
            Getter = getter;
        }
        public abstract void Apply(TState state);
    }
}
