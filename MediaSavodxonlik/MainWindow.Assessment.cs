using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaSavodxonlik;

public partial class MainWindow
{
    private void ShowTopicAssessment()
    {
        int topic = currentTopic;
        var concepts = TopicAssessments.Read(topic);
        var lesson = TeachingMethods.Find(topic)!;
        ResourceDescription.Text = allTopics[topic - 1].Title;

        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        MethodPanel.Children.Add(grid);

        string questions = $"""
            1. «{concepts[0].Term}» va «{concepts[1].Term}» tushunchalarini izohlab, farqini misol orqali tushuntiring.

            2. «{concepts[2].Term}» tushunchasi ushbu mavzuda qanday qo‘llanadi? Bitta misol keltiring.

            3. Ko‘rgazmali stenddagi vaziyatni «{concepts[3].Term}» yoki «{concepts[4].Term}» tushunchasi yordamida tahlil qiling. Xulosangizni asoslang.
            """;
        AddCard(0, 0, "Nazorat savollari", "\uE8A5", "#93C5FD", "#DA182F50", questions);
        AddCard(0, 1, "Amaliy topshiriq", "\uE8FD", "#FDBA74", "#DC3A2B2B",
            $"Ish materiali\n{lesson.Setup}\n\nBajarish tartibi\n{lesson.Activity}\n\nTopshiriladigan ish\n{lesson.Evidence}");
        AddCard(1, 0, "Baholash mezonlari", "\uE73E", "#86EFAC", "#DC193D39",
            $"40 ballgacha · O‘quv natijasi\n{lesson.Goal}\n\n35 ballgacha · Bajarish va asoslash\nAmallar ketma-ketligi to‘g‘ri, qarorlar misol yoki dalilga tayangan.\n\n25 ballgacha · Yakuniy ish\nTopshiriladigan ish to‘liq va tushunarli. Uch mezon bo‘yicha olingan ballar qo‘shiladi.");
        var test = AddCard(1, 1, "Mavzu testi", "\uE9D5", "#C4B5FD", "#DC32264F",
            "10 ta savol · 10 ball\n\nSavollar va javob variantlari har urinishda aralashadi. Savollar bittadan chiqadi. Har to‘g‘ri javob 1 ball. Yakunda natija va xato javoblarning to‘g‘ri variantlari ko‘rsatiladi.");
        test.Children.Add(CourseButton("Testni boshlash →", (_, _) =>
            RunQuiz(TopicAssessments.Questions(topic), MethodPanel, () => SelectResource("assessment"), topicQuiz: true)));

        StackPanel AddCard(int row, int column, string title, string icon, string accent, string surface, string text)
        {
            var body = new StackPanel();
            var heading = new DockPanel { Margin = new Thickness(0, 0, 0, 10) };
            var glyph = BoardHeading(icon, accent, 22);
            glyph.FontFamily = new FontFamily("Segoe MDL2 Assets");
            glyph.Margin = new Thickness(0, 0, 12, 0);
            DockPanel.SetDock(glyph, Dock.Left);
            heading.Children.Add(glyph);
            heading.Children.Add(BoardHeading(title, accent, 21));
            body.Children.Add(heading);
            body.Children.Add(CourseText(text, 16));
            var card = BoardSurface(body, surface, accent);
            card.Margin = new Thickness(0, 0, column == 0 ? 12 : 0, 12);
            Grid.SetRow(card, row);
            Grid.SetColumn(card, column);
            grid.Children.Add(card);
            return body;
        }
    }
}
