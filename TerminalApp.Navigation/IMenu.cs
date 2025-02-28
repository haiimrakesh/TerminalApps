namespace TerminalApp.Navigation;

public interface IMenu
{
    IMenu ParentMenu { get; }
    string Header { get; }
    IEnumerable<Choice> Choices { get; }
    IMenu ProcessInput();
}
