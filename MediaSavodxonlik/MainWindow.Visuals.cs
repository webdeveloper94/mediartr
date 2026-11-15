using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace MediaSavodxonlik;

public partial class MainWindow
{
    private void ShowVisualBoard(VisualLessons.Board board)
    {
        ResourceDescription.Text = allTopics[currentTopic - 1].Title;

        // Container Grid with 2 columns: Left (Interactive Diagram / Flow), Right (Interactive Scenario & Worked Example)
        var grid = new Grid { Margin = new Thickness(0, 4, 0, 16) };
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.15, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });

        var diagram = new StackPanel();
        var details = new StackPanel();

        // 1st Level Surfaces for SmokeTests compatibility (Left = diagram, Right = details)
        var left = BoardSurface(diagram, "#D80F1E2E", "#403ED1C8");
        left.Margin = new Thickness(0, 0, 14, 0);

        var right = BoardSurface(details, "#ED131D33", "#4A7C6BD6");
        Grid.SetColumn(right, 1);

        grid.Children.Add(left);
        grid.Children.Add(right);
        MethodPanel.Children.Add(grid);

        // Header Section in Diagram
        var headerDock = new DockPanel { Margin = new Thickness(0, 0, 0, 14) };
        
        var badgeBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 45, 212, 191)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(45, 212, 191)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(10, 4, 10, 4)
        };
        DockPanel.SetDock(badgeBorder, Dock.Right);
        var badgeText = CourseText(board.DiagramBadge, 11);
        badgeText.FontWeight = FontWeights.Bold;
        badgeText.Foreground = (Brush)new BrushConverter().ConvertFromString("#5EEAD4")!;
        badgeBorder.Child = badgeText;
        headerDock.Children.Add(badgeBorder);

        var mainTitle = BoardHeading(board.Title, "#99F6E4", 23);
        mainTitle.TextWrapping = TextWrapping.Wrap;
        headerDock.Children.Add(mainTitle);

        diagram.Children.Add(headerDock);

        var subtitle = CourseText(board.Subtitle, 14);
        subtitle.Foreground = (Brush)new BrushConverter().ConvertFromString("#94A3B8")!;
        subtitle.Margin = new Thickness(0, 0, 0, 16);
        diagram.Children.Add(subtitle);

        // Visual Accent Palette
        string[] accents = ["#5EEAD4", "#93C5FD", "#C4B5FD", "#FDBA74"];
        string[] icons = ["\uE721", "\uE7BE", "\uE73E", "\uE8FD"]; // Search, Insight, Check, Lightbulb

        string[] labels = board.Labels;
        string[] notes = board.Details;

        // Render Exactly labels.Length cards (3 or 4) so smoke tests and layout stay 100% compliant
        for (int index = 0; index < labels.Length; index++)
        {
            if (index > 0)
            {
                var connectorGrid = new Grid { Margin = new Thickness(24, 2, 0, 2) };
                var line = new Border
                {
                    Width = 2,
                    Height = 18,
                    Background = new SolidColorBrush(Color.FromArgb(120, 148, 163, 184)),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(14, 0, 0, 0)
                };
                var arrow = BoardHeading("↓", "#67E8F9", 14);
                arrow.HorizontalAlignment = HorizontalAlignment.Left;
                arrow.Margin = new Thickness(10, 2, 0, 0);
                connectorGrid.Children.Add(line);
                connectorGrid.Children.Add(arrow);
                diagram.Children.Add(connectorGrid);
            }

            var cardDock = new DockPanel();

            // Left icon / badge container
            var badgeContainer = new Border
            {
                Width = 44,
                Height = 44,
                Background = new SolidColorBrush(Color.FromArgb(40, 255, 255, 255)),
                BorderBrush = (Brush)new BrushConverter().ConvertFromString(accents[index % accents.Length])!,
                BorderThickness = new Thickness(1.5),
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(0, 0, 14, 0)
            };
            DockPanel.SetDock(badgeContainer, Dock.Left);

            var badgeStack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            var badgeNum = BoardHeading($"{index + 1:00}", accents[index % accents.Length], 16);
            badgeNum.HorizontalAlignment = HorizontalAlignment.Center;
            badgeStack.Children.Add(badgeNum);
            badgeContainer.Child = badgeStack;
            cardDock.Children.Add(badgeContainer);

            // Right text container
            var textStack = new StackPanel();
            var stepLabel = BoardHeading(labels[index], accents[index % accents.Length], 17);
            textStack.Children.Add(stepLabel);

            var description = CourseText(notes[index], 15);
            description.Foreground = (Brush)new BrushConverter().ConvertFromString("#E2E8F0")!;
            description.Margin = new Thickness(0, 5, 0, 0);
            textStack.Children.Add(description);

            cardDock.Children.Add(textStack);

            // Surface wrapper for step
            var cardBox = BoardSurface(cardDock, "#8517283C", accents[index % accents.Length]);
            cardBox.Padding = new Thickness(14, 12, 14, 12);
            cardBox.Cursor = Cursors.Hand;
            
            // Interactive hover effect
            cardBox.MouseEnter += (_, _) =>
            {
                cardBox.Background = (Brush)new BrushConverter().ConvertFromString("#AA1E3550")!;
                cardBox.BorderThickness = new Thickness(2);
            };
            cardBox.MouseLeave += (_, _) =>
            {
                cardBox.Background = (Brush)new BrushConverter().ConvertFromString("#8517283C")!;
                cardBox.BorderThickness = new Thickness(1);
            };

            if (index == 0) cardBox.Margin = new Thickness(0, 8, 0, 0);
            diagram.Children.Add(cardBox);
        }

        // Core principle bar at bottom of left panel
        var coreBar = new Border
        {
            Margin = new Thickness(0, 16, 0, 0),
            Padding = new Thickness(12, 8, 12, 8),
            Background = new SolidColorBrush(Color.FromArgb(60, 20, 50, 60)),
            BorderBrush = new SolidColorBrush(Color.FromArgb(100, 94, 234, 212)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8)
        };
        var coreText = CourseText($"💡 Oltin qoida: {board.CoreConcept}", 13);
        coreText.Foreground = (Brush)new BrushConverter().ConvertFromString("#99F6E4")!;
        coreBar.Child = coreText;
        diagram.Children.Add(coreBar);

        // DETAILS COLUMN (RIGHT)
        details.Children.Add(BoardHeading("Ishlangan amaliy vaziyat", "#C4B5FD", 23));

        var scenarioBox = new Border
        {
            Background = (Brush)new BrushConverter().ConvertFromString("#35233E60")!,
            BorderBrush = (Brush)new BrushConverter().ConvertFromString("#5560A5FA")!,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(14, 10, 14, 10),
            Margin = new Thickness(0, 10, 0, 14)
        };
        var scenarioStack = new StackPanel();
        var scenarioTag = BoardHeading("REAl VAZIYAT (KEYS):", "#93C5FD", 12);
        scenarioStack.Children.Add(scenarioTag);

        var scenario = CourseText(board.Scenario, 15);
        scenario.Foreground = (Brush)new BrushConverter().ConvertFromString("#F8FAFC")!;
        scenario.Margin = new Thickness(0, 4, 0, 0);
        scenarioStack.Children.Add(scenario);
        scenarioBox.Child = scenarioStack;
        details.Children.Add(scenarioBox);

        // Worked examples from VisualLessons.WorkedExamples
        string[] examples = VisualLessons.WorkedExamples[currentTopic - 1].Split('|');
        for (int index = 0; index < examples.Length; index++)
        {
            string[] example = examples[index].Split(" — ", 2, StringSplitOptions.None);
            var item = new StackPanel();

            var rowHeader = new DockPanel();
            var title = BoardHeading(example[0], accents[index % accents.Length], 16);
            rowHeader.Children.Add(title);

            // If we have interactive item recommendation, add pill
            if (index < board.InteractiveItems.Length)
            {
                var pill = new Border
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = (Brush)new BrushConverter().ConvertFromString("#330F172A")!,
                    BorderBrush = (Brush)new BrushConverter().ConvertFromString(board.InteractiveItems[index].TagColor)!,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(6, 2, 6, 2)
                };
                var pillTxt = CourseText(board.InteractiveItems[index].Status, 11);
                pillTxt.Foreground = (Brush)new BrushConverter().ConvertFromString(board.InteractiveItems[index].TagColor)!;
                pill.Child = pillTxt;
                rowHeader.Children.Add(pill);
            }
            item.Children.Add(rowHeader);

            var description = CourseText(example[1], 15);
            description.Foreground = (Brush)new BrushConverter().ConvertFromString("#CBD5E1")!;
            description.Margin = new Thickness(0, 4, 0, 0);
            item.Children.Add(description);

            // Additional advice if available
            if (index < board.InteractiveItems.Length && !string.IsNullOrWhiteSpace(board.InteractiveItems[index].Recommendation))
            {
                var rec = CourseText("Tavsiya: " + board.InteractiveItems[index].Recommendation, 13);
                rec.Foreground = (Brush)new BrushConverter().ConvertFromString(accents[index % accents.Length])!;
                rec.Margin = new Thickness(0, 4, 0, 0);
                item.Children.Add(rec);
            }

            var card = BoardSurface(item, "#A2192942", "#485375A3");
            card.Padding = new Thickness(13, 10, 13, 10);
            card.Margin = new Thickness(0, 0, 0, 9);
            
            // Hover effect on worked examples
            card.Cursor = Cursors.Hand;
            card.MouseEnter += (_, _) =>
            {
                card.Background = (Brush)new BrushConverter().ConvertFromString("#BD223859")!;
            };
            card.MouseLeave += (_, _) =>
            {
                card.Background = (Brush)new BrushConverter().ConvertFromString("#A2192942")!;
            };

            details.Children.Add(card);
        }

        // Takeaway / Summary card (4th border child in details for SmokeTests assertion)
        var takeaway = new StackPanel();
        var takeawayHeader = new DockPanel();
        var takeawayGlyph = BoardHeading("✔", "#86EFAC", 16);
        takeawayGlyph.Margin = new Thickness(0, 0, 8, 0);
        DockPanel.SetDock(takeawayGlyph, Dock.Left);
        takeawayHeader.Children.Add(takeawayGlyph);
        takeawayHeader.Children.Add(BoardHeading("Xulosa va amaliy ko‘nikma", "#86EFAC", 16));
        takeaway.Children.Add(takeawayHeader);

        var takeawayBody = CourseText(board.Takeaway, 15);
        takeawayBody.Foreground = (Brush)new BrushConverter().ConvertFromString("#F1F5F9")!;
        takeawayBody.Margin = new Thickness(0, 6, 0, 0);
        takeaway.Children.Add(takeawayBody);

        var takeawayBorder = BoardSurface(takeaway, "#B0133832", "#6648BA94");
        takeawayBorder.Margin = new Thickness(0, 3, 0, 0);
        details.Children.Add(takeawayBorder);
    }

    private static Border BoardSurface(UIElement content, string background, string border) => new()
    {
        Child = content,
        Background = (Brush)new BrushConverter().ConvertFromString(background)!,
        BorderBrush = (Brush)new BrushConverter().ConvertFromString(border)!,
        BorderThickness = new Thickness(1),
        Padding = new Thickness(18),
        CornerRadius = new CornerRadius(14)
    };

    private static TextBlock BoardHeading(string value, string color, double size)
    {
        var heading = CourseText(value, size);
        heading.FontWeight = FontWeights.SemiBold;
        heading.Foreground = (Brush)new BrushConverter().ConvertFromString(color)!;
        return heading;
    }

    private void RenderTopicGlossaryCards(string rawGlossary)
    {
        TextAreaTitle.Text = "Mavzu glossariysi va asosiy tushunchalar";
        GlossaryContainer.Children.Clear();
        GlossaryContainer.Visibility = Visibility.Visible;
        PreviewArea.Visibility = Visibility.Collapsed;

        // Visual presentation of terms
        var lines = rawGlossary.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        // Control bar (Search/Filter + Count badge)
        var toolbar = new DockPanel { Margin = new Thickness(0, 0, 0, 16) };
        
        var countBadge = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(50, 134, 239, 172)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(134, 239, 172)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(12, 6, 12, 6)
        };
        DockPanel.SetDock(countBadge, Dock.Right);
        var countText = CourseText($"{lines.Length} ta muhim atama", 12);
        countText.FontWeight = FontWeights.SemiBold;
        countText.Foreground = (Brush)new BrushConverter().ConvertFromString("#86EFAC")!;
        countBadge.Child = countText;
        toolbar.Children.Add(countBadge);

        // Search within topic glossary
        var searchBox = new TextBox
        {
            FontSize = 13,
            Padding = new Thickness(10, 6, 10, 6),
            Foreground = Brushes.White,
            Background = new SolidColorBrush(Color.FromRgb(18, 28, 45)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(56, 78, 105)),
            CaretBrush = Brushes.White,
            MaxWidth = 360,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        System.Windows.Automation.AutomationProperties.SetName(searchBox, "Mavzu atamalarini qidirish");
        
        var searchPrompt = CourseText("🔍 Atama bo‘yicha tezkor qidiruv:", 13);
        searchPrompt.Foreground = (Brush)new BrushConverter().ConvertFromString("#94A3B8")!;
        searchPrompt.Margin = new Thickness(0, 0, 10, 0);
        searchPrompt.VerticalAlignment = VerticalAlignment.Center;
        
        var searchDock = new StackPanel { Orientation = Orientation.Horizontal };
        searchDock.Children.Add(searchPrompt);
        searchDock.Children.Add(searchBox);
        toolbar.Children.Add(searchDock);

        GlossaryContainer.Children.Add(toolbar);

        var cardsGrid = new StackPanel();
        GlossaryContainer.Children.Add(cardsGrid);

        // Accent cycle for varied visual cards
        string[] accents = ["#86EFAC", "#5EEAD4", "#93C5FD", "#C4B5FD", "#FDBA74"];
        string[] cardBgs = ["#A5112523", "#A510252C", "#A5142338", "#A51C1B38", "#A52B2023"];

        void UpdateCards(string filter)
        {
            cardsGrid.Children.Clear();
            int displayed = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int sep = line.IndexOfAny(['—', '–']);
                if (sep < 0)
                {
                    int dash = line.IndexOf(" - ", StringComparison.Ordinal);
                    if (dash >= 0) sep = dash + 1;
                }
                if (sep < 0) sep = line.IndexOf(':');

                string term = sep > 0 ? line[..sep].Trim() : line;
                string def = sep > 0 ? line[(sep + 1)..].Trim() : "";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    if (!term.Contains(filter, StringComparison.OrdinalIgnoreCase) &&
                        !def.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                displayed++;
                string accent = accents[i % accents.Length];
                string bg = cardBgs[i % cardBgs.Length];

                var cardDock = new DockPanel();

                // Left term number / icon badge
                var leftBadge = new Border
                {
                    Width = 38,
                    Height = 38,
                    Background = new SolidColorBrush(Color.FromArgb(35, 255, 255, 255)),
                    BorderBrush = (Brush)new BrushConverter().ConvertFromString(accent)!,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 14, 0),
                    VerticalAlignment = VerticalAlignment.Top
                };
                DockPanel.SetDock(leftBadge, Dock.Left);

                var badgeTxt = CourseText($"{i + 1:00}", 13);
                badgeTxt.FontWeight = FontWeights.Bold;
                badgeTxt.Foreground = (Brush)new BrushConverter().ConvertFromString(accent)!;
                badgeTxt.HorizontalAlignment = HorizontalAlignment.Center;
                badgeTxt.VerticalAlignment = VerticalAlignment.Center;
                leftBadge.Child = badgeTxt;
                cardDock.Children.Add(leftBadge);

                // Content stack
                var contentStack = new StackPanel();
                var termHeader = BoardHeading(term, accent, 18);
                contentStack.Children.Add(termHeader);

                if (!string.IsNullOrWhiteSpace(def))
                {
                    var defText = CourseText(def, 15);
                    defText.Foreground = (Brush)new BrushConverter().ConvertFromString("#E2E8F0")!;
                    defText.Margin = new Thickness(0, 5, 0, 0);
                    contentStack.Children.Add(defText);
                }

                cardDock.Children.Add(contentStack);

                var cardBorder = BoardSurface(cardDock, bg, accent);
                cardBorder.Padding = new Thickness(16, 12, 16, 12);
                cardBorder.Margin = new Thickness(0, 0, 0, 10);
                cardBorder.Cursor = Cursors.Hand;

                // Interactive hover styling
                cardBorder.MouseEnter += (_, _) =>
                {
                    cardBorder.Background = (Brush)new BrushConverter().ConvertFromString("#BD1E3550")!;
                    cardBorder.BorderThickness = new Thickness(2);
                };
                cardBorder.MouseLeave += (_, _) =>
                {
                    cardBorder.Background = (Brush)new BrushConverter().ConvertFromString(bg)!;
                    cardBorder.BorderThickness = new Thickness(1);
                };

                cardsGrid.Children.Add(cardBorder);
            }

            if (displayed == 0)
            {
                var emptyNotice = CourseText("Qidiruv bo‘yicha mos keluvchi atama topilmadi.", 15);
                emptyNotice.Foreground = (Brush)new BrushConverter().ConvertFromString("#94A3B8")!;
                emptyNotice.Margin = new Thickness(0, 10, 0, 10);
                cardsGrid.Children.Add(emptyNotice);
            }
        }

        UpdateCards("");
        searchBox.TextChanged += (_, _) => UpdateCards(searchBox.Text.Trim());
    }
}
