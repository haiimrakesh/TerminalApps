
using System.Reflection;
namespace TerminalApp.Navigation;

public class Form<T> : IForm<T>
{
    public bool IsCreateMode { get; set; }
    public IMenu ParentMenu { get; private set; }
    public string Header { get; }
    public List<Choice> Choices { get; }
    IEnumerable<Choice> IMenu.Choices => Choices;
    public T Data { get; private set; }

    public Action<T>? CallBack { get; set; }

    public Form(string header, T data, bool isCreateMode, IMenu? parentMenu = null)
    {
        Header = header;
        Choices = new List<Choice>();
        Data = data;
        ParentMenu = parentMenu ?? new ExitMenu("Exit");
        IsCreateMode = isCreateMode;
    }
    public IMenu ProcessInput()
    {
        Console.Clear();
        if (Data == null)
        {
            Console.WriteLine("Data is null.");
            return ParentMenu;
        }

        int reAttempt = 0;
        while (reAttempt < 3)
        {
            Console.WriteLine(IsCreateMode ? "Create Mode." : "Edit Mode.");
            Console.WriteLine("================================");
            Console.Clear();
            readValues(Data);
            Console.Clear();
            Console.WriteLine("Updated data looks like this:");
            Console.WriteLine("================================");
            displayData(Data);
            Console.WriteLine("================================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1: Save Changes");
            Console.WriteLine("2: Reattempt");
            Console.WriteLine("3: Cancel");
            var saveChanges = Console.ReadLine();
            switch (saveChanges)
            {
                case "1":
                    try
                    {
                        CallBack?.Invoke(Data);
                        return ParentMenu;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return ParentMenu;
                    }
                case "2":
                    reAttempt++;
                    break;
                default:
                    return ParentMenu;
            }
        }

        Console.ReadKey();
        return ParentMenu;
    }

    private void readValues(object data)
    {
        data.GetType().GetProperties().ToList().ForEach(p =>
                    {
                        if (!p.PropertyType.IsValueType && p.PropertyType != typeof(string))
                        {
                            Object? obj = p.GetValue(data);
                            if (obj == null)
                            {
                                obj = Activator.CreateInstance(p.PropertyType);
                                p.SetValue(data, obj);
                            }
                            if (obj != null)
                                readValues(obj);
                            return;
                        }
                        int attempts = 0;
                        while (attempts < 3)
                        {
                            try
                            {
                                Console.Write($"Enter {p.Name}:");
                                if (!IsCreateMode)
                                {
                                    Console.Write($"{p.GetValue(data)} : ");
                                }
                                var value = Console.ReadLine();
                                if (string.IsNullOrEmpty(value))
                                {
                                    break;
                                }
                                p.SetValue(data, Convert.ChangeType(value, p.PropertyType));
                                break;
                            }
                            catch
                            {
                                attempts++;
                                Console.WriteLine("Invalid input. Please try again.");
                            }
                        }
                    });
    }

    private void displayData(object data)
    {
        data?.GetType().GetProperties().ToList().ForEach(p =>
        {
            if (!p.PropertyType.IsValueType && p.PropertyType != typeof(string))
            {
                Object? obj = p.GetValue(data);
                if (obj == null)
                {
                    obj = Activator.CreateInstance(p.PropertyType);
                    p.SetValue(data, obj);
                }
                if (obj != null)
                    displayData(obj);
                return;
            }
            Console.WriteLine($"{p.Name}: {p.GetValue(data)} ");
        });
    }
}