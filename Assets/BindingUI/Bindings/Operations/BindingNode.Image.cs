using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> FillAmount(Func<TState, float> getter)
        {
            Add(new ImageFillAmountBinding<TState>(Get<Image>(), getter));
            return this;
        }
    }
}
