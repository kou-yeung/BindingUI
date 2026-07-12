using BindingUI.Helper;
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
            IBindingNodeResolver resolver = BindingUIHelper.GetInterface<IBindingNodeResolver>(gameObject);
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
}
