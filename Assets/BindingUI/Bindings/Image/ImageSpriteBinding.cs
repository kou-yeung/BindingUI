using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class ImageSpriteBinding<TState> : ImageBinding<TState, Sprite>
    {
        public ImageSpriteBinding(Image target, Func<TState, Sprite> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.sprite = Getter(state);
        }
    }
}
