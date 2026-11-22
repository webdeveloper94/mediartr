using System.IO;

namespace MediaSavodxonlik;

public sealed class ResourceStore(string root)
{
    public string Root => root;
    private static readonly Dictionary<string, string[]> AllowedFiles = new()
    {
        ["lecture"] = [".pdf", ".docx", ".txt"],
        ["slides"] = [".pptx", ".pdf", ".jpg", ".jpeg", ".png"],
        ["media"] = [".mp4", ".wmv", ".avi", ".mov", ".mp3", ".wav", ".gif"],
        ["visuals"] = [".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp"]
    };
    public static bool UsesFiles(string key) => AllowedFiles.ContainsKey(key);
    public static string Extensions(string key) => string.Join(";", AllowedFiles[key].Select(x => "*" + x));
    internal string Folder(int topic, string key)
    {
        if (topic is < 1 or > 20 || !new[] { "lecture", "methods", "media", "visuals", "slides", "assessment", "glossary" }.Contains(key))
            throw new ArgumentException("Noto‘g‘ri mavzu yoki resurs.");
        return Path.Combine(root, $"topic-{topic:00}", key);
    }
    public string Read(int topic, string key)
    {
        string path = Path.Combine(Folder(topic, key), "content.txt");
        return File.Exists(path) ? File.ReadAllText(path) : "";
    }

    internal string ReadGlossary(int topic)
    {
        string path = Path.Combine(Folder(topic, "glossary"), "content.txt");
        var defaults = TopicPages.Read(topic, "glossary");
        if (!File.Exists(path)) return defaults;

        var comparer = StringComparer.Create(System.Globalization.CultureInfo.GetCultureInfo("uz-Latn-UZ"), true);
        static string Term(string line)
        {
            int separator = line.IndexOfAny(['—', '–']);
            if (separator < 0) separator = line.IndexOf(" - ", StringComparison.Ordinal);
            if (separator < 0) separator = line.IndexOf(':');
            return separator > 0 ? line[..separator].Trim() : line;
        }
        var entries = File.ReadAllText(path).Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Concat(defaults.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        return string.Join("\n", entries.DistinctBy(Term, comparer).OrderBy(Term, comparer));
    }
    public void Save(int topic, string key, string text)
    {
        string folder = Folder(topic, key);
        Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, "content.txt");
        string temp = path + ".tmp";
        File.WriteAllText(temp, text);
        File.Move(temp, path, true);
    }
    public string[] Files(int topic, string key)
    {
        string folder = Folder(topic, key);
        return Directory.Exists(folder) ? Directory.GetFiles(folder).Where(f => AllowedFiles[key].Contains(Path.GetExtension(f).ToLowerInvariant())).OrderBy(Path.GetFileName).ToArray() : [];
    }
    internal void DeleteFile(int topic, string key, string name)
    {
        string? path = Files(topic, key).FirstOrDefault(file =>
            string.Equals(Path.GetFileName(file), name, StringComparison.OrdinalIgnoreCase));
        if (path is null) throw new FileNotFoundException("Fayl ushbu mavzu va material turida topilmadi.");
        File.Delete(path);
    }

    public async Task ImportAsync(int topic, string key, string source)
    {
        if (!AllowedFiles.TryGetValue(key, out var extensions) || !extensions.Contains(Path.GetExtension(source).ToLowerInvariant()))
            throw new ArgumentException("Bu bo‘lim uchun fayl turi mos emas.");
        string folder = Folder(topic, key);
        Directory.CreateDirectory(folder);
        string destination = Path.Combine(folder, Path.GetFileName(source));
        if (File.Exists(destination))
            destination = Path.Combine(folder, $"{Path.GetFileNameWithoutExtension(source)}-{Guid.NewGuid().ToString("N")[..8]}{Path.GetExtension(source)}");
        string staging = destination + ".upload";
        try
        {
            await using (var input = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true))
            await using (var output = new FileStream(staging, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true))
                await input.CopyToAsync(output).ConfigureAwait(false);
            File.Move(staging, destination);
        }
        finally { if (File.Exists(staging)) File.Delete(staging); }
    }
}
