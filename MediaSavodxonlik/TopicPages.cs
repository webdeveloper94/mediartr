namespace MediaSavodxonlik;

// To‘rtta resurs mazmunini shu faylda yozing. Matn foydalanuvchi sahifasida tahrirlanmaydi.
// Misol: [(1, "methods")] = "1-mavzu uchun o‘qitish metodlari...",
public static class TopicPages
{
    private static readonly Dictionary<(int Topic, string Resource), string> Content = new()
    {
        [(1, "assessment")] = "",
    };
    public static string Read(int topic, string resource) => resource switch
    {
        "methods" => TeachingMethods.Read(topic),
        "glossary" => TopicGlossary.Read(topic),
        "visuals" when VisualLessons.Find(topic) is { } board =>
            $"{board.Title}\n{string.Join("\n", board.Labels.Zip(board.Details, (label, note) => $"{label} — {note}"))}\n{board.Scenario}\n{VisualLessons.WorkedExamples[topic - 1].Replace('|', '\n')}\n{board.Takeaway}",
        _ => Content.GetValueOrDefault((topic, resource), "")
    };
}
