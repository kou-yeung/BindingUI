using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformRotationBinding<TState> : TransformBinding<TState, Quaternion>
    {
        public TransformRotationBinding(Transform target, Func<TState, Quaternion> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.rotation = Getter(state);
        }
    }
}
