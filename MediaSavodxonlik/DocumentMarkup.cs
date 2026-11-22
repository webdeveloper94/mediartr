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
    public static string Build(string path) => Build(path, out _);

    internal static string Build(string path, out int slideCount)
    {
        slideCount = 0;
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
                html.Append(slides ? $"<section id='slide-{page + 1}'>" : "<section>");
                if (slides) html.Append($"<h2>{++page}-slayd</h2>");
                foreach (var paragraph in xml.Descendants().Where(e => e.Name.LocalName == "p"))
                {
                    string text = string.Concat(paragraph.Descendants().Where(e => e.Name.LocalName == "t").Select(e => e.Value));
                    
                    // Check if paragraph contains blip (embedded image)
                    var blips = paragraph.Descendants().Where(e => e.Name.LocalName == "blip").ToList();
                    foreach (var blip in blips)
                    {
                        if (imageCount >= 30) break;
                        string folderPath = entry.FullName[..(entry.FullName.LastIndexOf('/') + 1)];
                        var relE = zip.GetEntry(folderPath + "_rels/" + entry.Name + ".rels");
                        if (relE is null) continue;
                        using var relStm = relE.Open();
                        using var relRdr = XmlReader.Create(relStm, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, MaxCharactersInDocument = 1_000_000 });
                        var rels = XDocument.Load(relRdr);
                        string? id = blip.Attributes().FirstOrDefault(a => a.Name.LocalName == "embed")?.Value;
                        var relation = rels.Descendants().FirstOrDefault(e => e.Attribute("Id")?.Value == id);
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
                        html.Append($"<div class='doc-img-box'><img alt='Didaktik ko‘rgazmali tasvir' src='data:{mime};base64,{Convert.ToBase64String(memory.ToArray())}'/></div>");
                        imageCount++;
                    }

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        string styleVal = paragraph.Descendants().FirstOrDefault(e => e.Name.LocalName == "pStyle")?.Attribute(XName.Get("val", "http://schemas.openxmlformats.org/wordprocessingml/2006/main"))?.Value ?? "";
                        if (styleVal.Contains("Heading1", StringComparison.OrdinalIgnoreCase) || styleVal.Contains("Title", StringComparison.OrdinalIgnoreCase))
                            html.Append("<h2>" + Encode(text) + "</h2>");
                        else if (styleVal.Contains("Heading2", StringComparison.OrdinalIgnoreCase))
                            html.Append("<h3>" + Encode(text) + "</h3>");
                        else
                            html.Append("<p>" + Encode(text) + "</p>");
                    }
                }
                html.Append("</section>");
                if (html.Length > 15_000_000) break;
            }
            slideCount = page;
            body = html.ToString();
        }
        return "<!doctype html><html lang='uz'><meta charset='utf-8'><meta name='viewport' content='width=device-width'><style>body{margin:0;padding:28px 16px;background:#0F172A;color:#E2E8F0;font:14pt/1.0 'Calibri','Segoe UI',sans-serif}section{background:#1E293B;max-width:920px;margin:0 auto 28px;padding:42px 48px;border-radius:18px;box-shadow:0 12px 35px rgba(0,0,0,0.35);border:1px solid #334155}body:has(section:target) section:not(:target){display:none}section:target{min-height:calc(100vh - 48px);box-sizing:border-box}aside{max-width:920px;margin:0 auto 16px;font:11pt 'Calibri';color:#94A3B8;text-align:right}p{margin:0 0 10px;line-height:1.0;font-family:'Calibri',sans-serif;font-size:14pt;white-space:pre-wrap;overflow-wrap:anywhere}h2{color:#2DD4BF;font-size:17pt;font-family:'Calibri',sans-serif;border-bottom:2px solid #0EA5E9;padding-bottom:10px;margin-top:0;margin-bottom:20px}h3{color:#38BDF8;font-size:15pt;font-family:'Calibri',sans-serif;margin-top:20px;margin-bottom:10px}.doc-img-box{text-align:center;margin:24px 0;background:#0B132B;padding:12px;border-radius:14px;border:1px solid #2DD4BF44;box-shadow:0 8px 24px rgba(0,0,0,0.4)}img{max-width:100%;max-height:480px;object-fit:contain;border-radius:8px;display:inline-block}pre{white-space:pre-wrap;font:inherit;overflow-wrap:anywhere}</style>" + body + "</html>";
    }
}
