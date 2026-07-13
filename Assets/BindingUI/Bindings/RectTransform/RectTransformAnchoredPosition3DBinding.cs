using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class RectTransformAnchoredPosition3DBinding<TState> : RectTransformBinding<TState, Vector3>
    {
        public RectTransformAnchoredPosition3DBinding(RectTransform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.anchoredPosition3D = Getter(state);
        }
    }
}
