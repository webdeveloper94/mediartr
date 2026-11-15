using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MediaSavodxonlik;

public partial class MainWindow
{
    private void ShowTeachingMethod(TeachingMethods.Lesson lesson)
    {
        ResourceDescription.Text = allTopics[currentTopic - 1].Title + " · " + allTopics[currentTopic - 1].Schedule;
        var tabs = new WrapPanel { Margin = new Thickness(0, 0, 0, 12) };
        var content = new StackPanel();
        MethodPanel.Children.Add(tabs);
        MethodPanel.Children.Add(content);

        Color[] accents = [Color.FromRgb(94, 234, 212), Color.FromRgb(253, 186, 116), Color.FromRgb(196, 181, 253), Color.FromRgb(134, 239, 172)];
        (string Label, string Icon, Color Accent, Action Render)[] sections =
        [
            ("O‘quv natijalari", "\uE73E", accents[0], Outcomes),
            ("Vaqt xaritasi", "\uE823", accents[1], Timeline),
            ("Interaktiv metod", "\uEA80", accents[2], Method),
            ("Baholash mezonlari", "\uE9D5", accents[3], Rubric)
        ];

        var choices = new List<RadioButton>();
        foreach (var section in sections)
        {
            var label = new StackPanel { Orientation = Orientation.Horizontal };
            label.Children.Add(new TextBlock
            {
                Text = section.Icon, FontFamily = new FontFamily("Segoe MDL2 Assets"),
                Foreground = new SolidColorBrush(section.Accent), Margin = new Thickness(0, 0, 8, 0)
            });
            label.Children.Add(CourseText(section.Label, 14));
            var tab = new RadioButton
            {
                Content = label, GroupName = "TeachingMethodSections", Padding = new Thickness(8, 0, 0, 0),
                VerticalContentAlignment = VerticalAlignment.Center, Cursor = Cursors.Hand
            };
            System.Windows.Automation.AutomationProperties.SetName(tab, section.Label);
            var tile = new Border
            {
                Child = tab, Background = new SolidColorBrush(Color.FromRgb(23, 37, 57)),
                CornerRadius = new CornerRadius(10), Padding = new Thickness(13, 9, 13, 9),
                Margin = new Thickness(0, 0, 8, 8), BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(68, 86, 108))
            };
            var defaultBorder = tile.BorderBrush;
            tab.Checked += (_, _) =>
            {
                tile.BorderBrush = new SolidColorBrush(section.Accent);
                content.Children.Clear();
                section.Render();
            };
            tab.Unchecked += (_, _) => tile.BorderBrush = defaultBorder;
            choices.Add(tab);
            tabs.Children.Add(tile);
        }
        choices[0].IsChecked = true;

        void Outcomes()
        {
            AddCard(0, "Dars yakunida", lesson.Goal);
            AddCard(1, "Darsga tayyorgarlik", lesson.Setup + " Internet cheklanganida oldindan saqlangan yoki bosma namunalardan foydalaning.");
            AddCard(3, "Topshiriladigan ish", lesson.Evidence);
        }

        void Timeline()
        {
            AddCard(1, "2 akademik soat · 90 daqiqa", lesson.Method);
            AddCard(0, "1. Kirish · 10 daqiqa", "O‘quvchi faoliyati", lesson.Introduction);
            AddCard(1, "2. Namoyish · 20 daqiqa", "O‘qituvchi faoliyati", lesson.Demonstration);
            AddCard(2, "3. Mashq · 45 daqiqa", "O‘quvchi faoliyati", lesson.Activity);
            AddCard(3, "4. Muhokama va tekshiruv · 15 daqiqa", "Yakuniy faoliyat", lesson.Assessment);
        }

        void Method()
        {
            AddCard(2, lesson.Method, lesson.Goal);
            AddCard(0, "Ish materiali", lesson.Setup);
            AddCard(2, "Mashqni o‘tkazish", lesson.Activity);
            AddCard(3, "Fikr bildirish va tekshirish", lesson.Assessment);
        }

        void Rubric()
        {
            AddCard(3, "Baholanadigan ish", lesson.Evidence);
            AddCard(0, "Mavzuga mos yechim · 40 ballgacha", "O‘quv natijasi qay darajada bajarilganini tekshiring.", lesson.Goal);
            AddCard(2, "Bajarish tartibi va asoslash · 35 ballgacha", "Muhim amallar bajarilgani va qarorlar misol, kuzatuv yoki dalil bilan izohlanganini tekshiring.");
            AddCard(1, "Yakuniy ish sifati · 25 ballgacha", "Ishning to‘liqligi, tushunarliligi va yuqorida ko‘rsatilgan mezonga mosligini tekshiring.");
            AddCard(3, "Tavsiya etiladigan hisob · 100 ball", "Har bir mezonda to‘liq bajarilgan ishga maksimal ball, qisman bajarilgan ishga bajarilishiga mos ball, bajarilmagan ishga 0 ball bering. Uch mezon ballarini qo‘shing.");
        }

        void AddCard(int color, string heading, params string[] paragraphs)
        {
            var accent = accents[color];
            var body = new StackPanel();
            var title = CourseText(heading, 19);
            title.Foreground = new SolidColorBrush(accent);
            title.FontWeight = FontWeights.SemiBold;
            body.Children.Add(title);
            foreach (string paragraph in paragraphs)
            {
                var text = CourseText(paragraph, 16);
                text.Foreground = new SolidColorBrush(Color.FromRgb(222, 234, 245));
                text.Margin = new Thickness(0, 6, 0, 0);
                body.Children.Add(text);
            }
            content.Children.Add(new Border
            {
                Background = new LinearGradientBrush(Color.FromRgb(19, 31, 49),
                    Color.FromRgb((byte)(19 + accent.R / 12), (byte)(31 + accent.G / 12), (byte)(49 + accent.B / 12)), 12),
                BorderBrush = new SolidColorBrush(Color.FromArgb(144, accent.R, accent.G, accent.B)),
                BorderThickness = new Thickness(3, 1, 1, 1),
                CornerRadius = new CornerRadius(13), Padding = new Thickness(18),
                Margin = new Thickness(0, 0, 0, 12), Child = body
            });
        }
    }
}
