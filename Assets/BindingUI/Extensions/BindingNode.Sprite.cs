using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class SpriteBinding<TState> : IBinding<TState>
    {
        readonly Image target;
        readonly Func<TState, Sprite> getter;

        public SpriteBinding(Image target, Func<TState, Sprite> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            target.sprite = getter(state);
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Sprite(Func<TState, Sprite> getter)
        {
            Add(new SpriteBinding<TState>(Get<Image>(), getter));
            return this;
        }
    }
}
