using System.IO;
using System.Text.Json;
namespace MediaSavodxonlik;
public sealed record TopicEntry(int Number, string Name)
{
    public string Title => string.IsNullOrWhiteSpace(Name) ? $"{Number}-mavzu" : Name;
    public string Label => $"MAVZU {Number:00}";
    public string Subtitle => string.IsNullOrWhiteSpace(Name) ? "Mavzu nomi hali kiritilmagan" : $"{Number}-mavzu · Media va axborot savodxonligi";
}
public sealed class TopicCatalog(string root)
{
    public List<TopicEntry> Load()
    {
        string file = Path.Combine(root, "topics.json");
        var saved = File.Exists(file) ? JsonSerializer.Deserialize<List<TopicEntry>>(File.ReadAllText(file)) ?? [] : [];
        return Enumerable.Range(1, 20).Select(n => new TopicEntry(n, saved.FirstOrDefault(t => t.Number == n)?.Name ?? "")).ToList();
    }
    public void SaveName(int number, string name)
    {
        if (number is < 1 or > 20 || string.IsNullOrWhiteSpace(name) || name.Trim().Length > 200) throw new ArgumentException("Mavzu nomini kiriting (1–200 belgi).");
        var topics = Load();
        topics[number - 1] = new TopicEntry(number, name.Trim());
        Directory.CreateDirectory(root);
        string path = Path.Combine(root, "topics.json");
        File.WriteAllText(path + ".tmp", JsonSerializer.Serialize(topics, new JsonSerializerOptions { WriteIndented = true }));
        File.Move(path + ".tmp", path, true);
    }
}
