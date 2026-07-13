using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class RectTransformBinding<TState, TValue> : ComponentBinding<TState, RectTransform, TValue>
    {
        public RectTransformBinding(RectTransform target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
}
}
