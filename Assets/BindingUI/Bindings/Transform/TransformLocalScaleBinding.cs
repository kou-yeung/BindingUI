using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformLocalScaleBinding<TState> : TransformBinding<TState, Vector3>
    {
        public TransformLocalScaleBinding(Transform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.localScale = Getter(state);
        }
    }
}
