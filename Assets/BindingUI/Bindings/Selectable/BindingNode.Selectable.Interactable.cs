using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class SelectableInteractableBinding<TState> : SelectableBinding<TState, bool>
    {
        public SelectableInteractableBinding(Selectable target, Func<TState, bool> getter) : base(target, getter)
        {
        }
        public override void Apply(TState state)
        {
            Target.interactable = Getter(state);
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Interactable(Func<TState, bool> getter)
        {
            Add(new SelectableInteractableBinding<TState>(Get<Selectable>(), getter));
            return this;
        }
    }
}
