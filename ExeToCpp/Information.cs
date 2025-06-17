namespace ExecutableToCppConverter;
public static class Information
{
    public static void DisplayGeneralHelp() => Console.WriteLine
    (
        "\nCommands:\n\n" +
        "clear: Clears the console.\n" +
        "credits: Displays the credits screen.\n" +
        "exe [-f] [--help]: Buffers the executable file.\n" +
        "cpp [-f] [--help]: Buffers the C++ file.\n" +
        "start [--help]: Buffers the C++ file.\n" +
        "help: Displays this menu.\n" +
        "\nArguments:\n\n" +
        "-f: Specify the file the you want to input.\n" +
        "--help: Displays the help menu for a general command.\n"
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
