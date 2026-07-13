using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformAnchoredPositionBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformAnchoredPositionBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.anchoredPosition = Getter(state);
        }
    }
}
