namespace ExecutableToCppConverter;

public static class SystemModel
{
    public static string ExeFilePath { get; set; } = string.Empty;
    public static string CppFilePath { get; set; } = string.Empty;
    public static LogFile LogFileProperty { get; set; } = new LogFile();
    public static string DirectoryPath { get; set; } = "C:\\ExecutableToCpp\\Logs";
}
