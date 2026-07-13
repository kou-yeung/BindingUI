using System;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> AnchorMin(Func<TState, Vector2> getter)
        {
            Add(new RectTransformAnchorMinBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> AnchorMax(Func<TState, Vector2> getter)
        {
            Add(new RectTransformAnchorMaxBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> AnchoredPosition(Func<TState, Vector2> getter)
        {
            Add(new RectTransformAnchoredPositionBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> SizeDelta(Func<TState, Vector2> getter)
        {
            Add(new RectTransformSizeDeltaBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> Pivot(Func<TState, Vector2> getter)
        {
            Add(new RectTransformPivotBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> AnchoredPosition3D(Func<TState, Vector3> getter)
        {
            Add(new RectTransformAnchoredPosition3DBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> OffsetMin(Func<TState, Vector2> getter)
        {
            Add(new RectTransformOffsetMinBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
        public BindingNode<TState> OffsetMax(Func<TState, Vector2> getter)
        {
            Add(new RectTransformOffsetMaxBinding<TState>(Get<RectTransform>(), getter));
            return this;
        }
    }
}
