using System.IO;
using System.Runtime.InteropServices;

namespace MediaSavodxonlik;

internal static class PowerPointSlides
{
    private static readonly object ExportLock = new();

    public static Task<string[]> ExportAsync(string path, string folder)
    {
        var completion = new TaskCompletionSource<string[]>(TaskCreationOptions.RunContinuationsAsynchronously);
        var thread = new Thread(() =>
        {
            try
            {
                // PowerPoint can share one application instance across automation calls.
                lock (ExportLock) completion.SetResult(Export(path, folder));
            }
            catch (Exception ex) { completion.SetException(ex); }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        return completion.Task;
    }

    private static string[] Export(string path, string folder)
    {
        var type = Type.GetTypeFromProgID("PowerPoint.Application")
            ?? throw new InvalidOperationException("PPTX maketini ko‘rsatish uchun Microsoft PowerPoint kerak.");
        dynamic app = Activator.CreateInstance(type)!;
        dynamic? presentations = null;
        dynamic? deck = null;
        dynamic? slides = null;
        int? security = null;
        try
        {
            security = (int)app.AutomationSecurity;
            app.AutomationSecurity = 3; // msoAutomationSecurityForceDisable
            presentations = app.Presentations;
            deck = presentations.Open(Path.GetFullPath(path), -1, 0, 0);
            slides = deck.Slides;
            Directory.CreateDirectory(folder);
            var files = new string[(int)slides.Count];
            for (int i = 0; i < files.Length; i++)
            {
                dynamic slide = slides.Item(i + 1);
                try
                {
                    files[i] = Path.Combine(folder, $"slide-{i + 1}.png");
                    slide.Export(files[i], "PNG", 1920);
                }
                finally { Marshal.FinalReleaseComObject(slide); }
            }
            return files;
        }
        finally
        {
            try { if (deck is not null) deck.Close(); }
            finally
            {
                try { if (security is not null) app.AutomationSecurity = security.Value; }
                finally
                {
                    if (slides is not null) Marshal.FinalReleaseComObject(slides);
                    if (deck is not null) Marshal.FinalReleaseComObject(deck);
                    if (presentations is not null) Marshal.FinalReleaseComObject(presentations);
                    Marshal.FinalReleaseComObject(app);
                }
            }
        }
    }
}
