using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class SpriteRendererSpriteBinding<TState> : SpriteRendererBinding<TState, Sprite>
    {
        public SpriteRendererSpriteBinding(SpriteRenderer target, Func<TState, Sprite> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.sprite = Getter(state);
        }
    }
}
