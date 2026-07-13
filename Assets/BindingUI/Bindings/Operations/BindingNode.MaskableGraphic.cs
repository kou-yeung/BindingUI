using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Color(Func<TState, Color> getter)
        {
            Add(new MaskableGraphicColorBinding<TState>(Get<MaskableGraphic>(), getter));
            return this;
        }
        public BindingNode<TState> Maskable(Func<TState, bool> getter)
        {
            Add(new MaskableGraphicMaskableBinding<TState>(Get<MaskableGraphic>(), getter));
            return this;
        }
    }
}
