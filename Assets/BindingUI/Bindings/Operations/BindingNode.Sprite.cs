using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Sprite(Func<TState, Sprite> getter)
        {
            if (GameObject.TryGetComponent<Image>(out var image))
            {
                Add(new ImageSpriteBinding<TState>(image, getter));
                return this;
            }
            if (GameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                Add(new SpriteRendererSpriteBinding<TState>(spriteRenderer, getter));
                return this;
            }
            throw new MissingComponentException($"BindingNode '{Id}' on GameObject '{GameObject.name}' " + $"does not have component 'Image' or 'SpriteRenderer'.");
        }
    }
}
