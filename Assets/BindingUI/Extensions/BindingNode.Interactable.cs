using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class InteractableBinding<TState> : IBinding<TState>
    {
        readonly Selectable target;
        readonly Func<TState, bool> getter;

        public InteractableBinding(Selectable target, Func<TState, bool> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            target.interactable = getter(state);
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Interactable(Func<TState, bool> getter)
        {
            Add(new InteractableBinding<TState>(Get<Selectable>(), getter));
            return this;
        }
    }
}
