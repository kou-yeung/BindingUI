using System;
using UnityEngine.UI;

namespace BindingUI
{
    public abstract class ImageBinding<TState, TValue> : ComponentBinding<TState, Image, TValue>
    {
        public ImageBinding(Image target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
}
}
