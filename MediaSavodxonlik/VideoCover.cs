using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MediaSavodxonlik;

internal static class VideoCover
{
    // Decode one thumbnail at a time, even when the library contains many videos.
    private static readonly SemaphoreSlim Decoder = new(1, 1);

    internal static async Task<BitmapSource?> LoadAsync(string path, CancellationToken cancellation)
    {
        await Decoder.WaitAsync(cancellation);
        var player = new MediaPlayer { IsMuted = true, ScrubbingEnabled = true };
        try
        {
            var opened = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            player.MediaOpened += (_, _) => opened.TrySetResult(true);
            player.MediaFailed += (_, _) => opened.TrySetResult(false);
            player.Open(new Uri(path));
            player.Pause();
            if (!await opened.Task.WaitAsync(TimeSpan.FromSeconds(5), cancellation) || player.NaturalVideoWidth == 0)
                return null;

            if (player.NaturalDuration.HasTimeSpan)
                player.Position = TimeSpan.FromSeconds(Math.Min(3, player.NaturalDuration.TimeSpan.TotalSeconds * .12));

            // WPF has no frame-ready notification for a paused MediaPlayer seek.
            await Task.Delay(220, cancellation);
            await Dispatcher.Yield(DispatcherPriority.Render);
            cancellation.ThrowIfCancellationRequested();

            const int width = 480;
            int height = Math.Clamp((int)Math.Round(width * (double)player.NaturalVideoHeight / player.NaturalVideoWidth), 1, 720);
            var visual = new DrawingVisual();
            using (var drawing = visual.RenderOpen())
                drawing.DrawVideo(player, new Rect(0, 0, width, height));
            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            bitmap.Freeze();
            return bitmap;
        }
        catch (Exception ex) when (ex is TimeoutException or InvalidOperationException or ArgumentException or System.Runtime.InteropServices.COMException)
        {
            return null;
        }
        finally
        {
            player.Close();
            Decoder.Release();
        }
    }
}
