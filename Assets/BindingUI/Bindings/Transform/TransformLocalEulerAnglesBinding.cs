using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformLocalEulerAnglesBinding<TState> : TransformBinding<TState, Vector3>
    {
        public TransformLocalEulerAnglesBinding(Transform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.localEulerAngles = Getter(state);
        }
    }
}
