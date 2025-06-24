namespace ExecutableToCppConverter.NeuralNetworkEngine;

public class MLP
{
    public List<Layer> Layers { get; set; } = new List<Layer>();

    public MLP(int nin, List<int> nouts)
    {
        var sz = nouts.Append(nin).ToList();

        for (int i = 0; i < nouts.Count; i++)
        {
            var inputSize = sz[i];
            var outputSize = sz[i + 1];
            var applyNonlin = i != nouts.Count - 1;

            Layers.Add(new Layer(inputSize, outputSize, applyNonlin));
        }
    }

    public List<Value> Call(List<Value> x)
    {
        foreach (var layer in Layers)
        {
            x = layer.Call(x);
        }

        return x;
    }

    public List<Value> Paramaters()
    {
        List<Value> paramaters = new();

        foreach (var layer in Layers)
        {
            foreach (var p in layer.Paramaters())
            {
                paramaters.Add(p);
            }
        }

        return paramaters;
    }

    public override string ToString()
    {
        string output = string.Empty;

        foreach (var layer in Layers)
        {
            output += layer.ToString();
            output += ", ";
        }

        return $"MLP of " + output;
    }
}
