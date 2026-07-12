using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class SpriteRendererBinding<TState, TValue> : ComponentBinding<TState, SpriteRenderer, TValue>
    {
        public SpriteRendererBinding(SpriteRenderer target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
}
}
