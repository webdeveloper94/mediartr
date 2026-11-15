using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MediaSavodxonlik;

public static class TopicContext
{
    private static string ReadText(string path)
    {
        using var reader = new StreamReader(path);
        var buffer = new char[5000];
        return new string(buffer, 0, reader.ReadBlock(buffer, 0, buffer.Length));
    }
    public static string Build(ResourceStore store, int topic)
    {
        var result = new StringBuilder();
        var namedTopic = new TopicCatalog(store.Root).Load().First(t => t.Number == topic);
        result.AppendLine($"Mavzu nomi: {namedTopic.Title}");
        foreach (string key in new[] { "methods", "visuals", "assessment", "glossary" })
        {
            string text = key == "glossary" ? store.ReadGlossary(topic) : TopicPages.Read(topic, key);
            if (!string.IsNullOrWhiteSpace(text)) result.AppendLine($"[{key}]\n{text[..Math.Min(text.Length, 3500)]}");
        }
        foreach (string key in new[] { "lecture", "slides" })
        foreach (string file in store.Files(topic, key).Take(4))
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            string text = "";
            try
            {
                if (extension == ".txt") text = ReadText(file);
                else if (extension is ".docx" or ".pptx")
                {
                    using var zip = ZipFile.OpenRead(file);
                    foreach (var entry in zip.Entries.Where(e => extension == ".docx" ? e.FullName == "word/document.xml" : System.Text.RegularExpressions.Regex.IsMatch(e.FullName, @"^ppt/slides/slide\d+\.xml$")).OrderBy(e => e.FullName).Take(20))
                    {
                        using var stream = entry.Open();
                        using var reader = XmlReader.Create(stream, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, MaxCharactersInDocument = 2_000_000 });
                        var xml = XDocument.Load(reader);
                        text += string.Join(" ", xml.Descendants().Where(e => e.Name.LocalName == "t").Select(e => e.Value)) + "\n";
                        if (text.Length >= 5000) break;
                    }
                }
                if (text.Length > 0) result.AppendLine($"[{Path.GetFileName(file)}]\n{text[..Math.Min(text.Length, 5000)]}");
            }
            catch (Exception ex) when (ex is IOException or XmlException or InvalidDataException) { result.AppendLine($"[{Path.GetFileName(file)}] Matn o‘qilmadi."); }
        }
        result.AppendLine("PDF, rasmlar, audio va video mazmuni avtomatik o‘qilmadi. Mavzu nomi faqat raqamdan iborat bo‘lsa, haqiqiy nom hali kiritilmagan.");
        return result.ToString()[..Math.Min(result.Length, 22000)];
    }
}
