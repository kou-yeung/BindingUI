using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformAnchorMaxBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformAnchorMaxBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.anchorMax = Getter(state);
        }
    }
}
