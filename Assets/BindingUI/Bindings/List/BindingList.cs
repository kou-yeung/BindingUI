using BindingUI.Helper;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed class BindingList : MonoBehaviour
    {
        [SerializeField]
        Transform content;

        [SerializeField]
        GameObject itemPrefab;

        public Transform Content => 
            content != null
                ? content
                : transform;

        public GameObject CreateItem()
        {
            if (itemPrefab == null)
            {
                throw new InvalidOperationException(
                    $"ItemPrefab is not assigned on ListView '{name}'.");
            }

            var instance = Instantiate(
                itemPrefab,
                Content,
                false);

            instance.SetActive(true);
            return instance;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (content == null)
            {
                var scrollRect = GetComponent<ScrollRect>();
                content = scrollRect?.content ?? transform;
            }
        }
#endif
    }

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

            var view = BindingUIHelper.GetInterface<IRenderable<TItem>>(instance);

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
}