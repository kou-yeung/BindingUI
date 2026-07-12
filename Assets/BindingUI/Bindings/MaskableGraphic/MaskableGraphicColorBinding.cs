using System;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class MaskableGraphicColorBinding<TState> : MaskableGraphicBinding<TState, Color>
    {
        public MaskableGraphicColorBinding(MaskableGraphic target, Func<TState, Color> getter) : base(target, getter)
        {
        }
        public override void Apply(TState state)
        {
            Target.color = Getter(state);
        }
    }
}
