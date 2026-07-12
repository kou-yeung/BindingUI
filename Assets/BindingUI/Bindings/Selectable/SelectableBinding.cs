using System;
using UnityEngine.UI;

namespace BindingUI
{
    public abstract class SelectableBinding<TState, TValue> : ComponentBinding<TState, Selectable, TValue>
    {
        protected SelectableBinding(Selectable target, Func<TState, TValue> getter) : base(target, getter)
        {
        }
    }
}
