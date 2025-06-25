using ExecutableToCppConverter.NeuralNetworkEngine;

namespace ExecutableToCppConverter;

public static class Test
{
    public static void RunTest1()
    {
        Value x = new Value(-4);
        Value z = 2 * x + 2 + x;
        Value q = z.Relu() + z * x;
        Value h = (z * z).Relu();
        Value y = h + q + q * x;

        Console.WriteLine($"Before Backward Propagation\n");

        Console.WriteLine($"x: {x}");
        Console.WriteLine($"z: {z}");
        Console.WriteLine($"q: {q}");
        Console.WriteLine($"h: {h}");
        Console.WriteLine($"y: {y}\n");

        y.BackwardPropagate();
        Console.WriteLine($"After Backward Propagation\n");

        Console.WriteLine($"x: {x}");
        Console.WriteLine($"z: {z}");
        Console.WriteLine($"q: {q}");
        Console.WriteLine($"h: {h}");
        Console.WriteLine($"y: {y}\n");
    }
    public static void RunTest2(string? filePath = null)
    {
        string[] lines = File.ReadAllLines(filePath != null ? filePath : "C:\\ExecutableToCpp\\Test\\Names.txt");

        Console.WriteLine($"\nLines: {lines.Length}");
        Console.WriteLine($"Min length word: {lines.OrderBy(x => x.Length).ToList()[0]}");
        Console.WriteLine($"Max length word: {lines.OrderByDescending(x => x.Length).ToList()[0]}\n");

        foreach (string name in lines) 
        {

        }
    }
}
