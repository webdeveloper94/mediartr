using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Wpf;
using MediaSavodxonlik;
internal static class ViewerTests
{
    public static void Run(string root)
    {
        // This window belongs to the test process, contains synthetic content only,
        // and never activates over the user's working window.
        var viewer = new InternalViewer();
        var host = new Window { Title = "MediaLab · ichki ko‘ruvchi sinovi", Content = viewer, Width = 900, Height = 700, ShowActivated = false, ShowInTaskbar = false };
        Exception? failure = null;
        Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
        {
        try
        {
            string path = Path.Combine(root, "viewer-test.pdf");
            string[] objects = ["<< /Type /Catalog /Pages 2 0 R >>", "<< /Type /Pages /Kids [3 0 R] /Count 1 >>", "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 500 300] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>", "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>", "<< /Length 48 >>\nstream\nBT /F1 24 Tf 50 180 Td (MediaLab PDF test) Tj ET\nendstream"];
            var pdf = new StringBuilder("%PDF-1.4\n");
            var offsets = new List<int>();
            for (int i = 0; i < objects.Length; i++) { offsets.Add(pdf.Length); pdf.Append($"{i + 1} 0 obj\n{objects[i]}\nendobj\n"); }
            int xref = pdf.Length;
            pdf.Append($"xref\n0 6\n0000000000 65535 f \n");
            foreach (int offset in offsets) pdf.Append($"{offset:0000000000} 00000 n \n");
            pdf.Append($"trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n{xref}\n%%EOF");
            File.WriteAllText(path, pdf.ToString(), Encoding.ASCII);
            var browser = (WebView2CompositionControl)viewer.FindName("Browser");
            await viewer.OpenAsync(path).WaitAsync(TimeSpan.FromSeconds(35));
            if (browser.CoreWebView2 is null || ((TextBlock)viewer.FindName("Status")).Text.Length > 0)
                throw new Exception("PDF viewer failed: " + ((TextBlock)viewer.FindName("Status")).Text);
            if (browser.CoreWebView2.Source != new Uri(path).AbsoluteUri) throw new Exception("PDF source mismatch");
            viewer.Reset();
            await viewer.OpenAsync(Path.Combine(root, "admin-tests", "lesson.docx")).WaitAsync(TimeSpan.FromSeconds(35));
            if (((TextBlock)viewer.FindName("Status")).Text.Length > 0) throw new Exception("DOCX internal viewer failed");
            viewer.Reset();
            string readerRoot = Path.Combine(root, "reader-test");
            var storage = new ResourceStore(readerRoot);
            await storage.ImportAsync(1, "lecture", path);
            var readerWindow = new MainWindow(readerRoot) { ShowActivated = false, Width = 1440, Height = 920 };
            readerWindow.Show();
            try
            {
                await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                typeof(MainWindow).GetMethod("DisplayTopic", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!.Invoke(readerWindow, [1]);
                typeof(MainWindow).GetMethod("SelectResource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!.Invoke(readerWindow, ["lecture"]);
                var readingViewer = (InternalViewer)readerWindow.FindName("LectureViewer");
                var readingBrowser = (WebView2CompositionControl)readingViewer.FindName("Browser");
                var until = DateTime.UtcNow.AddSeconds(30);
                while ((readingBrowser.CoreWebView2 is null || !readingBrowser.CoreWebView2.Source.EndsWith("viewer-test.pdf") || ((TextBlock)readingViewer.FindName("Status")).Text.Length > 0) && DateTime.UtcNow < until)
                    await Task.Delay(100);
                if (readingBrowser.CoreWebView2 is null || !readingBrowser.CoreWebView2.Source.EndsWith("viewer-test.pdf")) throw new Exception("Lecture did not auto-open");
                if (((FrameworkElement)readerWindow.FindName("PageScroll")).Visibility != Visibility.Collapsed) throw new Exception("Old resource header still visible");
                if (readingViewer.ActualHeight < 600) throw new Exception("Lecture viewer did not use available page height");
                typeof(MainWindow).GetMethod("DisplayTopic", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!.Invoke(readerWindow, [2]);
                if (((FrameworkElement)readerWindow.FindName("LectureReader")).Visibility != Visibility.Collapsed) throw new Exception("Reading view did not close on return");
            }
            finally { readerWindow.Close(); }
            Console.WriteLine("PASS: real WebView2 engine initialized, local PDF navigation succeeded, DOCX opened internally and viewer reset.");
            Console.WriteLine("PASS: lecture auto-opens in dedicated full-height reader, old header hidden, return navigation resets reader.");
        }
        catch (Exception ex) { failure = ex; }
        finally { viewer.Reset(); Application.Current.Shutdown(); }
        }));
        Application.Current.Run(host);
        if (failure is not null) throw failure;
    }
}
