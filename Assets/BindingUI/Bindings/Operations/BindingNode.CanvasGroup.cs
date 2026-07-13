using System;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> CanvasGroupAlpha(Func<TState, float> getter)
        {
            Add(new CanvasGroupAlphaBinding<TState>(Get<CanvasGroup>(), getter));
            return this;
        }
        public BindingNode<TState> CanvasGroupInteractable(Func<TState, bool> getter)
        {
            Add(new CanvasGroupInteractableBinding<TState>(Get<CanvasGroup>(), getter));
            return this;
        }
        public BindingNode<TState> CanvasGroupBlocksRaycasts(Func<TState, bool> getter)
        {
            Add(new CanvasGroupBlocksRaycastsBinding<TState>(Get<CanvasGroup>(), getter));
            return this;
        }
    }
}
