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
        1 => "#2DD4BF",
        2 => "#38BDF8",
        3 => "#818CF8",
        4 => "#A78BFA",
        5 => "#C084FC",
        6 => "#F472B6",
        7 => "#FB7185",
        8 => "#F87171",
        9 => "#FB923C",
        10 => "#FBBF24",
        11 => "#34D399",
        12 => "#2DD4BF",
        13 => "#06B6D4",
        14 => "#38BDF8",
        15 => "#60A5FA",
        16 => "#818CF8",
        17 => "#C084FC",
        18 => "#F472B6",
        19 => "#FB923C",
        _ => "#10B981"
    };
    [System.Text.Json.Serialization.JsonIgnore]
    public string Surface => Number switch
    {
        1 => "#113835",
        2 => "#103248",
        3 => "#1D2352",
        4 => "#291E52",
        5 => "#341753",
        6 => "#411438",
        7 => "#451424",
        8 => "#451717",
        9 => "#442210",
        10 => "#3F2C0E",
        11 => "#123829",
        12 => "#113835",
        13 => "#0E353D",
        14 => "#103248",
        15 => "#122A4D",
        16 => "#1D2352",
        17 => "#341753",
        18 => "#411438",
        19 => "#442210",
        _ => "#123829"
    };
    [System.Text.Json.Serialization.JsonIgnore]
    public string CardGradientStart => Number switch
    {
        1 => "#20183A40",
        2 => "#2013324B",
        3 => "#201E224F",
        4 => "#202C1F52",
        5 => "#20341951",
        6 => "#2042173B",
        7 => "#20461726",
        8 => "#20441919",
        9 => "#20432313",
        10 => "#203E2C11",
        11 => "#20153B2D",
        12 => "#20183A40",
        13 => "#20113740",
        14 => "#2013324B",
        15 => "#20162A4F",
        16 => "#201E224F",
        17 => "#20341951",
        18 => "#2042173B",
        19 => "#20432313",
        _ => "#20153B2D"
    };
    public string NumberDisplay => Number.ToString();
    public string Label => $"{Number}-mavzu";
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
