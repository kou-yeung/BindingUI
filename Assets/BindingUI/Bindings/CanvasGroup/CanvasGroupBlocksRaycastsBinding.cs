using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class CanvasGroupBlocksRaycastsBinding<TState> : CanvasGroupBinding<TState, bool>
    {
        public CanvasGroupBlocksRaycastsBinding(CanvasGroup target, Func<TState, bool> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.blocksRaycasts = Getter(state);
        }
    }
}
