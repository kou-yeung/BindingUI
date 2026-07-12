using UnityEngine;

namespace BindingUI.Helper
{
    public static class BindingUIHelper
    {
        public static TInterface GetInterface<TInterface>(GameObject gameObject) where TInterface : class
        {
            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
            {
                if (component is TInterface result)
                {
                    return result;
                }
            }
            return null;
        }

        public static TInterface GetInterfaceInParents<TInterface>(GameObject gameObject) where TInterface : class
        {
            var current = gameObject.transform.parent;

            while(current != null)
            {
                var marker = GetInterface<TInterface>(current.gameObject);
                if (marker != null)
                {
                    return marker;
                }
                current = current.parent;
            }
            return null;
        }
    }
}
