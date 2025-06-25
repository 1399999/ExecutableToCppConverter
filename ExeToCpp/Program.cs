using ExecutableToCppConverter;

internal class Program
{
    private static void Main(string[] args)
    {
        Start: { }

        Console.WriteLine("================ Executable to C++ Converter - Test ================\n");

        while (true) 
        {
            Console.Write("> ");

            string? tempInput = Console.ReadLine();

            if (tempInput == null)
            {
                SystemError.DisplayMysteryError();
                continue;
            }

            string[] inputVectors = tempInput.Trim().Split(' ');

            if (SystemNavigator.Navigate(inputVectors) == true)
            {
                goto Start;
            }
        }
    }
}
