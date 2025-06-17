using ExecutableToCppConverter;

internal class Program
{
    private static void Main(string[] args)
    {
        Start: { }

        Console.WriteLine("================ Executable to C++ Converter ================\n");

        while (true) 
        {
            Console.Write("> ");
            string[] inputVectors = Console.ReadLine().Trim().Split(' ');

            if (SystemNavigator.Navigate(inputVectors) == true)
            {
                goto Start;
            }
        }
    }
}
