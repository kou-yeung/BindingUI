# BindingUI

> **Keep using Unity Prefabs. Make UI updates declarative.**

BindingUI is a lightweight declarative UI binding framework for Unity.

Unlike React-style frameworks, BindingUI **does not replace Unity's Prefab workflow**.

Instead, it lets you continue using:

- ✅ Prefabs
- ✅ Inspector
- ✅ Animator
- ✅ Layout Groups
- ✅ Existing uGUI Components

while writing **state-driven UI updates** in a declarative way.

---

## Why?

Traditional Unity UI often looks like this:

```csharp
if (state.Visible)
{
    image.enabled = true;
    button.interactable = true;
}
else
{
    image.enabled = false;
    button.interactable = false;
}

text.text = state.Name;
```

As UI grows, update code becomes full of imperative branches.

BindingUI lets you describe **how state is applied** instead.

```csharp
root.Bind("ValueImage")
    .Visible(v => v.Visible)
    .ImageColor(v => v.Color);

root.Bind("ValueText")
    .Text(v => v.ValueString);

root.Bind("Button")
    .Interactable(v => v.Interactable);
```

Updating the UI becomes simply:

```csharp
bindingRoot.Apply(state);
```

---

# Features

- Prefab-first design
- Declarative state binding
- Strongly typed
- No reflection during rendering
- Nested Views
- Dynamic List support
- Extensible Binding API
- Minimal Inspector setup (BindingId only)

---

# Install

Use UPM : https://github.com/kou-yeung/BindingUI.git?path=Assets/BindingUI

# Getting Started

## 1. Add BindingId

Attach **BindingId** to any GameObject you want to control.

<img width="418" height="130" alt="image" src="https://github.com/user-attachments/assets/bda58f40-86c0-4000-83ea-a48a0fb87515" />

If Id is empty, the GameObject name is used automatically.

---

## 2. Create BindingRoot

```csharp
BindingRoot<SampleState> bindingRoot;

void Start()
{
    bindingRoot = new(gameObject);
}
```

BindingRoot scans all child BindingIds and builds an internal lookup table.

---

## 3. Register Bindings

```csharp
bindingRoot.Bind("ValueImage")
    .ImageColor(v => v.Color)
    .Visible(v => v.Visible);

bindingRoot.Bind("ValueText")
    .Text(v => v.ValueString);

bindingRoot.Bind("Button")
    .Interactable(v => v.Interactable);
```

---

## 4. Apply State

```csharp
bindingRoot.Apply(state);
```

That's it.

---

# Demo

https://github.com/user-attachments/assets/b1d7c8e9-01ba-4210-8674-1a5ad4508699

---

# Dynamic Lists

BindingUI also supports dynamic collections.

https://github.com/user-attachments/assets/7001c67d-d4a4-4ffa-91b0-e77ca8e26e53

ScrollView requires no code.

Simply add:

- BindingId
- BindingList

<img width="405" height="168" alt="image" src="https://github.com/user-attachments/assets/cb3c1443-890c-418f-90c8-4b263783f41e" />

Then write:

```csharp
bindingRoot.Bind("ScrollView")
    .List(v => v);
```

Each item is implemented using `BindingView<T>`.

```csharp
public class ListItem : BindingView<int>
{
    protected override void Build(BindingRoot<int> root)
    {
        root.Bind("Label")
            .Text(v => v.ToString());
    }
}
```

The same approach works with your own data classes.

```csharp
BindingView<EquipmentDisplayData>
```

---

# Extending BindingUI

Creating a new binding only requires two classes.

```csharp
public partial class BindingNode<TState>
{
    public BindingNode<TState> Visible(Func<TState, bool> getter)
    {
        Add(new VisibleBinding<TState>(GameObject, getter));
        return this;
    }
}
```

```csharp
public class VisibleBinding<TState> : IBinding<TState>
{
    ...
}
```

This makes BindingUI easy to extend for your own components.

---

# Philosophy

BindingUI is **not** another React for Unity.

It embraces Unity's existing workflow.

- Layout → Unity Editor
- Animation → Animator
- Visuals → Prefabs
- State Updates → BindingUI

Keep what Unity already does well.

Only make UI updates declarative.

## BindingUI.Boost

To keep **BindingUI** simple and stable, experimental features and optional integrations are developed in **BindingUI.Boost**. [LINK](https://github.com/kou-yeung/BindingUI.Boost)

BindingUI.Boost is the home for:

- Experimental bindings
- Third-party integrations (e.g. DOTween, R3, Addressables)
- Editor tools
- Utility extensions

If you've created a useful binding or integration for another Unity package, we'd love to see your contribution.

**Pull Requests are welcome!**
