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
}
