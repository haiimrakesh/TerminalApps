
namespace TerminalApp.Navigation;

public class ExitMenu : IMenu
{
    public IMenu ParentMenu { get; init; }
    public string Header { get; }
    IEnumerable<Choice> IMenu.Choices => null!;
    public ExitMenu(string header)
    {
        Header = header;
        ParentMenu = null!;
    }

    public IMenu ProcessInput()
    {
        Environment.Exit(0);
        return null!;
    }
}