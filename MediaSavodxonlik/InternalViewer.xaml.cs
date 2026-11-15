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
    private bool previewingVideo;
    private int revision;
    private string allowedUri = "";
    private Task? browserInit;
    private string? temporaryPreview;
    private string[] slides = [];
    private string? slideFolder;
    private int slideNumber;
    public bool IsPdf { get; private set; }
    public double DocumentZoom => Browser.ZoomFactor;
    public void SetDocumentZoom(double zoom) => Browser.ZoomFactor = zoom;

    internal bool MoveSlide(int delta)
    {
        if (slides.Length == 0) return false;

        int next = Math.Clamp(slideNumber + delta, 1, slides.Length);
        if (next != slideNumber) OpenSlide(next);
        return true;
    }

    private void OpenSlide(int number)
    {
        slideNumber = number;
        Picture.Source = LoadPicture(slides[number - 1]);
        SlideNavigation.Visibility = Visibility.Visible;
        SlidePosition.Text = $"{number} / {slides.Length}";
        PreviousSlide.IsEnabled = number > 1;
        NextSlide.IsEnabled = number < slides.Length;
    }
    private void GoToPreviousSlide(object sender, RoutedEventArgs e) => MoveSlide(-1);
    private void GoToNextSlide(object sender, RoutedEventArgs e) => MoveSlide(1);

    private static BitmapImage LoadPicture(string path)
    {
        using var stream = File.OpenRead(path);
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }

    private static void DeleteSlides(string folder)
    {
        try { if (Directory.Exists(folder)) Directory.Delete(folder, true); }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
    public void UseReadingLayout()
    {
        ViewerToolbar.Visibility = Visibility.Collapsed;
        ViewerBorder.Padding = new Thickness(0);
        ViewerBorder.BorderThickness = new Thickness(0);
        ViewerBorder.Background = System.Windows.Media.Brushes.Black;
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
        previewingVideo = seeking = false;
        PosterPlay.Visibility = Visibility.Collapsed;
        Seek.Value = 0;
        PositionLabel.Text = "";
        slides = [];
        slideNumber = 0;
        SlideNavigation.Visibility = Visibility.Collapsed;
        if (slideFolder is not null)
        {
            DeleteSlides(slideFolder);
            slideFolder = null;
        }
        IsPdf = false;
        SetDocumentZoom(1);
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
    public async Task OpenAsync(string path, bool previewVideo = false)
    {
        Reset(); int version = revision;
        Visibility = Visibility.Visible; FileTitle.Text = Path.GetFileName(path); Status.Text = "Material ochilmoqda…";
        try
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Material topilmadi.");
            string extension = Path.GetExtension(path).ToLowerInvariant();
            IsPdf = extension == ".pdf";
            if (extension is ".mp4" or ".wmv" or ".avi" or ".mov" or ".mp3" or ".wav")
            {
                previewingVideo = previewVideo && extension is ".mp4" or ".wmv" or ".avi" or ".mov";
                VideoArea.Visibility = Visibility.Visible;
                Player.Source = new Uri(path);
                if (previewingVideo)
                {
                    PosterPlay.Visibility = Visibility.Visible;
                    Player.Pause();
                }
                else Player.Play();
                Status.Text = "";
                return;
            }
            if (extension is ".jpg" or ".jpeg" or ".png" or ".gif")
            {
                Picture.Source = LoadPicture(path);
                ImageArea.Visibility = Visibility.Visible;
                Status.Text = extension == ".gif" ? "GIF: birinchi kadr ko‘rsatiladi." : "";
                return;
            }
            if (extension == ".pptx")
            {
                string folder = Path.Combine(Path.GetTempPath(), "MediaLabSlides", Guid.NewGuid().ToString("N"));
                try
                {
                    var images = await PowerPointSlides.ExportAsync(path, folder);
                    if (version != revision) return;
                    if (images.Length == 0) throw new InvalidDataException("Taqdimotda slayd yo‘q.");
                    slides = images;
                    OpenSlide(1);
                    slideFolder = folder;
                    ImageArea.Visibility = Visibility.Visible;
                    Status.Text = "";
                }
                finally
                {
                    if (slideFolder != folder) DeleteSlides(folder);
                }
                return;
            }
            string? html = extension is ".docx" or ".txt" ? await Task.Run(() => DocumentMarkup.Build(path)) : null;
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
        {
            if (version == revision) Status.Text = "Material ochilmadi: " + ex.Message
                + (Path.GetExtension(path).Equals(".pptx", StringComparison.OrdinalIgnoreCase)
                    ? " PowerPoint dasturida fayl ochilishini tekshiring yoki taqdimotni PDF sifatida yuklang."
                    : "");
        }
    }
    private void CloseViewer(object sender, RoutedEventArgs e) => Reset();
    private void VideoOpened(object sender, RoutedEventArgs e)
    {
        if (Player.NaturalDuration.HasTimeSpan)
        {
            var duration = Player.NaturalDuration.TimeSpan;
            Seek.Maximum = duration.TotalSeconds;
            if (previewingVideo) Player.Position = TimeSpan.FromSeconds(Math.Min(3, duration.TotalSeconds * 0.12));
        }
        if (!previewingVideo) timer.Start();
    }
    private void VideoFailed(object sender, ExceptionRoutedEventArgs e)
    {
        timer.Stop();
        PosterPlay.Visibility = Visibility.Collapsed;
        Status.Text = "Videoni ijro etib bo‘lmadi. MP4 (H.264/AAC) formatida qayta yuklang.";
    }
    private void VideoEnded(object sender, RoutedEventArgs e) { Player.Pause(); timer.Stop(); }
    private void PlayVideo(object sender, RoutedEventArgs e)
    {
        if (previewingVideo) Player.Position = TimeSpan.Zero;
        previewingVideo = false;
        PosterPlay.Visibility = Visibility.Collapsed;
        Player.Play();
        timer.Start();
    }
    private void PauseVideo(object sender, RoutedEventArgs e) => Player.Pause();
    private void StartSeek(object sender, MouseButtonEventArgs e) => seeking = true;
    private void EndSeek(object sender, MouseButtonEventArgs e) { Player.Position = TimeSpan.FromSeconds(Seek.Value); seeking = false; }
}
