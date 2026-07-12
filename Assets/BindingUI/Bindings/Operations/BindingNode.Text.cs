using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Text(Func<TState, string> getter)
        {
            if (GameObject.TryGetComponent<TextMeshProUGUI>(out var textMeshProUGUI))
            {
                Add(new TMPTextValueBinding<TState>(textMeshProUGUI, getter));
                return this;
            }

            if (GameObject.TryGetComponent<Text>(out var text))
            {
                Add(new TextValueBinding<TState>(Get<Text>(), getter));
                return this;
            }

            throw new MissingComponentException($"BindingNode '{Id}' on GameObject '{GameObject.name}' " + $"does not have component 'Text' or 'TextMeshProUGUI'.");
        }
    }
}
