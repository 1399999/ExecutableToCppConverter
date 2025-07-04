namespace ExecutableToCppConverter;

public class LogFile
{
    public string TestNumber { get; set; } = string.Empty;
    public Dictionary<string, DateTime> Times { get; set; } = new Dictionary<string, DateTime>();
}
