using System;
using UnityEngine.UI;

namespace BindingUI
{
    public abstract class TextBinding<TState, TValue> : ComponentBinding<TState, Text, TValue>
    {
        public TextBinding(Text target, Func<TState, TValue> getter) : base(target, getter)
        {
           
        }
    }
}
