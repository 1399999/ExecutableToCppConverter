namespace ExecutableToCppConverter.NeuralNetworkEngine;

public class Layer
{
    public List<Neuron> Neurons { get; set; } = new List<Neuron>();

    public Layer(int nin, int nout, bool nonlin)
    {
        for (int i = 0; i < nout; i++)
        {
            Neurons.Add(new Neuron(nin, nonlin));
        }
    }

    public List<Value> Call(List<Value> x)
    {
        List<Value> output = new List<Value>();

        foreach (Neuron neuron in Neurons)
        {
            output.Add(neuron.Call(x));
        }

        return output;

        //return output.Count == 1 ? output[0] : output;
    }

    /// <summary>
    /// WARNING: DO NOT THINK IT WORKS.
    /// </summary>
    /// <returns></returns>
    public List<Value> Paramaters()
    {
        List<Value> paramaters = new();

        foreach (var item in Neurons)
        {
            foreach (var p in item.Paramaters())
            {
                paramaters.Add(p);
            }
        }

        return paramaters;
    }

    public override string ToString()
    {
        string result = string.Empty;

        foreach (var item in Neurons)
        {
            result += item.ToString();
            result += ", ";
        }

        return $"Layer of {result}";
    }
}
