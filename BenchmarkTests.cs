using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Drawing;

[SimpleJob(RuntimeMoniker.Net60)]
public class SearchBenchmark
{
    private ImageSearchEngine _engine;
    private Image testImage;

    [GlobalSetup]
    public void Setup()
    {
        _engine = new ImageSearchEngine();
        _engine.BuildDatabase("TestData");
        testImage = Image.FromFile("TestData/test.jpg");
    }

    [Benchmark]
    public void SearchPerformance()
    {
        _engine.Search(testImage);
    }
}

class Program
{
    static void Main()
    {
        var summary = BenchmarkRunner.Run<SearchBenchmark>();
    }
}    