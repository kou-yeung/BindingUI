using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformAnchorMinBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformAnchorMinBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.anchorMin = Getter(state);
        }
    }
}
