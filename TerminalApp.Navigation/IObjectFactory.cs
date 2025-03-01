namespace TerminalApp.Navigation
{
    public interface IObjectFactory
    {
        T GetNewInstance<T>();
    }
}