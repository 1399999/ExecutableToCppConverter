namespace ExecutableToCppConverter.NeuralNetworkEngine;

public class Value
{
    public delegate void Backward();

    public double Data { get; set; }
    public double Grad { get; set; }
    public string Op { get; set; }
    public (Value, Value)? Prev { get; set; }
    public static Backward? BackwardFunction { get; set; }

    public Value(double data, (Value, Value)? children = null, string op = "")
    {
        Data = data;
        Grad = 0;
        Prev = children;
        Op = op; // Vestigial variable.
    }

    public static Value operator +(Value self, Value other) => PlusCommon(self, other);
    public static Value operator +(Value self, double other) => PlusCommon(self, new Value(other));
    public static Value operator +(double self, Value other) => PlusCommon(new Value(self), other);

    private static Value PlusCommon(Value self, Value other)
    {
        var output = new Value(self.Data + other.Data, (self, other), "+");

        BackwardFunction = () => 
        {
            self.Grad += output.Grad;
            other.Grad += output.Grad;
        };

        return output;
    }

    public static Value operator *(Value self, Value other) => MulCommon(self, other);
    public static Value operator *(Value self, double other) => MulCommon(self, new Value(other));
    public static Value operator *(double self, Value other) => MulCommon(new Value(self), other);

    private static Value MulCommon(Value self, Value other)
    {
        var output = new Value(self.Data * other.Data, (self, other), "*");

        BackwardFunction = () =>
        {
            self.Grad += other.Data * output.Grad;
            other.Grad += self.Data * output.Grad;
        };

        return output;
    }

    public static Value operator ^(Value self, double other)
    {
        var output = new Value(Math.Pow(self.Data, other), (self, new Value(int.MinValue)), "*");

        BackwardFunction = () =>
        {
            self.Grad += (other * Math.Pow(self.Data, other - 1)) * output.Grad;
        };

        return output;
    }
}
