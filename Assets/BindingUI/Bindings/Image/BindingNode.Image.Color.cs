using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class ImageColorBinding<TState> : ImageBinding<TState, Color>
    {
        public ImageColorBinding(Image target, Func<TState, Color> getter) : base(target, getter)
        {
        }
        public override void Apply(TState state)
        {
            Target.color = Getter(state);
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
