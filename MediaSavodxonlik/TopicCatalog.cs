using System.IO;
using System.Text.Json;
namespace MediaSavodxonlik;
public sealed record TopicEntry(int Number, string Name)
{
    public string Title => string.IsNullOrWhiteSpace(Name) ? $"{Number}-mavzu" : Name;
    public bool IsPractical => Number is 3 or 6 or 7 or 10 or 11 or 14 or 15 or 19 or 20;
    public string Schedule => IsPractical ? "Amaliy · 2 soat" : "Nazariy · 2 soat";
    [System.Text.Json.Serialization.JsonIgnore]
    public string Icon => Number switch
    {
        1 => "\uE82D", 2 => "\uE714", 3 => "\uE8B7", 4 => "\uE721", 5 => "\uE9D5",
        6 => "\uE71C", 7 => "\uE8A5", 8 => "\uE77B", 9 => "\uE7BA", 10 => "\uE73E",
        11 => "\uE768", 12 => "\uE70F", 13 => "\uE7F4", 14 => "\uEB9F", 15 => "\uE714",
        16 => "\uE90A", 17 => "\uE8A1", 18 => "\uE72E", 19 => "\uE8C8", _ => "\uE72E"
    };
    [System.Text.Json.Serialization.JsonIgnore]
    public string Track => Number switch
    {
        <= 3 => "Media asoslari",
        <= 7 => "Axborot izlash",
        <= 11 => "Tanqidiy tahlil",
        <= 15 => "Kontent yaratish",
        _ => "Raqamli mas’uliyat"
    };
    [System.Text.Json.Serialization.JsonIgnore]
    public string Accent => Number switch
    {
        <= 3 => "#5EEAD4", <= 7 => "#93C5FD", <= 11 => "#FDBA74", <= 15 => "#C4B5FD", _ => "#86EFAC"
    };
    [System.Text.Json.Serialization.JsonIgnore]
    public string Surface => Number switch
    {
        <= 3 => "#20484A", <= 7 => "#243E61", <= 11 => "#52382E", <= 15 => "#433360", _ => "#284737"
    };
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
