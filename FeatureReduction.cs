using Accord.Statistics.Analysis;
using System;

public static class FeatureReduction
{
    public static float[] ApplyPCA(float[] originalFeatures)
    {
        var pca = new PrincipalComponentAnalysis();
        var matrix = new double[1][] { originalFeatures.Select(f => (double)f).ToArray() };
        pca.Learn(matrix);
        var result = pca.Transform(matrix);
        return result[0].Select(d => (float)d).ToArray();
    }
}    