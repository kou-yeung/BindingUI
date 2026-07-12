using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class VisibleBinding<TState> : GameObjectBinding<TState, bool>
    {
        public VisibleBinding(GameObject target, Func<TState, bool> getter) : base(target, getter){}

        public override void Apply(TState state)
        {
            Target.SetActive(Getter(state));
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
