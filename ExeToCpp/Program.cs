using ExecutableToCppConverter;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("================ Executable to C++ Converter ================\n");

        while (true) 
        {
            Console.Write("> ");
            string[] inputVectors = Console.ReadLine().Trim().Split(' ');

            if (File.Exists(inputVectors[0]))
            {
                SystemError.DisplayNoArgumentError(inputVectors[0]);
            }

            else if ((inputVectors[0] == "file" || inputVectors[0] == "run" || inputVectors[0] == "start") &&
                Arguments.CheckForNoArguments(inputVectors))
            {
                SystemError.DisplayNoArgumentError(inputVectors[0]);
            }

            else if ((inputVectors[0] == "file" || inputVectors[0] == "run" || inputVectors[0] == "start") &&
                Arguments.CheckForImmediateFileArgument(inputVectors))
            {
                Parser.ParseIntoByteArrayAndDisplay(inputVectors[1]);
            }

            else if ((inputVectors[0] == "file" || inputVectors[0] == "run" || inputVectors[0] == "start") &&
                Arguments.CheckForImmediateFileArgument(inputVectors))
            {
                Parser.ParseIntoByteArrayAndDisplay(inputVectors[1]);
            }

            else if ((inputVectors[0] == "file" || inputVectors[0] == "run" || inputVectors[0] == "start") && 
                Arguments.CheckForFileArgumentFlag(inputVectors) && File.Exists(inputVectors[2]))
            {
                Parser.ParseIntoByteArrayAndDisplay(inputVectors[2]);
            }

            else if ((inputVectors[0] == "file" || inputVectors[0] == "run" || inputVectors[0] == "start") &&
                Arguments.CheckForHelp(inputVectors))
            {
                Information.DisplayStartHelp();
            }

            else if (inputVectors[0] == "help")
            {
                Information.DisplayGeneralHelp();
            }

            else if (inputVectors[0] == "credit" || inputVectors[0] == "credits")
            {
                Information.DisplayCredits();
            }

            else if (inputVectors[0].Trim() != string.Empty)
            {
                SystemError.DisplayGeneralCommandError(inputVectors[0]);
            }
        }
    }
}
