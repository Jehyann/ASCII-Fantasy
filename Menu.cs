using System;

public class Menu
{
    private string[] options;
    private int selectedIndex;

    public Menu(string[] menuOptions)
    {
        options = menuOptions;
        selectedIndex = 0;
    }

    public void Display()
    {
        Console.Clear();

        int menuWidth = 20;
        int leftPosition = Console.WindowWidth / 2 - menuWidth / 2;

        Console.SetCursorPosition(leftPosition, Console.CursorTop);
        Console.WriteLine("+------------------+");
        Console.SetCursorPosition(leftPosition, Console.CursorTop);
        Console.WriteLine("|    MAIN MENU     |");
        Console.SetCursorPosition(leftPosition, Console.CursorTop);
        Console.WriteLine("+------------------+");

        for (int i = 0; i < options.Length; i++)
        {
            Console.SetCursorPosition(leftPosition, Console.CursorTop);
            Console.Write("|");

            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" > ");
                Console.Write(options[i].PadRight(menuWidth - 5));
                Console.ResetColor();
            }
            else
            {
                Console.Write("   ");
                Console.Write(options[i].PadRight(menuWidth - 5));
            }

            Console.WriteLine("|");
        }

        Console.SetCursorPosition(leftPosition, Console.CursorTop);
        Console.WriteLine("+------------------+");

        Console.SetCursorPosition(leftPosition - 15, Console.CursorTop);
        Console.WriteLine("         />_________________________________");

        Console.SetCursorPosition(leftPosition - 15, Console.CursorTop);
        Console.WriteLine("[########[]_________________________________>");

        Console.SetCursorPosition(leftPosition - 15, Console.CursorTop);
        Console.WriteLine("         \\>");
    }

    public void MoveUp()
    {
        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
    }

    public void MoveDown()
    {
        selectedIndex = (selectedIndex + 1) % options.Length;
    }

    public string GetSelectedOption()
    {
        return options[selectedIndex];
    }
}