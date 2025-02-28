using TerminalApp.Navigation;

internal class Program
{

    private static void Main(string[] args)
    {
        Menu menu = new Menu("Welcome to the Terminal App!");
        Menu updateEmployeeMenu = new Menu("Update an employee", menu);
        updateEmployeeMenu.AddChoice("1", "Update name", () => { Console.WriteLine("Update name"); });
        updateEmployeeMenu.AddChoice("2", "Update email", () => { Console.WriteLine("Update email"); });
        updateEmployeeMenu.AddChoice("3", "Update phone number", () => { Console.WriteLine("Update phone number"); });
        updateEmployeeMenu.AddChoice("4", "Update address", () => { Console.WriteLine("Update address"); });
        updateEmployeeMenu.AddChoice("5", "Return to main menu", () => { });

        menu.AddChoice("1", "View all employees", () => { Console.WriteLine("View all employees"); });
        menu.AddChoice("2", "Add a new employee", () => { });
        menu.AddChoice("3", "Update an employee", updateEmployeeMenu);
        menu.AddChoice("4", "Delete an employee", () => { });
        menu.AddChoice("5", "Exit", () => Environment.Exit(0));

        IMenu nextMenu = menu.ProcessInput();
        while (true)
        {
            nextMenu = nextMenu.ProcessInput();
        }
    }
}