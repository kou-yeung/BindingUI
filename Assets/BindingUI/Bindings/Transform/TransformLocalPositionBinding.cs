using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformLocalPositionBinding<TState> : TransformBinding<TState, Vector3>
    {
        public TransformLocalPositionBinding(Transform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.localPosition = Getter(state);
        }
    }
}
