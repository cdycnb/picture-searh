using System.Drawing;

public static class ImageProcessing
{
    public static float[] ExtractColorHistogram(Bitmap image)
    {
        float[] hist = new float[64];
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                Color c = image.GetPixel(x, y);
                int index = (c.R / 64) * 16 + (c.G / 64) * 4 + (c.B / 64);
                hist[index]++;
            }
        }
        return Normalize(hist);
    }

    public static float[] Normalize(this float[] array)
    {
        float sum = array.Sum();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] /= sum;
        }
        return array;
    }
}    