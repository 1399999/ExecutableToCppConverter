using ExecutableToCppConverter;

internal class Program
{
    public static string ExeFilePath { get; set; }
    public static string CppFilePath { get; set; }

    private static void Main(string[] args)
    {
        Start: { }

        Console.WriteLine("================ Executable to C++ Converter ================\n");

        while (true) 
        {
            Console.Write("> ");
            string[] inputVectors = Console.ReadLine().Trim().Split(' ');

            if (File.Exists(inputVectors[0]))
            {
                SystemError.DisplayNoArgumentError(inputVectors[0]);
            }

            else if ((inputVectors[0] == "exe" || inputVectors[0] == "exec" || inputVectors[0] == "executable") &&
                Arguments.CheckForNoArguments(inputVectors))
            {
                SystemError.DisplayNoArgumentError(inputVectors[0]);
            }

            else if ((inputVectors[0] == "exe" || inputVectors[0] == "exec" || inputVectors[0] == "executable") &&
                Arguments.CheckForImmediateFileArgument(inputVectors))
            {
                ExeFilePath = inputVectors[1];
                Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[1]}\"\n");
            }

            else if ((inputVectors[0] == "exe" || inputVectors[0] == "exec" || inputVectors[0] == "executable") &&
                Arguments.CheckForFileArgumentFlag(inputVectors))
            {
                ExeFilePath = inputVectors[1];
                Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[2]}\"\n");
            }

            else if ((inputVectors[0] == "exe" || inputVectors[0] == "exec" || inputVectors[0] == "executable") &&
                Arguments.CheckForImmediateFileArgument(inputVectors))
            {
                CppFilePath = inputVectors[1];
                Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[1]}\"\n");
            }

            else if ((inputVectors[0] == "cpp") &&
                Arguments.CheckForNoArguments(inputVectors))
            {
                SystemError.DisplayNoArgumentError(inputVectors[0]);
            }

            else if ((inputVectors[0] == "cpp") &&
                Arguments.CheckForIncorrect2ndArgument(inputVectors))
            {
                SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
            }

            else if ((inputVectors[0] == "cpp") &&
                Arguments.CheckForFileArgumentFlag(inputVectors))
            {
                CppFilePath = inputVectors[1];
                Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[2]}\"\n");
                SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
            }

            else if ((inputVectors[0] == "cpp") &&
                Arguments.CheckForIncorrect2ndArgument(inputVectors))
            {
                SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
            }

            else if ((inputVectors[0] == "start" || inputVectors[0] == "run") &&
                Arguments.CheckForNoArguments(inputVectors))
            {
                if (ExeFilePath != string.Empty && CppFilePath != string.Empty)
                {
                    Parser.ParseIntoByteArrayAndDisplay(ExeFilePath);
                    Console.WriteLine("<START>");
                    Parser.ParseIntoByteArrayAndDisplay(CppFilePath);
                }

                else if (ExeFilePath == string.Empty)
                {
                    SystemError.DisplayFileDoesNotExistError("<Executive File Path>");
                }

                else if (ExeFilePath == string.Empty)
                {
                    SystemError.DisplayFileDoesNotExistError("<C++ File Path>");
                }
            }

            else if ((inputVectors[0] == "start" || inputVectors[0] == "run") &&
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

            else if (inputVectors[0] == "clear" || inputVectors[0] == "cls")
            {
                Console.Clear();
                goto Start;
            }

            else if (inputVectors[0].Trim() != string.Empty)
            {
                SystemError.DisplayGeneralCommandError(inputVectors[0]);
            }
        }
    }
}
