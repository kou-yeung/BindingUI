using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class ImageColorBinding<TState> : IBinding<TState>
    {
        readonly Image target;
        readonly Func<TState, Color> getter;

        public ImageColorBinding(Image target, Func<TState, Color> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            target.color = getter(state);
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> ImageColor(Func<TState, Color> getter)
        {
            Add(new ImageColorBinding<TState>(Get<Image>(), getter));
            return this;
        }
    }
}
