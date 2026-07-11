using UnityEngine;

namespace BindingUI
{
    public interface IBindingNodeResolver
    {
        GameObject GameObject { get; }
        bool TryResolve(string bindingId, out BindingId target);
    }
}
