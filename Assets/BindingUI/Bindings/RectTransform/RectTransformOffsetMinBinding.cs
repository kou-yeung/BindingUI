using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformOffsetMinBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformOffsetMinBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.offsetMin = Getter(state);
        }
    }
}
