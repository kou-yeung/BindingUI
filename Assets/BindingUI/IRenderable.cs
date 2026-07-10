namespace BindingUI
{
    public interface IRenderable<in T>
    {
        void Render(T data);
    }
}