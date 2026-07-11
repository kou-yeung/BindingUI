using BindingUI;
using System.Linq;
using UnityEngine;

public class ListSample : MonoBehaviour
{
    BindingRoot<int[]> bindingRoot;

    void Start()
    {
        bindingRoot = new BindingRoot<int[]>(gameObject);
        bindingRoot.Bind("ScrollView").List(v => v);

        // 適用
        bindingRoot.Apply(Enumerable.Range(1, 100).ToArray());
    }
}

