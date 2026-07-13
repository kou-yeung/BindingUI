using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class MaskableGraphicMaskableBinding<TState> : MaskableGraphicBinding<TState, bool>
    {
        public MaskableGraphicMaskableBinding(MaskableGraphic target, Func<TState, bool> getter) : base(target, getter)
        {
        }
        public override void Apply(TState state)
        {
            Target.maskable = Getter(state);
        }
    }
}
