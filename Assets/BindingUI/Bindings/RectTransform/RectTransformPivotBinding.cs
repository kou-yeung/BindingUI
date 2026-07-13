using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformPivotBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformPivotBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.pivot = Getter(state);
        }
    }
}
