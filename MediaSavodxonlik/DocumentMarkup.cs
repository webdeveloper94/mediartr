using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace MediaSavodxonlik;
public static class DocumentMarkup
{
    private static string Encode(string text) => WebUtility.HtmlEncode(text);
    public static string Build(string path)
    {
        string body;
        if (Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            using var reader = new StreamReader(path);
            var buffer = new char[500000];
            int length = reader.ReadBlock(buffer, 0, buffer.Length);
            body = "<section><pre>" + Encode(new string(buffer, 0, length)) + "</pre></section>";
        }
        else
        {
            using var zip = ZipFile.OpenRead(path);
            bool slides = Path.GetExtension(path).Equals(".pptx", StringComparison.OrdinalIgnoreCase);
            var entries = zip.Entries.Where(e => slides ? System.Text.RegularExpressions.Regex.IsMatch(e.FullName, @"^ppt/slides/slide\d+\.xml$") : e.FullName == "word/document.xml")
                .OrderBy(e => slides ? int.Parse(System.Text.RegularExpressions.Regex.Match(e.Name, @"\d+").Value) : 0).Take(200);
            var html = new StringBuilder("<aside>Soddalashtirilgan ko‘rinish: matn va rasmlar. Asl dizayn, jadvallar, animatsiyalar va joylashuv to‘liq saqlanmaydi. To‘liq dizayn uchun PDF yuklang.</aside>");
            int page = 0, imageCount = 0;
            foreach (var entry in entries)
            {
                using var stream = entry.Open();
                using var reader = XmlReader.Create(stream, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, MaxCharactersInDocument = 4_000_000 });
                var xml = XDocument.Load(reader);
                html.Append("<section>");
                if (slides) html.Append($"<h2>{++page}-slayd</h2>");
                foreach (var paragraph in xml.Descendants().Where(e => e.Name.LocalName == "p"))
                {
                    string text = string.Concat(paragraph.Descendants().Where(e => e.Name.LocalName == "t").Select(e => e.Value));
                    if (!string.IsNullOrWhiteSpace(text)) html.Append("<p>" + Encode(text) + "</p>");
                }
                string folder = entry.FullName[..(entry.FullName.LastIndexOf('/') + 1)];
                var relEntry = zip.GetEntry(folder + "_rels/" + entry.Name + ".rels");
                if (relEntry is not null && imageCount < 30)
                {
                    using var relStream = relEntry.Open();
                    using var relReader = XmlReader.Create(relStream, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, MaxCharactersInDocument = 1_000_000 });
                    var relations = XDocument.Load(relReader);
                    foreach (var blip in xml.Descendants().Where(e => e.Name.LocalName == "blip").Take(30 - imageCount))
                    {
                        string? id = blip.Attributes().FirstOrDefault(a => a.Name.LocalName == "embed")?.Value;
                        var relation = relations.Descendants().FirstOrDefault(e => e.Attribute("Id")?.Value == id);
                        if (relation is null || relation.Attribute("TargetMode")?.Value == "External") continue;
                        string? target = relation.Attribute("Target")?.Value;
                        if (target is null) continue;
                        string location = new Uri(new Uri("https://package/" + entry.FullName), target).AbsolutePath.TrimStart('/');
                        var image = zip.GetEntry(Uri.UnescapeDataString(location));
                        if (image is null || image.Length > 5_000_000) continue;
                        string extension = Path.GetExtension(image.Name).ToLowerInvariant();
                        string? mime = extension switch { ".png" => "image/png", ".jpg" or ".jpeg" => "image/jpeg", ".gif" => "image/gif", _ => null };
                        if (mime is null) continue;
                        using var imageStream = image.Open(); using var memory = new MemoryStream(); imageStream.CopyTo(memory);
                        html.Append($"<img alt='Hujjat rasmi' src='data:{mime};base64,{Convert.ToBase64String(memory.ToArray())}'/>"); imageCount++;
                    }
                }
                html.Append("</section>");
                if (html.Length > 15_000_000) break;
            }
            body = html.ToString();
        }
        return "<!doctype html><html lang='uz'><meta charset='utf-8'><meta name='viewport' content='width=device-width'><style>body{margin:0;padding:24px;background:#e9eef4;color:#172033;font:18px Calibri,Segoe UI,sans-serif}section{background:#fff;max-width:950px;margin:0 auto 24px;padding:36px;border-radius:12px}aside{max-width:950px;margin:0 auto 20px;font:13px Segoe UI;color:#42536c}p{line-height:1.55;white-space:pre-wrap}pre{white-space:pre-wrap;font:inherit}img{max-width:100%;max-height:650px;object-fit:contain;display:block;margin:20px auto}h2{color:#167b73}</style>" + body + "</html>";
    }
}
