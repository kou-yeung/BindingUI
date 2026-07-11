using BindingUI;

public class ListItem : BindingView<int>
{
    protected override void Build(BindingRoot<int> root)
    {
        root.Bind("Label").Text(v=> v.ToString());
    }
}
