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

        var a = new Value(2, label: "a");
        var b = new Value(-3, label: "b");
        var c = new Value(10, label: "c");
        var e = a * b; e.Label ="e";
        var d = e + c; d.Label = "d";
        var f = new Value(-2, label: "f");
        var l = f * d; l.Label = "l";
        var l1 = l.Data;

        double h = 0.0001;
        //f.Grad = 4;
        //d.Grad = -2;
        //l.Grad = 1;
        //c.Grad = -2;
        //e.Grad = -2;

        a.Data += 0.01 * a.Grad;
        b.Data += 0.01 * b.Grad;
        c.Data += 0.01 * c.Grad;
        f.Data += 0.01 * f.Grad;

        e = a * b;
        d = e + c;
        l = f * d;

        Console.WriteLine($"l.Data: {l.Data}");

        a = new Value(2, label: "a");
        b = new Value(-3, label: "b");
        c = new Value(10, label: "c");
        e = a * b; e.Label = "e";
        d = e + c; d.Label = "d";
        f = new Value(-2, label: "f");
        l = f * d; l.Label = "l";
        var l2 = l.Data;

        // Calculus: dz/dx = (dz/dy)*(dy/dx).

        Console.WriteLine($"Derivitive of a Value Class: {(l2-l1)/h}");

        // ####################
        // ## NEURON EXAMPLE ##
        // ####################

        // Inputs: x1, x2.
        Value x1 = new Value(2, label: "x1");
        Value x2 = new Value(0, label: "x2");

        // Weights: w1, w2.
        Value w1 = new Value(-3, label: "w1");
        Value w2 = new Value(1, label: "w2");

        // Bias: b
        b = new Value(6.8813735870195432, label: "b");

        // For all of the below: x1w1 + x2w2 + b.
        Value x1w1 = x1 * w1; x1w1.Label = "x1*w1";
        Value x2w2 = x2 * w2; x2w2.Label = "x2*w2";

        Value x1w1x2w2 = x1w1 + x2w2; x1w1x2w2.Label = "x1*w1 + x2*w2";
        Value n = x1w1x2w2 + b; n.Label = "n";

        Value o = n.TanH(); o.Label = "o";

        Console.WriteLine($"Output of tan(h): {o}\n");

        //o.BackwardTan();

        //x1w1.Grad = 0.5;
        //x2w2.Grad = 0.5;
        //x1w1x2w2.Grad = 0.5;
        //b.Grad = 0.5;
        //n.Grad = 0.5;
        //o.Grad = 1;

        //x1.Grad = w1.Grad * x1w1.Grad;
        //w1.Grad = x1.Grad * x1w1.Grad;

        //w2.Grad = w2.Data * x2w2.Grad;
        //x2.Grad = x2.Data * x2w2.Grad;
    }
}

public class Value
{
    public delegate void BackwardFunction(Value value1, Value value2, Value value3);
    public delegate void BackwardTanHFunction(Value self, Value output, int t);

    public double Data { get; set; }
    public (Value, Value?) Prev { get; set; }
    public char Op { get; set; }
    public string Label { get; set; } = string.Empty;
    public double Grad { get; set; } = 0;
    public BackwardFunction? BackwardOperation { get; set; }
    public BackwardTanHFunction? BackwardTan { get; set; }

    public Value(double data, (Value, Value?) children, char op = char.MinValue, string label = "")
    {
        Data = data;
        Prev = children;
        Label = label;
        Op = op;
        BackwardOperation = null;
        BackwardTan = null;
    }

    public Value(double data, char op = char.MinValue, string label = "")
    {
        Data = data;
        Label = label;
        Op = op;
        BackwardOperation = null;
        BackwardTan = null;
    }

    public override string ToString() => Data.ToString();

    public static Value operator + (Value value1, Value value2) 
    {
        var output =  new Value(value1.Data + value2.Data, (value1, value2), '+');
        output.BackwardOperation = BackwardAdd;

        return output;
    }

    public static Value operator * (Value value1, Value value2) 
    {
        var output = new Value(value1.Data * value2.Data, (value1, value2), '*');
        output.BackwardOperation = BackwardMul;

        return output;
    }

    public Value TanH()
    {
        double n = Data;
        var t = (Math.Exp(2 * n) - 1) / (Math.Exp(2 * n) + 1);

        BackwardTan = BackwardTanh;

        return new Value(t, (this, null), label: "tanh");
    }

    static void BackwardAdd(Value self, Value other, Value output)
    {
        self.Grad = 1.0 * output.Grad;
        other.Grad = 1.0 * output.Grad;
    }

    static void BackwardMul(Value self, Value other, Value output)
    {
        self.Grad = 1.0 * other.Data * output.Grad;
        other.Grad = 1.0 * self.Data * output.Grad;
    }

    static void BackwardTanh(Value self, Value output, int t)
    {
        self.Grad = (1 - Math.Pow(t, 2)) * output.Grad;
    }
}
