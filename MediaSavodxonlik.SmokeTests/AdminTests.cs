using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediaSavodxonlik;
internal static class AdminTests
{
    public static void Run(string root)
    {
        static void Check(bool value, string message) { if (!value) throw new Exception(message); }
        string isolated = Path.Combine(root, "admin-tests");
        var credentials = new AdminCredentials(isolated);
        Check(!credentials.IsConfigured, "Credentials should be unconfigured");
        credentials.Create("test-password-123");
        Check(new AdminCredentials(isolated).Verify("test-password-123"), "Password not persisted");
        Check(!credentials.Verify("wrong"), "Wrong password accepted");
        Check(!File.ReadAllText(Path.Combine(isolated, ".admin", "credential.json")).Contains("test-password-123"), "Password stored in plaintext");
        bool overwrite = false;
        try { credentials.Create("different-password"); } catch (InvalidOperationException) { overwrite = true; }
        Check(overwrite, "Existing credential could be overwritten");
        var catalog = new TopicCatalog(isolated);
        catalog.SaveName(3, "Axborotni tekshirish");
        Check(new TopicCatalog(isolated).Load()[2].Title == "Axborotni tekshirish", "Topic name not persisted");
        var window = new MainWindow(isolated);
        Check(window.InputBindings.OfType<KeyBinding>().Any(b => b.Key == Key.A && b.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift)), "Missing admin shortcut");
        var login = new AdminLogin(credentials);
        Check(((PasswordBox)login.FindName("Confirm")).Visibility == Visibility.Collapsed, "Existing password allowed setup");
        var panel = new AdminPanel(catalog, new ResourceStore(isolated));
        Check(((ComboBox)panel.FindName("Topics")).Items.Count == 20, "Missing admin topics");
        ((ComboBox)panel.FindName("Topics")).SelectedIndex = 2;
        Check(((TextBox)panel.FindName("TopicName")).Text == "Axborotni tekshirish", "Admin did not load saved title");
        // Render real OpenXML text including escaped HTML, and preserve numeric slide order.
        string docx = Path.Combine(isolated, "lesson.docx");
        using (var zip = ZipFile.Open(docx, ZipArchiveMode.Create))
        using (var writer = new StreamWriter(zip.CreateEntry("word/document.xml").Open()))
            writer.Write("<w:document xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'><w:body><w:p><w:r><w:t>&lt;script&gt;Test&lt;/script&gt;</w:t></w:r></w:p></w:body></w:document>");
        string html = DocumentMarkup.Build(docx);
        Check(html.Contains("&lt;script&gt;Test&lt;/script&gt;") && !html.Contains("<script>"), "Document text not HTML-escaped");
        string pptx = Path.Combine(isolated, "slides.pptx");
        using (var zip = ZipFile.Open(pptx, ZipArchiveMode.Create))
            foreach (int n in new[] { 10, 2, 1 })
            {
                using var writer = new StreamWriter(zip.CreateEntry($"ppt/slides/slide{n}.xml").Open());
                writer.Write($"<sld><a:p xmlns:a='http://schemas.openxmlformats.org/drawingml/2006/main'><a:r><a:t>Slide-{n}</a:t></a:r></a:p></sld>");
            }
        html = DocumentMarkup.Build(pptx);
        Check(html.IndexOf("Slide-1</p>") < html.IndexOf("Slide-2</p>") && html.IndexOf("Slide-2</p>") < html.IndexOf("Slide-10</p>"), "Slides out of order");
        panel.Close(); login.Close(); window.Close();
        Console.WriteLine("PASS: password hashing/verification, setup lock, topic persistence, shortcut, admin views, OpenXML HTML escaping and slide order.");
    }
}
