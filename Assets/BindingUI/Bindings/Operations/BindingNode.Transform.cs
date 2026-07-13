using System;
using UnityEngine;

namespace BindingUI
{
    public sealed partial class BindingNode<TState>
    {
        public BindingNode<TState> Position(Func<TState, Vector3> getter)
        {
            Add(new TransformPositionBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> LocalPosition(Func<TState, Vector3> getter)
        {
            Add(new TransformLocalPositionBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> EulerAngles(Func<TState, Vector3> getter)
        {
            Add(new TransformEulerAnglesBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> LocalEulerAngles(Func<TState, Vector3> getter)
        {
            Add(new TransformLocalEulerAnglesBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> Rotation(Func<TState, Quaternion> getter)
        {
            Add(new TransformRotationBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> LocalRotation(Func<TState, Quaternion> getter)
        {
            Add(new TransformLocalRotationBinding<TState>(Get<Transform>(), getter));
            return this;
        }
        public BindingNode<TState> LocalScale(Func<TState, Vector3> getter)
        {
            Add(new TransformLocalScaleBinding<TState>(Get<Transform>(), getter));
            return this;
        }
    }
}
