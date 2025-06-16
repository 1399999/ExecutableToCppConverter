namespace ExecutableToCppConverter;
public static class Information
{
    public static void DisplayGeneralHelp() => Console.WriteLine
    (
        "\nCommands:\n\n" +
        "credits: Displays the credits screen.\n" +
        "start [--help]: Runs the program.\n" +
        "help: Displays this menu.\n"
    );

    public static void DisplayCredits() => Console.WriteLine
    (
        "\nCopyright (C) Mikhail Zhebrunov 2020-2025.\n"
    );

    public static void DisplayStartHelp() => Console.WriteLine
    (
        "\nCurrently only displays the hex output of the file.\n"
    );
}
