using System;
using System.Collections.Generic;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> List<TItem>(Func<TState, IReadOnlyList<TItem>> getter)
        {
            Add(new ListBinding<TState, TItem>(Get<BindingList>(), getter));
            return this;
        }
    }
}