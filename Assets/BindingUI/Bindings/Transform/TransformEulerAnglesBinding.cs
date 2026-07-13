using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class TransformEulerAnglesBinding<TState> : TransformBinding<TState, Vector3>
    {
        public TransformEulerAnglesBinding(Transform target, Func<TState, Vector3> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.eulerAngles = Getter(state);
        }
    }
}
