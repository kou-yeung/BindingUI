using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformSizeDeltaBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformSizeDeltaBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.sizeDelta = Getter(state);
        }
    }
}
