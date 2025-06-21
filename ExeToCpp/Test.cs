namespace ExecutableToCppConverter;

public static class Test
{
    private static double F(double x) => 3 * Math.Pow(x, 2D) - 4 * x + 5;
    private static double GetDerivative()
    {
        double h = 0.0000001D;
        double x = 3D;
        return (F(x + h) - F(x)) / h;
    }

    private static double GetComplexDerivative()
    {
        double h = 0.0000001D;

        double a = 2D;
        double b = -3D;
        double c = 10D;

        double d1 = a * b + c;
        a += h;

        double d2 = a * b + c;

        return (d2 - d1)/h;
    }

    public static void Run()
    {
        Console.WriteLine($"\nf(3) = {F(3)}");
        Console.WriteLine($"Derivitive of f(3) = {GetDerivative()}");
        Console.WriteLine($"Complex Derivitive of complex function = {GetComplexDerivative()}");

        //Console.WriteLine($"Value Class(2) = {new Value(2)}");
        //Console.WriteLine($"Value Class(-3) = {new Value(-3)}");
        //Console.WriteLine($"Value Class(-3) = {new Value(-3) * new Value(2) + new Value(10)}\n");

        var a = new Value(2, 'a');
        var b = new Value(-3, 'b');
        var c = new Value(10, 'c');
        var e = a * b; e.Label = 'e';
        var d = e + c; d.Label = 'd';
        var f = new Value(-2, 'f');
        var l = f * d; l.Label = 'l';
        var l1 = l.Data;

        double h = 0.0001;
        f.Grad = 4;
        d.Grad = -2;
        l.Grad = 1;
        c.Grad = -2;
        e.Grad = -2;

        a.Data += 0.01 * a.Grad;
        b.Data += 0.01 * b.Grad;
        c.Data += 0.01 * c.Grad;
        f.Data += 0.01 * f.Grad;

        e = a * b;
        d = e + c;
        l = f * d;

        Console.WriteLine($"l.Data: {l.Data}\n");

        a = new Value(2, 'a');
        b = new Value(-3, 'b');
        c = new Value(10, 'c');
        e = a * b; e.Label = 'e';
        d = e + c; d.Label = 'd';
        f = new Value(-2, 'f');
        l = f * d; l.Label = 'l';
        var l2 = l.Data;

        // Calculus: dz/dx = (dz/dy)*(dy/dx).

        Console.WriteLine($"Derivitive of a Value Class: {(l2-l1)/h}\n");
    }
}

public class Value
{
    public double Data { get; set; }
    public (Value, Value) Prev { get; set; }
    public char Op { get; set; }
    public char Label { get; set; }
    public double Grad { get; set; } = 0;

    public Value(double data) => Data = data;
    public Value(double data, char label)
    {
        Data = data;
        Label = label;
    }

    public Value(double data, (Value, Value) children, char op)
    {
        Data = data;
        Prev = children;
        Op = op;
    }

    public override string ToString() => Data.ToString();

    public static Value operator + (Value value1, Value value2) => new Value(value1.Data + value2.Data, (value1, value2), '+');
    public static Value operator * (Value value1, Value value2) => new Value(value1.Data * value2.Data, (value1, value2), '*');
}
