using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformLocalRotationBinding<TState> : TransformBinding<TState, Quaternion>
    {
        public TransformLocalRotationBinding(Transform target, Func<TState, Quaternion> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.localRotation = Getter(state);
        }
    }
}
