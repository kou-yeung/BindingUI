using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class CanvasGroupInteractableBinding<TState> : CanvasGroupBinding<TState, bool>
    {
        public CanvasGroupInteractableBinding(CanvasGroup target, Func<TState, bool> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.interactable = Getter(state);
        }
    }
}
