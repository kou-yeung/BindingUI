using BindingUI;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    class SampleState
    {
        public string ValueString;
        public bool Interactable;
        public bool Visible;
        public Color Color;
    }

    BindingRoot<SampleState> bindingRoot;

    void Start()
    {
        bindingRoot = new(gameObject);

        bindingRoot.Bind("ValueImage")
            .ImageColor(v => v.Color)
            .Visible(v => v.Visible);
            
        bindingRoot.Bind("ValueText")
            .Text(v => v.ValueString);

        bindingRoot.Bind("BTN")
            .Interactable(v => v.Interactable);

        var slider = bindingRoot.Bind("Slider").Get<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        slider.value = 5;
    }
    private void OnValueChanged(float value)
    {
        bindingRoot.Apply(new SampleState
        {
            ValueString = value.ToString(),
            Color = new Color(1, 1, 1, value / 10),
            Visible = value != 0,
            Interactable = value >= 5
        });
    }
}
