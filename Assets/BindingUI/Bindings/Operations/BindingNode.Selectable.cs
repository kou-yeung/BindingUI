using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Interactable(Func<TState, bool> getter)
        {
            Add(new SelectableInteractableBinding<TState>(Get<Selectable>(), getter));
            return this;
        }
    }
}
