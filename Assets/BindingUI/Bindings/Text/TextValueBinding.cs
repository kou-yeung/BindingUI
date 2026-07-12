using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class TextValueBinding<TState> : TextBinding<TState, string>
    {
        public TextValueBinding(Text target, Func<TState, string> getter) : base(target, getter)
        {
        }

        public override void Apply(TState state)
        {
            Target.text = Getter(state);
        }
    }
}
