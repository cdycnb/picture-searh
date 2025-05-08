using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.Caching;

public class ImageSearchEngine
{
    private Dictionary<string, float[]> _featureDB = new();
    private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

    public void BuildDatabase(string imageFolder)
    {
        var files = Directory.EnumerateFiles(imageFolder, "*.jpg");
        Parallel.ForEach(files, file =>
        {
            using var image = new Bitmap(file);
            float[] features = ExtractColorHistogram(image);
            lock (_featureDB)
            {
                _featureDB.Add(file, features);
            }
        });
        SaveFeatureDatabase();
    }

    public void SaveFeatureDatabase()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText("features.json", JsonSerializer.Serialize(_featureDB, options));
    }

    public void LoadFeatureDatabase()
    {
        if (File.Exists("features.json"))
        {
            var json = File.ReadAllText("features.json");
            _featureDB = JsonSerializer.Deserialize<Dictionary<string, float[]>>(json);
        }
    }

    public List<SearchResult> Search(Image queryImage)
    {
        var cacheKey = ComputeHash(queryImage);
        return _cache.GetOrCreate(cacheKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return ComputeSearchResults(queryImage);
        });
    }

    private List<SearchResult> ComputeSearchResults(Image queryImage)
    {
        using var bitmap = new Bitmap(queryImage);
        float[] queryFeatures = ExtractColorHistogram(bitmap);
        var results = new List<SearchResult>();
        foreach (var kvp in _featureDB)
        {
            float similarity = CosineSimilarity(queryFeatures, kvp.Value);
            results.Add(new SearchResult { ImagePath = kvp.Key, Similarity = similarity });
        }
        return results.OrderByDescending(r => r.Similarity).ToList();
    }

    private string ComputeHash(Image image)
    {
        // 简单示例，实际需要更复杂的哈希算法
        return image.GetHashCode().ToString();
    }
}

public class SearchResult
{
    public string ImagePath { get; set; }
    public float Similarity { get; set; }
}    