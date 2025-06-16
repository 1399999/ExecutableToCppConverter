internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("================ Executable to C++ Converter ================\n");

        while (true) 
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (File.Exists(input))
            {
                Console.WriteLine();

                byte[] executableContents = File.ReadAllBytes(input);

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
    }
}
