using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AltMenu
{
    private string[] options;
    private int selectedIndex;

    public AltMenu(string[] _options)
    {
        options = _options;
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
        Console.WriteLine("|   ALT MENU       |");
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