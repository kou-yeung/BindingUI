using System;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> AnimationState(string parameterName, Func<TState, bool> getter)
        {
            var animator = Get<Animator>();
            var hash = Animator.StringToHash(parameterName);
            Add(new AnimatorBoolBinding<TState>(animator, hash,getter));
            return this;
        }
        public BindingNode<TState> AnimationState(string parameterName, Func<TState, int> getter)
        {
            var animator = Get<Animator>();
            var hash = Animator.StringToHash(parameterName);
            Add(new AnimatorIntBinding<TState>(animator, hash, getter));
            return this;
        }
        public BindingNode<TState> AnimationState(string parameterName, Func<TState, float> getter)
        {
            var animator = Get<Animator>();
            var hash = Animator.StringToHash(parameterName);
            Add(new AnimatorFloatBinding<TState>(animator, hash, getter));
            return this;
        }
    }
}
