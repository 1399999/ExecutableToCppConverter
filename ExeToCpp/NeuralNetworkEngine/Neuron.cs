namespace ExecutableToCppConverter.NeuralNetworkEngine;

public class Neuron
{
    public List<Value> W { get; set; } = new List<Value>();
    public Value B { get; set; }
    public bool Nonlin { get; set; }

    public Neuron(int nin, bool nonlin = true)
    {
        Random random = new Random();

        for (int i = 0; i < nin; i++)
        {
            W.Add(new Value(random.Next(-1, 1)));
        }

        B = new Value(0);

        Nonlin = nonlin;
    }

    public Value Call(List<Value> x)
    {
        Value act = W[0] * x[0];

        for (int i = 1; i < x.Count; i++)
        {
            act += (W[i] * x[i]);
        }

        act += B;

        return Nonlin ? act.Relu() : act;
    }

    public List<Value> Paramaters() => W.Append(B).ToList();

    public override string ToString()
    {
        return $"{(Nonlin ? "ReLU" : "Linear")} Neuron({W.Count})";
    }
}
