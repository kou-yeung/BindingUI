using System;
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
}