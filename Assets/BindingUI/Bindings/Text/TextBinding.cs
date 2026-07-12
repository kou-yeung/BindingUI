using System;
using TMPro;

namespace BindingUI
{
    public abstract class TMPTextBinding<TState, TValue> : ComponentBinding<TState, TextMeshProUGUI, TValue>
    {
        public TMPTextBinding(TextMeshProUGUI target, Func<TState, TValue> getter) : base(target, getter)
        {
           
        }
    }
}
