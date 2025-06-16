namespace ExecutableToCppConverter;

public class Arguments
{
    public static bool CheckForNoArguments(string[] commandVectors) => commandVectors.Length == 1;
    public static bool CheckForHelp(string[] commandVectors) => commandVectors.Length <= 1 ? false : commandVectors[1] == "help" || commandVectors[1] == "--help";
    public static bool CheckForImmediateFileArgument(string[] commandVectors) => commandVectors.Length <= 1 ? false : File.Exists(commandVectors[1]);
    public static bool CheckForFileArgumentFlag(string[] commandVectors) => commandVectors.Length <= 1 ? false : commandVectors[1] == "-f" || commandVectors[1] == "--file";
}
