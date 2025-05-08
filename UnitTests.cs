using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void HistogramDimensionTest()
    {
        using var testImage = new Bitmap(100, 100);
        var engine = new ImageSearchEngine();
        float[] features = ImageProcessing.ExtractColorHistogram(testImage);
        Assert.AreEqual(64, features.Length);
    }
}    