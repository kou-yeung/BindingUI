using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class TransformBinding<TState, TValue> : ComponentBinding<TState, Transform, TValue>
    {
        public TransformBinding(Transform target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
}
}
