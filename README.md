# 宣言的UI と 命令的UI の中間的なもの
UIToolKit 似てる操作方法で 既存プレハブUI に適用する検証的実装

# 使い方

■ 操作対象に BindingId コンポーネントアタッチし名前を付与する

<img width="418" height="130" alt="image" src="https://github.com/user-attachments/assets/bda58f40-86c0-4000-83ea-a48a0fb87515" />

■ Start() にて BindingRoot のインスタンス生成

```c#
// 仮にこのような状態を定義される
class SampleState
{
    public bool Visible;
    public Color Color;
}

// メンバー変数を定義する
BindingRoot<SampleState> bindingRoot;

void Start()
{
    // 入れ子から BindingId をリストアップして BindingNode テーブル作成
    bindingRoot = new(gameObject);
}
```

■ Bind 対象取得し操作を書く

```c#
bindingRoot.Bind("ValueImage")   // "ValueImage" を検索し
    .ImageColor(v => v.Color)    // カラー設定
    .Visible(v => v.Visible);    // 可視状態設定
```

■ 状態を反映する

bindingRoot.Apply(...) を実行すれば、渡された状態を BindingNode に反映されます

■ 拡張性について

BindingNode<TState> と IBinding<TState> を作成すれば、操作を増やせます

例：可視状態の実装を記載する

```c#
// BindingNode に Visible という操作を追加
public sealed partial class BindingNode<TState>
{
    public BindingNode<TState> Visible(Func<TState, bool> getter)
    {
        Add(new VisibleBinding<TState>(GameObject, getter));
        return this;
    }
}

// 操作実行時の振る舞いを記述
public sealed class VisibleBinding<TState> : IBinding<TState>
{
    readonly GameObject target;
    readonly Func<TState, bool> getter;

    public VisibleBinding(GameObject target, Func<TState, bool> getter)
    {
        this.target = target;
        this.getter = getter;
    }
    public void Apply(TState state)
    {
        target.SetActive(getter(state));
    }
}
```

■ 実演

```c#
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
}

// スライダー変更イベント
private void OnValueChanged(float value)
{
    // Apply 実行すれば反映されます
    bindingRoot.Apply(new SampleState
    {
        ValueString = value.ToString(),
        Color = new Color(1, 1, 1, value / 10),
        Visible = value != 0,
        Interactable = value >= 5
    });
}
```

Start() に上記コードを書くだけで、状態を Apply すれば 複雑の分岐書く必要なくなります

https://github.com/user-attachments/assets/b1d7c8e9-01ba-4210-8674-1a5ad4508699
