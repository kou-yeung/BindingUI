using System;
using UnityEngine;

namespace BindingUI
{
    public static class BindingNodeViewExtensions
    {
        public static BindingNode<TState> View<TState, TViewData>(this BindingNode<TState> node, Func<TState, TViewData> getter)
        {
            var view = GetInterface<IRenderable<TViewData>>(node.GameObject);

            if (view == null)
            {
                throw new MissingComponentException(
                    $"{node.Id} does not implement IRenderable<{typeof(TViewData).Name}>");
            }

            node.Add(new ViewBinding<TState, TViewData>(view, getter));

            return node;
        }

        static TInterface GetInterface<TInterface>(GameObject gameObject) where TInterface : class
        {
            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
            {
                if (component is TInterface result)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
