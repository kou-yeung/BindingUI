using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformPositionBinding<TState> : TransformBinding<TState, Vector3>
    {
        public TransformPositionBinding(Transform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.position = Getter(state);
        }
    }
}
