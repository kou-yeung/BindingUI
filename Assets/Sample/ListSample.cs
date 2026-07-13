using BindingUI;
using System.Linq;
using UnityEngine;

public class ListSample : MonoBehaviour
{
    BindingRoot<int[]> root;

    void Start()
    {
        root = new BindingRoot<int[]>(gameObject);
        root.Bind("ScrollView").List(v => v);

        // 適用
        root.Apply(Enumerable.Range(1, 100).ToArray());
    }

    public void OnClick()
    {
        var count = UnityEngine.Random.Range(25, 50);
        root.Apply(Enumerable.Range(1, count).ToArray());
    }
}

