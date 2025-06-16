namespace ExeToCpp;

public class Parser
{
    public static byte[] ParseIntoByteArray(string filePath) => File.ReadAllBytes(filePath);

    public static void ParseIntoByteArrayAndDisplay(string filePath)
    {
        Console.WriteLine();

        byte[] executableContents = ParseIntoByteArray(filePath);

        for (int i = 0; i < executableContents.Length; i++)
        {
            Console.Write($"{executableContents[i]} ");

            if (i % 16 == 15)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
    }
}
