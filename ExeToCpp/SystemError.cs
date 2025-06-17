namespace ExecutableToCppConverter;

public class SystemError
{
    public static void DisplayGeneralCommandError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nERROR: ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"The command is invalid. ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nUse the \"help\" command for help.\n");
    }

    public static void DisplayNoArgumentError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nERROR: ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"This command does not have necessary arguments required to run it.");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nUse the \"help\" command for help.\n");
    }

    public static void DisplayFileDoesNotExistError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nERROR: ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"This file does not exist.");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nUse the \"help\" command for help.\n");
    }
}
