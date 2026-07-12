using BindingUI.Helper;
using System;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> View<TViewData>(Func<TState, TViewData> getter)
        {
            var view = BindingUIHelper.GetInterface<IRenderable<TViewData>>(GameObject);

            if (view == null)
            {
                throw new MissingComponentException(
                    $"{Id} does not implement IRenderable<{typeof(TViewData).Name}>");
            }

            Add(new ViewBinding<TState, TViewData>(view, getter));

            return this;
        }
    }
}
