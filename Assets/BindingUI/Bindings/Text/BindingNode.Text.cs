using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Text(Func<TState, string> getter)
        {
            Add(new TextValueBinding<TState>(Get<Text>(), getter));
            return this;
        }
    }
}
