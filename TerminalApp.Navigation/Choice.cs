namespace TerminalApp.Navigation
{
    public record class Choice(string Key, string Description, IMenu? NextMenu, Action? Action);
}