using System;
using UnityEngine.UI;

namespace BindingUI
{
    public abstract class MaskableGraphicBinding<TState, TValue> : ComponentBinding<TState, MaskableGraphic, TValue>
    {
        public MaskableGraphicBinding(MaskableGraphic target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
}
}
