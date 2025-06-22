namespace ExecutableToCppConverter.NeuralNetworkEngine;

public class Value
{
    public delegate void Backward();

    public double Data { get; set; }
    public double Grad { get; set; }
    public string Op { get; set; }
    public List<Value>? Prev { get; set; }
    public Backward BackwardFunction { get; set; }

    public Value(double data, List<Value>? children = null, string op = "")
    {
        Data = data;
        Grad = 0;
        Prev = children;
        Op = op; // Vestigial variable.

        BackwardFunction = () => {};
    }

    public static Value operator +(Value self, Value other) => PlusCommon(self, other);
    public static Value operator +(Value self, double other) => PlusCommon(self, new Value(other));
    public static Value operator +(double self, Value other) => PlusCommon(new Value(self), other);

    private static Value PlusCommon(Value self, Value other)
    {
        var output = new Value(self.Data + other.Data, new List<Value>(2) { self, other }, "+");

        output.BackwardFunction = () => 
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
        var output = new Value(self.Data * other.Data, new List<Value>(2) { self, other }, "*");

        output.BackwardFunction = () =>
        {
            self.Grad += other.Data * output.Grad;
            other.Grad += self.Data * output.Grad;
        };

        return output;
    }

    public static Value operator ^(Value self, double other)
    {
        var output = new Value(Math.Pow(self.Data, other), new List<Value>(1) { self }, "*");

        output.BackwardFunction = () =>
        {
            self.Grad += (other * Math.Pow(self.Data, other - 1)) * output.Grad;
        };

        return output;
    }

    public Value Relu()
    {
        Value output = new Value(Data < 0 ? 0 : Data, new List<Value>(1) { this }, "ReLU");

        BackwardFunction = () =>
        {
            Grad += output.Data > 0 ? output.Data * output.Grad : 0;
        };

        return output;
    }

    public void BackwardPropagate()
    {
        List<Value> grandList = new List<Value>();
        List<Value> visited = new List<Value>();

        // Builds a topological order all of the children in the graph.
        BuildTopologicalList(grandList, visited, this);

        // Go one variable at a time and apply the chain rule to get its gradient.
        Grad = 1;

        grandList.Reverse();

        foreach (var grand in grandList) 
        {
            grand.BackwardFunction();
        }
    }

    private static void BuildTopologicalList(List<Value> grandList, List<Value> visited, Value value)
    {
        if (value.Prev == null)
        {
            return;
        }

        if (!visited.Contains(value)) 
        {
            visited.Add(value);

            foreach (var child in value.Prev)
            {
                BuildTopologicalList(grandList, visited, child);
            }

            grandList.Add(value);
        }
    }
}
