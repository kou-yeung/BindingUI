using BindingUI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BindingUI
{
    public sealed class ListBinding<TState, TItem> : IBinding<TState>
    {
        readonly BindingList target;
        readonly Func<TState, IReadOnlyList<TItem>> getter;

        readonly List<ItemHandle> items = new();

        public ListBinding(BindingList target, Func<TState, IReadOnlyList<TItem>> getter)
        {
            this.target = target;
            this.getter = getter;
        }

        public void Apply(TState state)
        {
            var values = getter(state);

            if (values == null)
            {
                HideItemsFrom(0);
                return;
            }

            EnsureItemCount(values.Count);

            for (var i = 0; i < values.Count; i++)
            {
                var item = items[i];

                item.SetActive(true);
                item.Render(values[i]);
            }

            HideItemsFrom(values.Count);
        }

        void EnsureItemCount(int requiredCount)
        {
            while (items.Count < requiredCount)
            {
                items.Add(CreateItem());
            }
        }

        ItemHandle CreateItem()
        {
            var instance = target.CreateItem();

            var view = BindingUICore.GetInterface<IRenderable<TItem>>(instance);

            if (view is IInitializable initializable)
            {
                initializable.Initialize();
            }

            return new ItemHandle(instance, view);
        }

        void HideItemsFrom(int startIndex)
        {
            for (var i = startIndex; i < items.Count; i++)
            {
                items[i].SetActive(false);
            }
        }

        sealed class ItemHandle
        {
            readonly GameObject root;
            readonly IRenderable<TItem> view;

            public ItemHandle(GameObject root, IRenderable<TItem> view)
            {
                this.root = root;
                this.view = view;
            }

            public void Render(TItem data)
            {
                view.Render(data);
            }

            public void SetActive(bool active)
            {
                if (root.activeSelf != active)
                {
                    root.SetActive(active);
                }
            }
        }
    }

    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> List<TItem>(Func<TState, IReadOnlyList<TItem>> getter)
        {
            Add(new ListBinding<TState, TItem>(Get<BindingList>(), getter));
            return this;
        }
    }
}