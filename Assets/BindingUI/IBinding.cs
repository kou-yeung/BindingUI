using UnityEngine;

namespace BindingUI
{
    public interface IBinding<TState>
    {
        void Apply(TState state);
    }
}
