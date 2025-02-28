namespace TerminalApp.Forms;

public interface IForm<T> where T : class
{
    T Data { get; }
    void Display();
    void ReadInput();
    void ValidateInput();
    void DisplayError(string message);
    void DisplaySuccess(string message);
    void DisplayInfo(string message);
    void DisplayWarning(string message);
    void DisplayPrompt(string message);
}
