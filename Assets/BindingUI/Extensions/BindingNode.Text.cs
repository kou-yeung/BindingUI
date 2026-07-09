using System;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class TextBinding<TState> : IBinding<TState>
    {
        readonly Text target;
        readonly Func<TState, string> getter;

        public TextBinding(Text target, Func<TState, string> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            target.text = getter(state);
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Text(Func<TState, string> getter)
        {
            Add(new TextBinding<TState>(Get<Text>(), getter));
            return this;
        }
    }
}
