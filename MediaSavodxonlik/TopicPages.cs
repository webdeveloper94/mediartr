namespace MediaSavodxonlik;

// To‘rtta resurs mazmunini shu faylda yozing. Matn foydalanuvchi sahifasida tahrirlanmaydi.
// Misol: [(1, "methods")] = "1-mavzu uchun o‘qitish metodlari...",
public static class TopicPages
{
    private static readonly Dictionary<(int Topic, string Resource), string> Content = new()
    {
        [(1, "methods")] = "",
        [(1, "visuals")] = "",
        [(1, "assessment")] = "",
        [(1, "glossary")] = ""
    };
    public static string Read(int topic, string resource) => Content.GetValueOrDefault((topic, resource), "");
}
