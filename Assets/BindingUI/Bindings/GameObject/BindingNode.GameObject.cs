using System;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Visible(Func<TState, bool> getter)
        {
            Add(new GameObjectVisibleBinding<TState>(GameObject, getter));
            return this;
        }
    }
}
