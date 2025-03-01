using TerminalApp.Navigation;

internal class Program
{

    private static void Main(string[] args)
    {
        Menu menu = new Menu("Welcome to the Terminal App!");
        IForm<Employee> updateEmployeeMenu = new Form<Employee>("Update Employee", new Employee(), false, menu);
        updateEmployeeMenu.CallBack = (employee) =>
        {
            Console.WriteLine("Employee updated successfully.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        };

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

public class Employee
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
    public string Address { get; set; } = string.Empty;
    public OfficialInfo OfficialInfo { get; set; } = new OfficialInfo();
}

public class OfficialInfo
{
    public int EmpId { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
}