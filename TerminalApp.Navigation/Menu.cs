
namespace TerminalApp.Navigation;

public class Menu : IMenu
{
    public IMenu ParentMenu { get; private set; }
    public string Header { get; }
    public List<Choice> Choices { get; }
    IEnumerable<Choice> IMenu.Choices => Choices;
    public Menu(string header, IMenu? parentMenu = null)
    {
        Header = header;
        Choices = new List<Choice>();
        ParentMenu = parentMenu ?? new ExitMenu("Exit");
    }

    public void AddChoice(string key, string description, IMenu NextMenu)
    {
        Choices.Add(new Choice(key, description, NextMenu, null));
    }
    public void AddChoice(string key, string description, Action action)
    {
        Choices.Add(new Choice(key, description, null, action));
    }

    public IMenu ProcessInput()
    {
        Console.Clear();
        Console.WriteLine(Header);
        foreach (var choice in Choices)
        {
            Console.WriteLine($"{choice.Key}: {choice.Description}");
        }

        int retryAttempts = 3;
        Choice? inputChoice;
        while (true)
        {
            var input = Console.ReadLine();
            inputChoice = Choices.FirstOrDefault(c => c.Key == input);
            if (inputChoice == null)
            {
                retryAttempts--;
                Console.WriteLine("Invalid choice. Please try again.");
                if (retryAttempts == 0)
                {
                    Console.WriteLine("You have exceeded the maximum number of attempts.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    break;
                }
                continue;
            }

            break;
        }

        if (inputChoice != null)
        {
            if (inputChoice.NextMenu != null)
                return inputChoice.NextMenu;
            else if (inputChoice.Action != null)
                inputChoice.Action();
        }
        return ParentMenu;
    }
}