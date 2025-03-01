namespace TerminalApp.Navigation;

public interface IForm<T> : IMenu
{
    bool IsCreateMode { get; }
    T Data { get; }
    Action<T>? CallBack { get; set; }
}
