using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class ImageFillAmountBinding<TState> : ImageBinding<TState, float>
    {
        public ImageFillAmountBinding(Image target, Func<TState, float> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.fillAmount = Getter(state);
        }
    }
}
