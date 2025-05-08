using System;

public static class SimilarityCalculation
{
    public static float CosineSimilarity(float[] vecA, float[] vecB)
    {
        float dot = 0, magA = 0, magB = 0;
        for (int i = 0; i < vecA.Length; i++)
        {
            dot += vecA[i] * vecB[i];
            magA += vecA[i] * vecA[i];
            magB += vecB[i] * vecB[i];
        }
        return dot / (MathF.Sqrt(magA) * MathF.Sqrt(magB));
    }
}    