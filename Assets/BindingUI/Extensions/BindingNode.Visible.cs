using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class VisibleBinding<TState> : IBinding<TState>
    {
        readonly GameObject target;
        readonly Func<TState, bool> getter;

        public VisibleBinding(GameObject target, Func<TState, bool> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            target.SetActive(getter(state));
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Visible(Func<TState, bool> getter)
        {
            Add(new VisibleBinding<TState>(GameObject, getter));
            return this;
        }
    }
}
