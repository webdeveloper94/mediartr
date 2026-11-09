using System.IO;
namespace MediaSavodxonlik;
public static class ContentPaths
{
    public static string Root
    {
        get
        {
            for (var folder = new DirectoryInfo(AppContext.BaseDirectory); folder is not null; folder = folder.Parent)
                if (File.Exists(Path.Combine(folder.FullName, "MediaSavodxonlik", "MediaSavodxonlik.csproj")))
                    return Path.Combine(folder.FullName, "MediaSavodxonlik", "Content");
            return Path.Combine(AppContext.BaseDirectory, "Content");
        }
    }
}
