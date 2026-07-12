using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class GameObjectVisibleBinding<TState> : GameObjectBinding<TState, bool>
    {
        public GameObjectVisibleBinding(GameObject target, Func<TState, bool> getter) : base(target, getter){}

        public override void Apply(TState state)
        {
            Target.SetActive(Getter(state));
        }
    }
}
