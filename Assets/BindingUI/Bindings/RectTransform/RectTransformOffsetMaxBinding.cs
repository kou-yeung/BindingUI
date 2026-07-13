using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformOffsetMaxBinding<TState> : RectTransformBinding<TState, Vector2>
    {
        public RectTransformOffsetMaxBinding(RectTransform target, Func<TState, Vector2> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.offsetMax = Getter(state);
        }
    }
}
