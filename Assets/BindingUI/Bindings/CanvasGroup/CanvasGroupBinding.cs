using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class CanvasGroupBinding<TState, TValue> : ComponentBinding<TState, CanvasGroup, TValue>
    {
        protected CanvasGroupBinding(CanvasGroup target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
    }
}
