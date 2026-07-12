using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class CanvasGroupAlphaBinding<TState> : CanvasGroupBinding<TState, float>
    {
        public CanvasGroupAlphaBinding(CanvasGroup target, Func<TState, float> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.alpha = Getter(state);
        }
    }
}
