using UnityEngine;

namespace BindingUI
{
    public sealed class BindingId : MonoBehaviour
    {
        [SerializeField] string id;
        public string Id => id;
    }
}
