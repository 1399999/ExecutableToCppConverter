﻿using System.Text.Json;
using ExecutableToCppConverter.NeuralNetworkEngine;
using TorchSharp;
using static TorchSharp.torch;

namespace ExecutableToCppConverter;

public static class Test
{
    private static readonly string STARTING_TOKEN = "<S>";
    private static readonly string ENDING_TOKEN = "<E>";
    private static readonly char SPLIT_TOKEN = '.';
    private static readonly int TEMP_WORDS_USED = 32033;

    private static readonly char[] ALPHABET = new char[27]
    {
        SPLIT_TOKEN, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
    };

    public static void RunTest1()   
    {
        Value x = new Value(-4);
        Value z = 2 * x + 2 + x;
        Value q = z.Relu() + z * x;
        Value h = (z * z).Relu();
        Value y = h + q + q * x;

        Console.WriteLine($"\nBefore Backward Propagation\n");

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

        string[] tempLines = new string[TEMP_WORDS_USED];

        for (int i = 0; i < TEMP_WORDS_USED; i++)
        {
            tempLines[i] = lines[i];
        }

        List<string> tempTokenMap = new List<string>();
        List<string> tokenMap = new List<string>();

        foreach (string name in tempLines)
        {
            tempTokenMap.Add(STARTING_TOKEN);

            for (int i = 0; i < name.Length; i++)
            {
                tempTokenMap.Add(name[i].ToString());
            }

            tempTokenMap.Add(ENDING_TOKEN);
        }

        for (int i = 0; i < tempTokenMap.Count - 1; i++)
        {
            if (tempTokenMap[i] != ENDING_TOKEN)
            {
                tokenMap.Add($"{tempTokenMap[i]} {tempTokenMap[i + 1]}");
            }
        }

        Dictionary<string, int> tokenFrequencyDictionary = new();

        for (int i = 0; i < tokenMap.Count; i++)
        {
            if (!tokenFrequencyDictionary.ContainsKey(tokenMap[i]))
            {
                tokenFrequencyDictionary.Add(tokenMap[i], 1);
            }

            else
            {
                tokenFrequencyDictionary[tokenMap[i]]++;
            }
        }

        for (int i = 0; i < tokenFrequencyDictionary.Count; i++)
        {
            Console.WriteLine($"{{{tokenFrequencyDictionary.Keys.ToList()[i]}}}: {tokenFrequencyDictionary.Values.ToList()[i]}");
        }

        Console.WriteLine("\n-------------------------------------------------------");

        for (int i = 0; i < tokenFrequencyDictionary.Count; i++)
        {
            if (tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[0] != STARTING_TOKEN && tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[0] != ENDING_TOKEN)
            {
                Console.Write(tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[0]);
            }

            else
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine("\n\n-------------------------------------------------------\n");

        for (int i = 0; i < tokenFrequencyDictionary.Count; i++)
        {
            if (tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[1] != STARTING_TOKEN && tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[1] != ENDING_TOKEN)
            {
                Console.Write(tokenFrequencyDictionary.Keys.ToList()[i].Split(' ')[1]);
            }

            else
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine("\n\n-------------------------------------------------------\n");

        Dictionary<string, int> orderedTokenFrequencyDictionary = tokenFrequencyDictionary.OrderByDescending(x => x.Value).ToDictionary();

        for (int i = 0; i < orderedTokenFrequencyDictionary.Count; i++)
        {
            Console.WriteLine($"{{{orderedTokenFrequencyDictionary.Keys.ToList()[i]}}}: {orderedTokenFrequencyDictionary.Values.ToList()[i]}");
        }

        int[][] charFrequencyTable = new int[28][]; // 28x28 array (A-Z, <S>, <E>)

        for (int i = 0; i < charFrequencyTable.Length; i++)
        {
            charFrequencyTable[i] = new int[28];
        }

        string[] alphabet = new string[28]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "<S>", "<E>"
        };

        for (int i = 0; i < charFrequencyTable.Length; i++)
        {
            for (int j = 0; j < charFrequencyTable.Length; j++)
            {
                if (tokenFrequencyDictionary.ContainsKey($"{alphabet[i]} {alphabet[j]}"))
                {
                    charFrequencyTable[i][j] = tokenFrequencyDictionary[$"{alphabet[i]} {alphabet[j]}"];
                }

                else
                {
                    charFrequencyTable[i][j] = 0;
                }
            }
        }

        Console.WriteLine("\n\n-------------------------------------------------------\n");

        foreach (var item in charFrequencyTable)
        {
            foreach (var miniItem in item)
            {
                Console.Write(miniItem + "|");
            }

            Console.WriteLine();
        }

        float[][] charProbabilities = new float[charFrequencyTable.Length][];
        float tempsum = charFrequencyTable[0].Sum();

        for (int i = 0; i < charProbabilities.Length; i++)
        {
            charProbabilities[i] = new float[charFrequencyTable[i].Length];

            for (int j = 0; j < charFrequencyTable[i].Length; j++) 
            {
                charProbabilities[i][j] = (float) charFrequencyTable[i][j] / tempsum;
            }
        }

        Console.WriteLine("\n\n-------------------------------------------------------\n");

        //foreach (var item in charProbabilities)
        //{
        //    Console.WriteLine(item);
        //}

        var generator = new Generator(device: device("cpu")).manual_seed(int.MaxValue);

        Tensor greatArray = zeros(28, float32);

        //for (int i = 0; i < charProbabilities.Length; i++)
        //{
        //    for (int j = 0; j < charProbabilities[i].Length; j++)
        //    {
        //        greatArray[i, j] = charProbabilities[i][j];
        //    }
        //}

        for (int i = 0; i < charProbabilities.Length; i++)
        {
            greatArray[i] = charProbabilities[0][i];
        }

        Tensor ix = multinomial(greatArray, 1, true, generator);

        Console.WriteLine(alphabet[ix.item<long>()]);

        Console.WriteLine("\n\n-------------------------------------------------------\n");

        for (int i = 0; i < 256; i++)
        {
            var index = multinomial(greatArray, 1, true, generator).item<long>();
            Console.Write(index != 27 && index != 28 ? alphabet[index] : "\n");
        }

        //Console.WriteLine("\n\n-------------------------------------------------------\n");

        //Console.WriteLine(p);

        Console.WriteLine("\n");
    }

    // 2.1 not 3.
    public static void RunTest3(string? filePath = null)
    {
        Console.WriteLine();

        string[] lines = File.ReadAllLines(filePath != null ? filePath : "C:\\ExecutableToCpp\\Test\\Names.txt");

        var sortedFrequencies = torch.zeros(27, 27, dtype: int32);

        foreach (string item in lines)
        {
            List<char> chs = new List<char>();

            chs.Add(SPLIT_TOKEN);
            chs.AddRange(item.ToCharArray());
            chs.Add(SPLIT_TOKEN);

            for (int i = 1; i < chs.Count; i++)
            {
                int xIndex = ALPHABET.FindIndex(chs[i - 1]);
                int yIndex = ALPHABET.FindIndex(chs[i]);

                sortedFrequencies[xIndex, yIndex] += 1;
            }
        }

        SystemModel.LogFileProperty.Times.Add("Added Tokens", DateTime.Now);

        var p = torch.zeros(27, dtype: float32);

        int sum = (int)sortedFrequencies[0].sum();

        for (int i = 0; i < 27; i++)
        {
            p[i] = sortedFrequencies[0, i] / sum;
        }

        SystemModel.LogFileProperty.Times.Add("Converted To Percentages", DateTime.Now);

        var generator = new Generator(device: device("cpu")).manual_seed(int.MaxValue);

        long index = multinomial(p, 1, true, generator).item<long>();

        p = torch.rand(3, generator: generator);
        p = p / p.sum();

        var addN = sortedFrequencies + 1;

        var p2 = addN.ToFloat(27, 27);
        p2 /= p2.sum(1, true);

        generator = new Generator(device: device("cpu")).manual_seed(int.MaxValue);

        for (int i = 0; i < 200; i++)
        {
            index = 0;
            string output = string.Empty;

            while (true)
            {
                p = p2[index];

                index = multinomial(p, 1, true, generator).item<long>();
                output += ALPHABET[index];

                if (index == 0)
                {
                    break;
                }
            }

            Console.WriteLine(output);
        }

        Console.WriteLine();
    }

    // 2.2 not 4.
    public static void RunTest4(string? filePath = null)
    {
        Console.WriteLine();

        string[] lines = File.ReadAllLines(filePath != null ? filePath : "C:\\ExecutableToCpp\\Test\\Names.txt");

        var sortedFrequencies = torch.zeros(27, 27, dtype: int32);

        foreach (string item in lines)
        {
            List<char> chs = new List<char>();

            chs.Add(SPLIT_TOKEN);
            chs.AddRange(item.ToCharArray());
            chs.Add(SPLIT_TOKEN);

            for (int i = 1; i < chs.Count; i++)
            {
                int xIndex = ALPHABET.FindIndex(chs[i - 1]);
                int yIndex = ALPHABET.FindIndex(chs[i]);

                sortedFrequencies[xIndex, yIndex] += 1;
            }
        }

        var p = torch.zeros(27, dtype: float32);

        int sum = (int)sortedFrequencies[0].sum();

        for (int i = 0; i < 27; i++)
        {
            p[i] = sortedFrequencies[0, i] / sum;
        }

        var generator = new Generator(device: device("cpu")).manual_seed(int.MaxValue);

        for (int i = 0; i < 200; i++)
        {
            long index = 0;
            string output = string.Empty;

            while (true)
            {
                p = torch.ones(27) / 27F;

                index = multinomial(p, 1, true, generator).item<long>();
                output += ALPHABET[index];

                if (index == 0)
                {
                    break;
                }
            }

            Console.WriteLine(output);
        }

        Console.WriteLine();
    }

    // 2.3 not 5.
    public static void RunTest5(string? filePath = null)
    {
        Console.WriteLine();

        string[] lines = File.ReadAllLines(filePath != null ? filePath : "C:\\ExecutableToCpp\\Test\\Names.txt");

        var sortedFrequencies = torch.zeros(27, 27, dtype: int32);

        foreach (string item in lines)
        {
            List<char> chs = new List<char>();

            chs.Add(SPLIT_TOKEN);
            chs.AddRange(item.ToCharArray());
            chs.Add(SPLIT_TOKEN);

            for (int i = 1; i < chs.Count; i++)
            {
                int xIndex = ALPHABET.FindIndex(chs[i - 1]);
                int yIndex = ALPHABET.FindIndex(chs[i]);

                sortedFrequencies[xIndex, yIndex] += 1;
            }
        }

        var p = torch.zeros(27, dtype: float32);
        var p2 = (sortedFrequencies + 1).ToFloat(27, 27);
        p2 /= p2.sum(1, true);

        float logLikelihood = 0;
        int counter = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            List<char> chs = new List<char>();

            chs.Add(SPLIT_TOKEN);
            chs.AddRange(lines[i].ToCharArray());
            chs.Add(SPLIT_TOKEN);

            for (int j = 1; j < chs.Count; j++)
            {
                int xIndex = ALPHABET.FindIndex(chs[j - 1]);
                int yIndex = ALPHABET.FindIndex(chs[j]);

                float prob = p2[xIndex, yIndex].item<float>();
                float logProb = log(prob).item<float>();

                logLikelihood += logProb;
                counter++;

                if (i <= 100)
                {
                    Console.WriteLine($"{ALPHABET[xIndex]}{ALPHABET[yIndex]} {prob} {logProb}");
                }
            }
        }

        Console.WriteLine("\n" + ((0 - logLikelihood) / counter));

        Console.WriteLine();
    }
}
