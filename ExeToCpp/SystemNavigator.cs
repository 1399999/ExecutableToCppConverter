namespace ExecutableToCppConverter;

public static class SystemNavigator
{
    public static bool? Navigate(string[] inputVectors)
    {
        if (inputVectors[0] == "exe" || inputVectors[0] == "exec" || inputVectors[0] == "executable")
        {
            NavigateExeCommand(inputVectors);
        }

        else if (inputVectors[0] == "cpp")
        {
            NavigateCppCommand(inputVectors);
        }

        else if (inputVectors[0] == "start" || inputVectors[0] == "run")
        {
            NavigateStartCommand(inputVectors);
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
            Console.Clear(); return true;
        }

        else if (inputVectors[0] == "test")
        {
            Test.Run();
        }

        else if (inputVectors[0].Trim() != string.Empty)
        {
            SystemError.DisplayGeneralCommandError(inputVectors[0]);
        }

        return null;
    }

    private static void NavigateExeCommand(string[] inputVectors)
    {
        if (Arguments.CheckForNoArguments(inputVectors))
        {
            SystemError.DisplayNoArgumentError(inputVectors[0]);
        }

        else if (Arguments.CheckForImmediateFileArgument(inputVectors))
        {
            SystemModel.ExeFilePath = inputVectors[1];
            Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[1]}\"\n");
        }

        else if (Arguments.CheckForFileArgumentFlag(inputVectors))
        {
            SystemModel.ExeFilePath = inputVectors[1];
            Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[2]}\"\n");
        }

        else if (Arguments.CheckForIncorrect2ndArgument(inputVectors))
        {
            SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
        }
    }

    private static void NavigateCppCommand(string[] inputVectors)
    {
        if (Arguments.CheckForNoArguments(inputVectors))
        {
            SystemError.DisplayNoArgumentError(inputVectors[0]);
        }

        else if (Arguments.CheckForImmediateFileArgument(inputVectors))
        {
            SystemModel.CppFilePath = inputVectors[1];
            Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[1]}\"\n");
        }

        else if (Arguments.CheckForFileArgumentFlag(inputVectors))
        {
            SystemModel.CppFilePath = inputVectors[1];
            Console.WriteLine($"\nSuccesfully buffered file \"{inputVectors[2]}\"\n");
            SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
        }

        else if (Arguments.CheckForIncorrect2ndArgument(inputVectors))
        {
            SystemError.DisplayFileDoesNotExistError(inputVectors[1]);
        }
    }

    private static void NavigateStartCommand(string[] inputVectors)
    {
        if (Arguments.CheckForNoArguments(inputVectors))
        {
            if (!string.IsNullOrEmpty(SystemModel.ExeFilePath) && !string.IsNullOrEmpty(SystemModel.CppFilePath))
            {
                Parser.ParseIntoByteArrayAndDisplay(SystemModel.ExeFilePath);
                Console.WriteLine("<START>");
                Parser.ParseIntoByteArrayAndDisplay(SystemModel.CppFilePath);
            }

            if (string.IsNullOrEmpty(SystemModel.ExeFilePath))
            {
                SystemError.DisplayFileDoesNotExistError("<Executable File Path>");
            }

            if (string.IsNullOrEmpty(SystemModel.CppFilePath))
            {
                SystemError.DisplayFileDoesNotExistError("<C++ File Path>");
            }
        }

        else if (Arguments.CheckForTwoFiles(inputVectors))
        {
            Parser.ParseIntoByteArrayAndDisplay(inputVectors[1]);
            Console.WriteLine("<START>");
            Parser.ParseIntoByteArrayAndDisplay(inputVectors[2]);
        }

        else if (Arguments.CheckForHelp(inputVectors))
        {
            Information.DisplayStartHelp();
        }
    }
}
