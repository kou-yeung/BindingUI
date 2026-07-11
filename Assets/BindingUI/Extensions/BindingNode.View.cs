using BindingUI.Core;
using System;
using UnityEngine;

namespace BindingUI
{
    public sealed class ViewBinding<TState, TChildData> : IBinding<TState>
    {
        readonly IRenderable<TChildData> target;
        readonly Func<TState, TChildData> getter;

        public ViewBinding(IRenderable<TChildData> target, Func<TState, TChildData> getter)
        {
            this.target = target;
            this.getter = getter;

            if (target is IInitializable initializable)
            {
                initializable.Initialize();
            }
        }

        public void Apply(TState state)
        {
            target.Render(getter(state));
        }
    }
    public abstract class BindingView<TDisplayData> : MonoBehaviour, IInitializable, IRenderable<TDisplayData>, IBindingMarker
    {
        BindingRoot<TDisplayData> bindingRoot;
        bool initialized;

        GameObject IBindingMarker.GameObject => gameObject;

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            // GameObjectに IBindingNodeResolver アタッチされたら先に使う
            IBindingNodeResolver resolver = BindingUICore.GetInterface<IBindingNodeResolver>(gameObject);
            bindingRoot = new BindingRoot<TDisplayData>(resolver ?? new HierarchyBindingNodeResolver(gameObject));
            Build(bindingRoot);
            initialized = true;
        }

        public void Render(TDisplayData data)
        {
            Initialize();
            bindingRoot.Apply(data);
        }

        protected abstract void Build(BindingRoot<TDisplayData> root);
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> View<TViewData>(Func<TState, TViewData> getter)
        {

            var view = BindingUICore.GetInterface<IRenderable<TViewData>>(GameObject);

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
