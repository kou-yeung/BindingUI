using System;
using UnityEngine;

namespace BindingUI
{
    public abstract class AnimatorBinding<TState, TValue> : ComponentBinding<TState, Animator, TValue>
    {
        protected AnimatorBinding(Animator target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
    }
}
