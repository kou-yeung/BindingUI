using System;
using UnityEngine;

namespace BindingUI
{
    internal abstract class AnimatorStateBinding<TState, TValue> : AnimatorBinding<TState, TValue>
    {
        protected readonly int parameterHash;
        public AnimatorStateBinding(Animator animator, int parameterHash, Func<TState, TValue> getter) : base(animator, getter)
        {
            this.parameterHash = parameterHash;
        }
    }

    sealed class AnimatorBoolBinding<TState> : AnimatorStateBinding<TState, bool>
    {
        public AnimatorBoolBinding(Animator animator, int parameterHash, Func<TState, bool> getter) : base(animator, parameterHash, getter)
        {
        }
        public override void Apply(TState state)
        {
            Target.SetBool(parameterHash, Getter(state));
        }
    }

    sealed class AnimatorIntBinding<TState> : AnimatorStateBinding<TState, int>
    {
        public AnimatorIntBinding(Animator animator, int parameterHash, Func<TState, int> getter) : base(animator, parameterHash, getter) { }

        public override void Apply(TState state)
        {
            Target.SetInteger(parameterHash, Getter(state));
        }
    }

    sealed class AnimatorFloatBinding<TState> : AnimatorStateBinding<TState, float>
    {
        public AnimatorFloatBinding(Animator animator, int parameterHash, Func<TState, float> getter) : base(animator, parameterHash, getter) { }

        public override void Apply(TState state)
        {
            Target.SetFloat(parameterHash, Getter(state));
        }
    }

}
