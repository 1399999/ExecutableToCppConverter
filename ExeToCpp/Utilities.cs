using static TorchSharp.torch;

namespace ExecutableToCppConverter;

public static class Utilities
{
    public static Tensor ToFloat(this Tensor originalTensor, int xDimension, int yDimension)
    {
        Tensor output = zeros(xDimension, yDimension, dtype: float32);

        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                output[i][j] = originalTensor[i][j];
            }
        }

        return output;
    }

    public static int FindIndex(this char[] list, char character)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == character)
            {
                return i;
            }
        }

        return 0;
    }
}
