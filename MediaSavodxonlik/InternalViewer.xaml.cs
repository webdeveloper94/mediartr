using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Core;
namespace MediaSavodxonlik;
public partial class InternalViewer : UserControl
{
    private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromMilliseconds(350) };
    private bool seeking;
    private int revision;
    private string allowedUri = "";
    private Task? browserInit;
    private string? temporaryPreview;
    public void UseReadingLayout()
    {
        ViewerToolbar.Visibility = Visibility.Collapsed;
        ViewerBorder.Padding = new Thickness(0);
        ViewerBorder.BorderThickness = new Thickness(0);
    }
    public InternalViewer()
    {
        InitializeComponent();
        timer.Tick += (_, _) => { if (!seeking && Player.NaturalDuration.HasTimeSpan) { Seek.Value = Player.Position.TotalSeconds; PositionLabel.Text = $"{Player.Position:mm\\:ss} / {Player.NaturalDuration.TimeSpan:mm\\:ss}"; } };
        Unloaded += (_, _) => Reset();
    }
    public void Reset()
    {
        revision++; timer.Stop(); Player.Stop(); Player.Source = null; Picture.Source = null;
        if (Browser.CoreWebView2 is not null) Browser.CoreWebView2.Navigate("about:blank");
        if (temporaryPreview is not null)
        {
            try { File.Delete(temporaryPreview); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            temporaryPreview = null;
        }
        Browser.Visibility = ImageArea.Visibility = VideoArea.Visibility = Visibility.Collapsed;
        Visibility = Visibility.Collapsed;
    }
    private async Task InitializeBrowser()
    {
        var environment = await CoreWebView2Environment.CreateAsync(userDataFolder: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MediaLab", "WebView2"));
        await Browser.EnsureCoreWebView2Async(environment);
        Browser.CoreWebView2.Settings.AreDevToolsEnabled = false;
        Browser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        Browser.CoreWebView2.Settings.IsScriptEnabled = false;
        Browser.CoreWebView2.NavigationStarting += (_, e) => { if (e.Uri != "about:blank" && e.Uri != allowedUri && !e.Uri.StartsWith("data:", StringComparison.OrdinalIgnoreCase)) e.Cancel = true; };
        Browser.CoreWebView2.NewWindowRequested += (_, e) => e.Handled = true;
        Browser.CoreWebView2.DownloadStarting += (_, e) => e.Cancel = true;
    }
    private async Task NavigateDocument(string uri)
    {
        var core = Browser.CoreWebView2;
        ulong? navigationId = null;
        var completion = new TaskCompletionSource<bool>();
        void Starting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri == uri) navigationId = e.NavigationId;
        }
        void Completed(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (navigationId != e.NavigationId) return;
            if (e.IsSuccess) completion.TrySetResult(true);
            else completion.TrySetException(new IOException("Hujjat navigatsiyasi: " + e.WebErrorStatus));
        }
        core.NavigationStarting += Starting;
        core.NavigationCompleted += Completed;
        try { core.Navigate(uri); await completion.Task.WaitAsync(TimeSpan.FromSeconds(20)); }
        finally { core.NavigationStarting -= Starting; core.NavigationCompleted -= Completed; }
    }
    public async Task OpenAsync(string path)
    {
        Reset(); int version = revision;
        Visibility = Visibility.Visible; FileTitle.Text = Path.GetFileName(path); Status.Text = "Material ochilmoqda…";
        try
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Material topilmadi.");
            string extension = Path.GetExtension(path).ToLowerInvariant();
            if (extension is ".mp4" or ".wmv" or ".avi" or ".mov" or ".mp3" or ".wav")
            {
                VideoArea.Visibility = Visibility.Visible; Player.Source = new Uri(path); Player.Play(); Status.Text = ""; return;
            }
            if (extension is ".jpg" or ".jpeg" or ".png" or ".gif")
            {
                using var stream = File.OpenRead(path);
                var bitmap = new BitmapImage(); bitmap.BeginInit(); bitmap.CacheOption = BitmapCacheOption.OnLoad; bitmap.StreamSource = stream; bitmap.EndInit(); bitmap.Freeze();
                Picture.Source = bitmap; ImageArea.Visibility = Visibility.Visible; Status.Text = extension == ".gif" ? "GIF: birinchi kadr ko‘rsatiladi." : ""; return;
            }
            string? html = extension is ".docx" or ".pptx" or ".txt" ? await Task.Run(() => DocumentMarkup.Build(path)) : null;
            if (version != revision) return;
            if (html is null && extension != ".pdf") throw new InvalidDataException("Bu format ichki ko‘ruvchida qo‘llanmaydi. PDF formatini yuklang.");
            Browser.Visibility = Visibility.Visible;
            try { await (browserInit ??= InitializeBrowser()); } catch { browserInit = null; throw; }
            if (version != revision) return;
            allowedUri = new Uri(path).AbsoluteUri;
            if (html is not null)
            {
                // NavigateToString has a 2 MB limit. Store generated markup beside runtime cache instead.
                string folder = Path.Combine(Path.GetTempPath(), "MediaLabPreview"); Directory.CreateDirectory(folder);
                string preview = Path.Combine(folder, Guid.NewGuid().ToString("N") + ".html");
                await File.WriteAllTextAsync(preview, html);
                if (version != revision) { File.Delete(preview); return; }
                temporaryPreview = preview;
                allowedUri = new Uri(preview).AbsoluteUri;
            }
            await NavigateDocument(allowedUri);
            if (version == revision) Status.Text = "";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException or System.Runtime.InteropServices.COMException or System.Xml.XmlException or ArgumentException or NotSupportedException or WebView2RuntimeNotFoundException or System.ComponentModel.Win32Exception or TimeoutException)
        { if (version == revision) Status.Text = "Material ochilmadi: " + ex.Message + " PDF uchun Microsoft Edge WebView2 Runtime mavjudligini tekshiring."; }
    }
    private void CloseViewer(object sender, RoutedEventArgs e) => Reset();
    private void VideoOpened(object sender, RoutedEventArgs e) { if (Player.NaturalDuration.HasTimeSpan) Seek.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds; timer.Start(); }
    private void VideoFailed(object sender, ExceptionRoutedEventArgs e) { timer.Stop(); Status.Text = "Videoni ijro etib bo‘lmadi. MP4 (H.264/AAC) formatida qayta yuklang."; }
    private void VideoEnded(object sender, RoutedEventArgs e) { Player.Pause(); timer.Stop(); }
    private void PlayVideo(object sender, RoutedEventArgs e) { Player.Play(); timer.Start(); }
    private void PauseVideo(object sender, RoutedEventArgs e) => Player.Pause();
    private void StartSeek(object sender, MouseButtonEventArgs e) => seeking = true;
    private void EndSeek(object sender, MouseButtonEventArgs e) { Player.Position = TimeSpan.FromSeconds(Seek.Value); seeking = false; }
}
