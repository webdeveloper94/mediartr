using System.IO;
using System.Windows;

namespace MediaSavodxonlik;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        HideContentDirectory();
    }

    private static void HideContentDirectory()
    {
        try
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string contentDir = Path.Combine(baseDir, "Content");
            if (Directory.Exists(contentDir))
            {
                var dirInfo = new DirectoryInfo(contentDir);
                if (!dirInfo.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    dirInfo.Attributes |= FileAttributes.Hidden;
                }
            }
        }
        catch
        {
            // Shunchaki o'tib ketadi (agar ruxsat bo'lmasa xato bermaydi)
        }
    }
}
