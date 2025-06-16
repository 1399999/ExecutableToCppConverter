namespace ExeToCpp;

public class SystemError
{
    public static void DisplayGeneralCommandError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nERROR: ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(message);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"The command is invalid. \nUse the \"help\" command for help.");

        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void DisplayNoArgumentError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nERROR: ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"This command does not have necessary arguments required to run it. \nUse the \"help\" command for help.\n");

        Console.ForegroundColor = ConsoleColor.White;
    }
}
