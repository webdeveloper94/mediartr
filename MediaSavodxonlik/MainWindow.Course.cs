using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaSavodxonlik;

public partial class MainWindow
{
    private void BuildCourseSection(string key)
    {
        (SectionTitle.Text, SectionDescription.Text) = key switch
        {
            "independent" => ("Mustaqil ta’lim", "14 ta mustaqil ish · har biri 1 soat"),
            "media" => ("Media kutubxona", "Mavzular bo‘yicha yuklangan videolar"),
            "tests" => ("Yakuniy nazorat", "Test yoki nazorat savollarini tanlang."),
            "glossary" => ("Glossariy", "Barcha mavzularga kiritilgan atamalar"),
            _ => ("", "")
        };
        Breadcrumb.Text = "O‘quv maydoni / " + SectionTitle.Text;
        try
        {
            switch (key)
            {
                case "independent": ShowIndependentWork(); break;
                case "media": ShowMediaLibrary(); break;
                case "tests": ShowAssessmentMenu(); break;
                case "glossary": ShowGlossary(); break;
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            SectionContent.Children.Add(CourseText("Materiallar o‘qilmadi. " + ex.Message));
        }
    }

    private static TextBlock CourseText(string text, double size = 15) => new()
    {
        Text = text, FontSize = size, TextWrapping = TextWrapping.Wrap,
        Foreground = Brushes.White, LineHeight = size * 1.5
    };

    private static Button CourseButton(string text, RoutedEventHandler click)
    {
        var button = new Button
        {
            Content = CourseText(text), Background = new SolidColorBrush(Color.FromRgb(33, 71, 68)),
            Margin = new Thickness(0, 6, 0, 0), HorizontalAlignment = HorizontalAlignment.Left
        };
        button.Click += click;
        return button;
    }

    private StackPanel CourseCard(string title, string details, StackPanel? host = null)
    {
        var body = new StackPanel();
        body.Children.Add(CourseText(title, 19));
        if (details.Length > 0)
        {
            var text = CourseText(details);
            text.Margin = new Thickness(0, 8, 0, 0);
            body.Children.Add(text);
        }
        (host ?? SectionContent).Children.Add(new Border
        {
            Child = body, Background = new SolidColorBrush(Color.FromRgb(21, 34, 56)),
            CornerRadius = new CornerRadius(12), Padding = new Thickness(18), Margin = new Thickness(0, 0, 0, 12)
        });
        return body;
    }

    private void OpenTopicIndependent(object sender, RoutedEventArgs e)
    {
        ShowPage("independent");
        SectionContent.Children.Clear();
        SectionDescription.Text = $"{currentTopic}-mavzu · {allTopics[currentTopic - 1].Title}";
        SectionContent.Children.Add(CourseButton("← Barcha mustaqil ishlar", (_, _) => ShowPage("independent")));
        ShowIndependentWork(currentTopic);
    }

    private void ShowIndependentWork(int? topicNumber = null)
    {
        string[] icons = ["\uE82D", "\uE8B7", "\uE721", "\uE9D5", "\uE71C", "\uE8A5", "\uE77B",
            "\uE7BA", "\uE73E", "\uE70F", "\uEB9F", "\uE90A", "\uE8A1", "\uE72E"];
        (Color Accent, Color Surface)[] palette =
        [
            (Color.FromRgb(94, 234, 212), Color.FromRgb(19, 45, 49)),
            (Color.FromRgb(196, 181, 253), Color.FromRgb(41, 32, 62)),
            (Color.FromRgb(253, 186, 116), Color.FromRgb(52, 38, 32)),
            (Color.FromRgb(134, 239, 172), Color.FromRgb(26, 46, 39))
        ];
        for (int workIndex = 0; workIndex < CourseActivities.Independent.Length; workIndex++)
        {
            var work = CourseActivities.Independent[workIndex];
            if (topicNumber is not null && work.Topic != topicNumber) continue;

            var topic = allTopics[work.Topic - 1];
            var (accentColor, surface) = palette[workIndex % palette.Length];
            var accent = new SolidColorBrush(accentColor);
            var layout = new DockPanel();
            var icon = new Border
            {
                Width = 54, Height = 54, CornerRadius = new CornerRadius(14),
                VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 0, 16, 0),
                Background = new SolidColorBrush(Color.FromArgb(34, accentColor.R, accentColor.G, accentColor.B)),
                Child = new TextBlock
                {
                    Text = icons[workIndex], FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 23,
                    Foreground = accent, HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            DockPanel.SetDock(icon, Dock.Left);
            layout.Children.Add(icon);
            var card = new StackPanel();
            layout.Children.Add(card);
            var topicHeading = CourseText($"{topic.Number}-mavzu · {topic.Title}", 18);
            topicHeading.FontWeight = FontWeights.SemiBold;
            card.Children.Add(topicHeading);
            var workHeading = CourseText(work.Title, 16);
            workHeading.Margin = new Thickness(0, 4, 0, 0);
            card.Children.Add(workHeading);
            var metadata = CourseText($"Mustaqil ish {workIndex + 1:00} · 1 soat", 13);
            metadata.Foreground = accent;
            metadata.Margin = new Thickness(0, 4, 0, 4);
            card.Children.Add(metadata);
            SectionContent.Children.Add(new Border
            {
                Child = layout, Background = new SolidColorBrush(surface),
                BorderBrush = new SolidColorBrush(Color.FromArgb(104, accentColor.R, accentColor.G, accentColor.B)),
                BorderThickness = new Thickness(3, 1, 1, 1), CornerRadius = new CornerRadius(14),
                Padding = new Thickness(18), Margin = new Thickness(0, 0, 0, 12)
            });
            var details = new StackPanel { Visibility = Visibility.Collapsed, Margin = new Thickness(0, 12, 0, 0) };
            details.Children.Add(CourseText("Bajarish tartibi", 17));
            string[] steps = work.Steps.Split('|');
            for (int index = 0; index < steps.Length; index++)
            {
                var step = CourseText($"{index + 1}. {steps[index]}");
                step.Margin = new Thickness(0, 6, 0, 0);
                details.Children.Add(step);
            }
            var toggle = CourseButton("Batafsil", (sender, _) =>
            {
                bool expand = details.Visibility == Visibility.Collapsed;
                details.Visibility = expand ? Visibility.Visible : Visibility.Collapsed;
                ((TextBlock)((Button)sender).Content).Text = expand ? "Yig‘ish" : "Batafsil";
            });
            toggle.Background = Brushes.Transparent;
            toggle.BorderBrush = accent;
            toggle.BorderThickness = new Thickness(1);
            ((TextBlock)toggle.Content).Foreground = accent;
            card.Children.Add(toggle);
            card.Children.Add(details);
        }
    }

    private void ShowMediaLibrary()
    {
        var allVideoList = new List<(TopicEntry Topic, ResourceFile Video)>();
        foreach (var topic in allTopics)
        {
            var videos = store.Files(topic.Number, "media")
                .Where(path => Path.GetExtension(path).ToLowerInvariant() is ".mp4" or ".wmv" or ".avi" or ".mov")
                .Select(path => new ResourceFile(path)).ToList();
            foreach (var v in videos) allVideoList.Add((topic, v));
        }

        // Search and Filter Bar
        var filterBar = new DockPanel { Margin = new Thickness(0, 0, 0, 16) };
        var searchBox = new TextBox
        {
            FontSize = 13,
            Padding = new Thickness(10, 6, 10, 6),
            Foreground = Brushes.White,
            Background = new SolidColorBrush(Color.FromRgb(18, 28, 45)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(56, 78, 105)),
            CaretBrush = Brushes.White,
            MaxWidth = 340,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        System.Windows.Automation.AutomationProperties.SetName(searchBox, "Videolarni qidirish");

        var searchLabel = CourseText("🔍 Video yoki mavzuni qidirish: ", 13);
        searchLabel.Foreground = new SolidColorBrush(Color.FromRgb(148, 163, 184));
        searchLabel.VerticalAlignment = VerticalAlignment.Center;
        searchLabel.Margin = new Thickness(0, 0, 10, 0);

        var searchPanel = new StackPanel { Orientation = Orientation.Horizontal };
        searchPanel.Children.Add(searchLabel);
        searchPanel.Children.Add(searchBox);
        filterBar.Children.Add(searchPanel);

        var countBadge = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(45, 94, 234, 212)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(94, 234, 212)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(12, 6, 12, 6),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        var countText = CourseText($"{allVideoList.Count} ta video mavjud", 12);
        countText.FontWeight = FontWeights.SemiBold;
        countText.Foreground = new SolidColorBrush(Color.FromRgb(153, 246, 228));
        countBadge.Child = countText;
        DockPanel.SetDock(countBadge, Dock.Right);
        filterBar.Children.Add(countBadge);

        var container = new StackPanel();
        SectionContent.Children.Add(filterBar);
        SectionContent.Children.Add(container);

        void RenderVideos(string query)
        {
            container.Children.Clear();
            var matches = allVideoList.Where(item =>
                string.IsNullOrWhiteSpace(query) ||
                item.Video.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                item.Topic.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                $"{item.Topic.Number}-mavzu".Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            SectionDescription.Text = $"{matches.Count} ta video · mavzular bo‘yicha";
            if (matches.Count == 0)
            {
                container.Children.Add(CourseText(allVideoList.Count == 0 ? "Hozircha video yuklanmagan." : "Qidiruvga mos keluvchi video topilmadi."));
                return;
            }

            var gallery = new UniformGrid { Columns = 2, Margin = new Thickness(0, 6, -14, 0) };
            foreach (var (t, vid) in matches)
            {
                var topicVideos = allVideoList.Where(x => x.Topic.Number == t.Number).Select(x => x.Video).ToList();
                gallery.Children.Add(CreateVideoCard(t, vid, (_, _) =>
                {
                    currentTopic = t.Number;
                    currentResource = "library";
                    OpenReader(topicVideos, vid);
                }));
            }
            container.Children.Add(gallery);
        }

        RenderVideos("");
        searchBox.TextChanged += (_, _) => RenderVideos(searchBox.Text.Trim());
    }

    private Button CreateVideoCard(TopicEntry topic, ResourceFile video, RoutedEventHandler open)
    {
        Color accent = topic.IsPractical ? Color.FromRgb(196, 181, 253) : Color.FromRgb(94, 234, 212);
        Color coverColor = topic.IsPractical ? Color.FromRgb(58, 41, 89) : Color.FromRgb(22, 74, 79);
        var accentBrush = new SolidColorBrush(accent);
        var cover = new Grid { ClipToBounds = true };
        var frame = new Image { Stretch = Stretch.UniformToFill, IsHitTestVisible = false };
        cover.Children.Add(frame);
        CancellationTokenSource? loading = null;
        frame.Loaded += async (_, _) =>
        {
            if (frame.Source is not null || loading is not null) return;
            using var request = new CancellationTokenSource();
            loading = request;
            try { frame.Source = await VideoCover.LoadAsync(video.Path, request.Token); }
            catch (OperationCanceledException) { }
            finally { if (loading == request) loading = null; }
        };
        frame.Unloaded += (_, _) =>
        {
            loading?.Cancel();
            loading = null;
        };
        cover.Children.Add(new Border
        {
            Background = new LinearGradientBrush(Colors.Transparent, Color.FromArgb(160, 8, 16, 28), 90),
            IsHitTestVisible = false
        });
        cover.Children.Add(new System.Windows.Shapes.Ellipse
        {
            Width = 112, Height = 112, Stroke = new SolidColorBrush(Color.FromArgb(60, accent.R, accent.G, accent.B))
        });
        cover.Children.Add(new Border
        {
            Width = 62, Height = 62, CornerRadius = new CornerRadius(31),
            HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
            Background = accentBrush,
            Child = new TextBlock
            {
                Text = "▶", FontSize = 24, Foreground = new SolidColorBrush(Color.FromRgb(14, 24, 40)),
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(3, 0, 0, 0)
            }
        });
        var format = CourseText(Path.GetExtension(video.Name).TrimStart('.').ToUpperInvariant(), 11);
        format.Foreground = accentBrush;
        format.FontWeight = FontWeights.SemiBold;
        format.HorizontalAlignment = HorizontalAlignment.Right;
        format.VerticalAlignment = VerticalAlignment.Bottom;
        format.Margin = new Thickness(0, 0, 14, 11);
        cover.Children.Add(format);

        var body = new StackPanel();
        body.Children.Add(new Border
        {
            Height = 148, CornerRadius = new CornerRadius(9), Child = cover,
            Background = new LinearGradientBrush(Color.FromRgb(14, 24, 40), coverColor, 25)
        });
        var number = CourseText($"{topic.Number:00}-mavzu", 13);
        number.Foreground = accentBrush;
        number.Margin = new Thickness(1, 12, 0, 3);
        body.Children.Add(number);
        var title = CourseText(topic.Title, 18);
        title.FontWeight = FontWeights.SemiBold;
        body.Children.Add(title);
        var fileName = CourseText(video.Name, 12);
        fileName.Foreground = new SolidColorBrush(Color.FromRgb(186, 203, 221));
        fileName.Margin = new Thickness(0, 4, 0, 10);
        body.Children.Add(fileName);
        var action = CourseText("Ko‘rish  →", 15);
        action.Foreground = accentBrush;
        action.FontWeight = FontWeights.SemiBold;
        body.Children.Add(action);

        var card = new Button
        {
            Content = body, HorizontalContentAlignment = HorizontalAlignment.Stretch,
            Background = new SolidColorBrush(Color.FromRgb(19, 31, 50)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(44, 60, 84)),
            BorderThickness = new Thickness(1), Padding = new Thickness(12),
            Margin = new Thickness(0, 0, 14, 14)
        };
        System.Windows.Automation.AutomationProperties.SetName(card, $"Ko‘rish: {topic.Number}-mavzu, {topic.Title}, {video.Name}");
        card.Click += open;
        card.MouseEnter += TopicHover;
        card.MouseLeave += TopicLeave;
        card.GotKeyboardFocus += TopicHover;
        card.LostKeyboardFocus += TopicLeave;
        return card;
    }

    private void ShowGlossary()
    {
        var culture = CultureInfo.GetCultureInfo("uz-Latn-UZ");
        var terms = allTopics.SelectMany(topic => store.ReadGlossary(topic.Number)
            .Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                int separator = line.IndexOfAny(['—', '–']);
                if (separator < 0)
                {
                    int dash = line.IndexOf(" - ", StringComparison.Ordinal);
                    if (dash >= 0) separator = dash + 1;
                }
                if (separator < 0) separator = line.IndexOf(':');
                return separator > 0
                    ? (Topic: topic, Term: line[..separator].Trim(), Definition: line[(separator + 1)..].Trim())
                    : (Topic: topic, Term: line, Definition: "");
            }))
            .OrderBy(entry => entry.Term, StringComparer.Create(culture, true))
            .ThenBy(entry => entry.Topic.Number).ToList();

        var filters = new UniformGrid { Columns = 2, Margin = new Thickness(0, 0, 0, 12) };
        var search = new TextBox
        {
            FontSize = 14, Padding = new Thickness(10, 7, 10, 7),
            Foreground = Brushes.White, Background = new SolidColorBrush(Color.FromRgb(21, 34, 56)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(64, 86, 112)), CaretBrush = Brushes.White
        };
        var topicFilter = new ComboBox { FontSize = 14, Padding = new Thickness(8, 6, 8, 6), MaxDropDownHeight = 360 };
        topicFilter.Items.Add(new ComboBoxItem { Content = "Barcha mavzular" });
        foreach (var topic in allTopics)
        {
            string label = $"{topic.Number}-mavzu · {topic.Title}";
            topicFilter.Items.Add(new ComboBoxItem
            {
                Tag = topic.Number, ToolTip = label,
                Content = new TextBlock { Text = label, TextTrimming = TextTrimming.CharacterEllipsis }
            });
        }
        topicFilter.SelectedIndex = 0;
        System.Windows.Automation.AutomationProperties.SetName(search, "Atama yoki izohni qidirish");
        System.Windows.Automation.AutomationProperties.SetName(topicFilter, "Mavzu bo‘yicha filtr");
        var searchField = new StackPanel { Margin = new Thickness(0, 0, 12, 0) };
        searchField.Children.Add(CourseText("Atama yoki izohni qidirish", 13));
        searchField.Children.Add(search);
        var topicField = new StackPanel();
        topicField.Children.Add(CourseText("Mavzu bo‘yicha filtr", 13));
        topicField.Children.Add(topicFilter);
        filters.Children.Add(searchField);
        filters.Children.Add(topicField);

        var count = CourseText("", 13);
        count.Foreground = new SolidColorBrush(Color.FromRgb(186, 203, 221));
        count.Margin = new Thickness(0, 0, 0, 10);
        var results = new StackPanel();
        SectionContent.Children.Add(filters);
        SectionContent.Children.Add(count);
        SectionContent.Children.Add(results);

        string Normalize(string text) => text.Replace('‘', '\'').Replace('’', '\'').Replace('ʻ', '\'').Replace('ʼ', '\'');
        bool Matches(string value, string query) =>
            culture.CompareInfo.IndexOf(Normalize(value), query, CompareOptions.IgnoreCase) >= 0;

        void Refresh()
        {
            string query = Normalize(search.Text.Trim());
            int? selected = (topicFilter.SelectedItem as ComboBoxItem)?.Tag as int?;
            var matches = terms.Where(entry => (selected is null || entry.Topic.Number == selected)
                && (query.Length == 0 || Matches(entry.Term, query) || Matches(entry.Definition, query))).ToList();
            results.Children.Clear();
            count.Text = $"{matches.Count} ta atama";
            foreach (var entry in matches)
            {
                var accent = new SolidColorBrush((Color)ColorConverter.ConvertFromString(entry.Topic.Accent));
                var body = new StackPanel();
                var title = CourseText(entry.Term, 20);
                title.FontWeight = FontWeights.SemiBold;
                body.Children.Add(title);
                if (entry.Definition.Length > 0)
                {
                    var definition = CourseText(entry.Definition);
                    definition.Margin = new Thickness(0, 5, 0, 0);
                    body.Children.Add(definition);
                }
                var source = CourseText($"{entry.Topic.Number}-mavzu · {entry.Topic.Title}", 13);
                source.Foreground = accent;
                source.Margin = new Thickness(0, 10, 0, 0);
                body.Children.Add(source);
                results.Children.Add(new Border
                {
                    Child = body, Background = new SolidColorBrush(Color.FromRgb(21, 34, 56)),
                    BorderBrush = accent, BorderThickness = new Thickness(3, 0, 0, 0),
                    CornerRadius = new CornerRadius(11), Padding = new Thickness(16), Margin = new Thickness(0, 0, 0, 10)
                });
            }
            if (matches.Count == 0)
                results.Children.Add(CourseText(terms.Count == 0
                    ? "Hozircha glossariyga atama kiritilmagan."
                    : "Qidiruv va filtrga mos atama topilmadi."));
        }

        search.TextChanged += (_, _) => Refresh();
        topicFilter.SelectionChanged += (_, _) => Refresh();
        Refresh();
    }

    private void ShowAssessmentMenu()
    {
        var cards = new UniformGrid { Columns = 2, Margin = new Thickness(0, 12, -16, 0) };
        cards.Children.Add(AssessmentCard(
            "Yakuniy test", "\uE9D5", "#5EEAD4", "#1A3A3F",
            "10 ta savol · 10 ball",
            "Har urinishda 20 ta savoldan 10 tasi tasodifiy tanlanadi. Har bir to‘g‘ri javob 1 ball.",
            "Savollar bittadan chiqadi. Yakunda Medi natijangizni ko‘rsatadi.",
            "Testni boshlash", (_, _) => StartQuiz()));
        cards.Children.Add(AssessmentCard(
            "Yakuniy nazorat", "\uE8A5", "#C4B5FD", "#302B4E",
            "15 ta variant",
            "Har bir variantda 2 ta nazariy savol va 1 ta amaliy topshiriq bor.",
            "Variantni tanlab, javoblarni og‘zaki bayon qilish va topshiriqni amalda bajarish uchun foydalaning.",
            "Variantlarni ko‘rish", (_, _) => ShowVariants()));
        SectionContent.Children.Add(cards);
    }

    private Button AssessmentCard(string title, string glyph, string accentColor, string iconColor,
        string summary, string description, string guidance, string action, RoutedEventHandler onClick)
    {
        var accent = new SolidColorBrush((Color)ColorConverter.ConvertFromString(accentColor));
        var details = new StackPanel();
        details.Children.Add(new Border
        {
            Width = 54, Height = 54, CornerRadius = new CornerRadius(14),
            HorizontalAlignment = HorizontalAlignment.Left,
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(iconColor)),
            Child = new TextBlock
            {
                Text = glyph, FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 24, Foreground = accent,
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center
            }
        });
        var heading = CourseText(title, 24);
        heading.FontWeight = FontWeights.SemiBold;
        heading.Margin = new Thickness(0, 14, 0, 4);
        details.Children.Add(heading);
        var stats = CourseText(summary, 14);
        stats.Foreground = accent;
        stats.FontWeight = FontWeights.SemiBold;
        details.Children.Add(stats);
        foreach (string text in new[] { description, guidance })
        {
            var paragraph = CourseText(text, 14);
            paragraph.Foreground = new SolidColorBrush(Color.FromRgb(186, 203, 221));
            paragraph.Margin = new Thickness(0, 10, 0, 0);
            details.Children.Add(paragraph);
        }

        var body = new DockPanel { MinHeight = 272 };
        var link = CourseText(action + "  →", 15);
        link.Margin = new Thickness(0, 18, 0, 0);
        link.Foreground = accent;
        link.FontWeight = FontWeights.SemiBold;
        DockPanel.SetDock(link, Dock.Bottom);
        body.Children.Add(link);
        body.Children.Add(details);
        var card = new Button
        {
            Content = body, Background = new SolidColorBrush(Color.FromRgb(19, 31, 50)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(44, 60, 84)),
            BorderThickness = new Thickness(1), Padding = new Thickness(24),
            Margin = new Thickness(0, 0, 16, 0), HorizontalContentAlignment = HorizontalAlignment.Stretch
        };
        System.Windows.Automation.AutomationProperties.SetName(card, $"{title} · {action}");
        card.Click += onClick;
        card.MouseEnter += TopicHover;
        card.MouseLeave += TopicLeave;
        card.GotKeyboardFocus += TopicHover;
        card.LostKeyboardFocus += TopicLeave;
        return card;
    }

    private void StartQuiz()
    {
        RunQuiz(CourseActivities.Questions, SectionContent, () => ShowPage("tests"));
    }

    private void RunQuiz(CourseActivities.Question[] bank, StackPanel host, Action back, bool topicQuiz = false)
    {
        const int total = 10;
        bank = bank.ToArray();
        Random.Shared.Shuffle(bank);
        var questions = bank.Take(total).ToArray();
        var options = questions.Select(question => question.Options).ToArray();
        foreach (var choices in options) Random.Shared.Shuffle(choices);
        var answers = new string?[total];
        int index = 0;

        void ShowResult()
        {
            var missed = questions.Where((question, position) => answers[position] != question.Correct)
                .Select(question => question.Topic).Distinct().Order().ToArray();
            int score = questions.Where((question, position) => answers[position] == question.Correct).Count();
            string feedback = score switch
            {
                <= 3 => "Harakat yaxshi boshlandi. Quyidagi mavzularni qayta o‘qib, misollarni ishlab ko‘ring.",
                <= 7 => "Yaxshi natija! Shu mavzularni takrorlasangiz, natijangiz yanada yaxshilanadi.",
                _ => "Barakalla! Fanni juda yaxshi o‘zlashtirdingiz."
            };
            if (score <= 7)
            {
                var review = missed.Take(score <= 3 ? missed.Length : 4);
                feedback += $"\nTakrorlash uchun mavzular: {string.Join(", ", review)}.";
            }
            host.Children.Clear();
            if (!topicQuiz) SectionDescription.Text = "Test yakunlandi";
            if (topicQuiz)
                feedback = score == total ? "Barakalla! Barcha savollarga to‘g‘ri javob berdingiz."
                    : "Quyidagi javoblarni ko‘rib chiqing va mavzuni takrorlang.";
            CourseCard($"Natija · {score} / {total}", feedback, host);

            // Gamified Badge or Certificate Card for high scores
            double percent = (double)score / total * 100.0;
            if (percent >= 70.0)
            {
                var certBorder = new Border
                {
                    Background = new LinearGradientBrush(Color.FromRgb(20, 48, 55), Color.FromRgb(26, 30, 60), 45),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(94, 234, 212)),
                    BorderThickness = new Thickness(1.5),
                    CornerRadius = new CornerRadius(14),
                    Padding = new Thickness(18),
                    Margin = new Thickness(0, 0, 0, 14)
                };
                var certStack = new StackPanel();
                var certHead = new DockPanel();
                var star = CourseText("🏆", 24);
                star.Margin = new Thickness(0, 0, 10, 0);
                DockPanel.SetDock(star, Dock.Left);
                certHead.Children.Add(star);
                var certTitle = CourseText(topicQuiz ? "Mavzu o‘zlashtirildi!" : "Elektron E’tirof Sertifikati", 20);
                certTitle.FontWeight = FontWeights.Bold;
                certTitle.Foreground = new SolidColorBrush(Color.FromRgb(153, 246, 228));
                certHead.Children.Add(certTitle);
                certStack.Children.Add(certHead);

                var certDesc = CourseText(topicQuiz
                    ? $"Siz {currentTopic}-mavzu yuzasidan yuqori natija ({percent:0}%) qayd etdingiz. Nazariy va amaliy tushunchalar mustahkamlandi."
                    : $"Tabriklaymiz! Siz Media va axborot savodxonligi kursi bo‘yicha yakuniy nazoratni {percent:0}% muvaffaqiyat bilan yakunladingiz.", 14);
                certDesc.Foreground = new SolidColorBrush(Color.FromRgb(226, 232, 240));
                certDesc.Margin = new Thickness(0, 8, 0, 0);
                certStack.Children.Add(certDesc);
                certBorder.Child = certStack;
                host.Children.Add(certBorder);
            }
            if (topicQuiz)
            {
                for (int position = 0; position < total; position++)
                {
                    if (answers[position] == questions[position].Correct) continue;
                    CourseCard(questions[position].Prompt,
                        $"Sizning javobingiz — {answers[position]}\nTo‘g‘ri javob — {questions[position].Correct}", host);
                }
            }
            host.Children.Add(CourseButton("Yangi test boshlash", (_, _) => RunQuiz(bank, host, back, topicQuiz)));
            host.Children.Add(CourseButton(topicQuiz ? "← Baholash materiallari" : "← Yakuniy nazorat", (_, _) => back()));
            if (topicQuiz) host.BringIntoView();
            else PageScroll.ScrollToTop();
            if (!topicQuiz) ShowMediMessage($"{total} ta savoldan {score} tasi to‘g‘ri.\n{feedback}", "Natijani ko‘rish");
        }

        void ShowQuestion()
        {
            host.Children.Clear();
            if (topicQuiz)
                host.Children.Add(CourseText($"Mavzu testi · {index + 1} / {total}-savol", 20));
            else
            {
                SectionTitle.Text = "Yakuniy test";
                SectionDescription.Text = $"{index + 1} / {total}-savol";
            }
            host.Children.Add(CourseButton(topicQuiz ? "← Baholash materiallari" : "← Yakuniy nazorat", (_, _) => back()));

            int position = index;
            var card = CourseCard(questions[position].Prompt, "Bitta javobni tanlang.", host);
            var controls = new WrapPanel { Margin = new Thickness(0, 12, 0, 0) };
            var previous = CourseButton("← Oldingi", (_, _) => { index--; ShowQuestion(); });
            previous.IsEnabled = position > 0;
            previous.Margin = new Thickness(0, 6, 10, 0);
            var next = CourseButton(position == total - 1 ? "Testni yakunlash" : "Keyingi →", (_, _) =>
            {
                if (position == total - 1) ShowResult();
                else { index++; ShowQuestion(); }
            });
            next.IsEnabled = answers[position] is not null;
            foreach (string option in options[position])
            {
                var choice = new RadioButton
                {
                    Content = CourseText(option), GroupName = "FinalQuizQuestion",
                    IsChecked = answers[position] == option, Margin = new Thickness(0, 8, 0, 0)
                };
                choice.Checked += (_, _) =>
                {
                    answers[position] = option;
                    next.IsEnabled = true;
                };
                card.Children.Add(choice);
            }
            controls.Children.Add(previous);
            controls.Children.Add(next);
            host.Children.Add(controls);
            if (topicQuiz) host.BringIntoView();
            else PageScroll.ScrollToTop();
        }

        ShowQuestion();
        if (!topicQuiz) ShowMediMessage("Omad! Har bir savolni diqqat bilan o‘qing. Oldingi savolga qaytib, javobingizni o‘zgartirishingiz mumkin.", "Testni boshlash →");
    }

    private void ShowVariants()
    {
        FinishMediWelcome();
        SectionContent.Children.Clear();
        SectionTitle.Text = "Nazorat savollari";
        SectionDescription.Text = "15 ta variant · har birida 2 ta nazariy savol va 1 ta amaliy topshiriq";
        PageScroll.ScrollToTop();
        SectionContent.Children.Add(CourseButton("← Yakuniy nazorat", (_, _) => ShowPage("tests")));
        for (int index = 0; index < CourseActivities.Variants.Length; index++)
        {
            var variant = CourseActivities.Variants[index];
            CourseCard($"{index + 1}-variant", $"1. {variant.First}\n2. {variant.Second}\n\nAmaliy topshiriq\n{variant.Practice}");
        }
    }

    private void ShowAuthor()
    {
        var body = new StackPanel { Margin = new Thickness(24, 20, 24, 22) };

        // 1. Muallif fotosurati - aylana border va neon ramka
        var avatarBorder = new Border
        {
            Width = 118,
            Height = 118,
            CornerRadius = new CornerRadius(59),
            BorderBrush = new SolidColorBrush(Color.FromRgb(94, 234, 212)),
            BorderThickness = new Thickness(2.5),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 4, 0, 18)
        };

        try
        {
            var avatarBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/author.jpg")),
                Stretch = Stretch.UniformToFill
            };
            avatarBorder.Background = avatarBrush;
        }
        catch
        {
            avatarBorder.Background = new SolidColorBrush(Color.FromRgb(22, 36, 55));
        }

        body.Children.Add(avatarBorder);

        // 2. Muallif ma'lumotlari
        body.Children.Add(AuthorField("F.I.O.", "Tohirov To‘lqin Tolibjon o‘g‘li", "\uE77B", Color.FromRgb(94, 234, 212), 20));
        body.Children.Add(AuthorField("Ish joyi", "Buxoro viloyat Peshku tuman 1-son texnikumi", "\uE80F", Color.FromRgb(196, 181, 253), 15));
        body.Children.Add(AuthorField("Lavozimi", "Maxsus fan o‘qituvchisi", "\uE821", Color.FromRgb(253, 186, 116), 15));
        body.Children.Add(AuthorField("Telefon raqam", "+998 99 562 21 14", "\uE717", Color.FromRgb(56, 189, 248), 15));
        body.Children.Add(AuthorField("Elektron pochta", "tulkintakhirov@gmail.com", "\uE715", Color.FromRgb(244, 114, 182), 15));

        var dialog = new Window
        {
            Owner = this,
            Title = "MediaLab · Muallif",
            Width = 520,
            SizeToContent = SizeToContent.Height,
            ResizeMode = ResizeMode.NoResize,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(Color.FromRgb(14, 24, 40)),
            Content = body,
            ShowInTaskbar = false
        };

        var close = CourseButton("Yopish", (_, _) => dialog.Close());
        close.IsCancel = true;
        close.HorizontalAlignment = HorizontalAlignment.Right;
        close.Padding = new Thickness(22, 9, 22, 9);
        close.Margin = new Thickness(0, 4, 0, 0);
        body.Children.Add(close);
        dialog.ShowDialog();
    }

    private static Border AuthorField(string label, string value, string icon, Color color, double valueSize)
    {
        var accent = new SolidColorBrush(color);
        var row = new DockPanel();
        var badge = new Border
        {
            Width = 46, Height = 46, CornerRadius = new CornerRadius(12), Margin = new Thickness(0, 0, 14, 0),
            VerticalAlignment = VerticalAlignment.Center,
            Background = new SolidColorBrush(Color.FromArgb(34, color.R, color.G, color.B)),
            Child = new TextBlock
            {
                Text = icon, FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 21, Foreground = accent,
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center
            }
        };
        DockPanel.SetDock(badge, Dock.Left);
        row.Children.Add(badge);
        var text = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
        var caption = CourseText(label, 12);
        caption.Foreground = accent;
        text.Children.Add(caption);
        var info = CourseText(value, valueSize);
        info.FontWeight = FontWeights.SemiBold;
        text.Children.Add(info);
        row.Children.Add(text);
        return new Border
        {
            Child = row, CornerRadius = new CornerRadius(14), Padding = new Thickness(15),
            Margin = new Thickness(0, 0, 0, 10),
            Background = new SolidColorBrush(Color.FromRgb(22, 36, 55)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(76, color.R, color.G, color.B)),
            BorderThickness = new Thickness(1)
        };
    }
}
