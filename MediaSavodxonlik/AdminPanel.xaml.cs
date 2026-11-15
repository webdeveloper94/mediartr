using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MediaSavodxonlik;

public partial class AdminPanel : Window
{
    private readonly TopicCatalog catalog;
    private readonly ResourceStore store;
    private readonly Dictionary<int, string> drafts = [];
    private readonly Dictionary<int, string> glossaryDrafts = [];
    private string savedGlossary = "";
    private List<TopicEntry> entries = [];
    private bool loadingSelection;
    private bool busy;

    public AdminPanel(TopicCatalog catalog, ResourceStore store)
    {
        this.catalog = catalog;
        this.store = store;
        InitializeComponent();
        entries = catalog.Load();
        ResourceType.SelectedIndex = 0;
        FilterTopics(1);
        Loaded += (_, _) => TopicSearch.Focus();
        Closing += (_, e) =>
        {
            e.Cancel = busy || ((drafts.Count > 0 || glossaryDrafts.Count > 0) && MessageBox.Show(this,
                "Saqlanmagan o‘zgarishlar bor. Oynani yopasizmi?", Title,
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes);
        };
    }

    private TopicEntry? SelectedTopic => Topics.SelectedItem as TopicEntry;
    private string Key => (string)((ListBoxItem)ResourceType.SelectedItem).Tag;

    private void SearchTopics(object sender, TextChangedEventArgs e)
    {
        if (Topics is not null) FilterTopics(SelectedTopic?.Number);
    }

    private void FilterTopics(int? selected)
    {
        string query = TopicSearch.Text.Trim();
        var matches = entries.Where(topic => topic.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
            || topic.Number.ToString().Contains(query, StringComparison.Ordinal)).ToList();
        loadingSelection = true;
        Topics.ItemsSource = matches;
        Topics.SelectedItem = matches.FirstOrDefault(topic => topic.Number == selected) ?? matches.FirstOrDefault();
        loadingSelection = false;
        if (SelectedTopic is { } topic) Topics.ScrollIntoView(topic);
        NoTopics.Visibility = matches.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ShowSelection();
    }

    private void ChooseTopic(object sender, SelectionChangedEventArgs e)
    {
        if (!loadingSelection) ShowSelection();
    }

    private void ShowSelection()
    {
        var topic = SelectedTopic;
        Editor.Visibility = topic is null ? Visibility.Collapsed : Visibility.Visible;
        Status.Text = topic is not null && (drafts.ContainsKey(topic.Number) || glossaryDrafts.ContainsKey(topic.Number))
            ? "Mavzu nomida saqlanmagan o‘zgarish bor."
            : "";
        if (topic is null) return;

        SelectedTopicLabel.Text = $"{topic.Label} · {topic.Schedule}";
        loadingSelection = true;
        TopicName.Text = drafts.GetValueOrDefault(topic.Number, topic.Name);
        try
        {
            savedGlossary = store.ReadGlossary(topic.Number);
            GlossaryEditor.Text = glossaryDrafts.GetValueOrDefault(topic.Number, savedGlossary);
            GlossaryEditor.IsEnabled = true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            GlossaryEditor.Text = "";
            GlossaryEditor.IsEnabled = false;
            Status.Text = "Glossariy ochilmadi. " + ex.Message;
        }
        loadingSelection = false;
        SaveGlossaryButton.IsEnabled = GlossaryEditor.IsEnabled && glossaryDrafts.ContainsKey(topic.Number);
        SaveButton.IsEnabled = drafts.ContainsKey(topic.Number) && !string.IsNullOrWhiteSpace(TopicName.Text);
        RefreshFiles();
    }

    private void EditName(object sender, TextChangedEventArgs e)
    {
        if (loadingSelection || Topics is null || SelectedTopic is not { } topic) return;

        string name = TopicName.Text;
        if (name == topic.Name) drafts.Remove(topic.Number);
        else drafts[topic.Number] = name;
        SaveButton.IsEnabled = drafts.ContainsKey(topic.Number) && !string.IsNullOrWhiteSpace(name);
        Status.Text = drafts.ContainsKey(topic.Number) ? "Mavzu nomida saqlanmagan o‘zgarish bor." : "";
    }

    private void ChooseResource(object sender, SelectionChangedEventArgs e)
    {
        if (Files is not null && ResourceType.SelectedItem is not null) RefreshFiles();
    }

    private void EditGlossary(object sender, TextChangedEventArgs e)
    {
        if (loadingSelection || Topics is null || SelectedTopic is not { } topic) return;

        string text = GlossaryEditor.Text;
        if (text == savedGlossary) glossaryDrafts.Remove(topic.Number);
        else glossaryDrafts[topic.Number] = text;
        SaveGlossaryButton.IsEnabled = glossaryDrafts.ContainsKey(topic.Number);
    }

    private void SaveGlossary(object sender, RoutedEventArgs e)
    {
        if (SelectedTopic is not { } topic) return;

        try
        {
            store.Save(topic.Number, "glossary", GlossaryEditor.Text);
            savedGlossary = GlossaryEditor.Text;
            glossaryDrafts.Remove(topic.Number);
            SaveGlossaryButton.IsEnabled = false;
            Status.Text = "Glossariy saqlandi.";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Status.Text = "Glossariy saqlanmadi. " + ex.Message;
        }
    }

    private void RefreshFiles()
    {
        if (SelectedTopic is not { } topic) return;

        Formats.Text = "Fayl turlari · " + ResourceStore.Extensions(Key).Replace("*.", "").Replace(";", ", ").ToUpperInvariant();
        Destination.Text = "Nusxa saqlanadigan papka · " + store.Folder(topic.Number, Key);
        try
        {
            var files = store.Files(topic.Number, Key).Select(Path.GetFileName).ToList();
            Files.ItemsSource = files;
            FileCount.Text = files.Count == 0 ? "Bu bo‘limga hali fayl yuklanmagan." : $"{files.Count} ta fayl";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Files.ItemsSource = null;
            FileCount.Text = "Fayllar ro‘yxati ochilmadi. " + ex.Message;
        }
    }

    private void SaveTopic(object sender, RoutedEventArgs e)
    {
        if (SelectedTopic is not { } topic) return;

        try
        {
            catalog.SaveName(topic.Number, TopicName.Text);
            drafts.Remove(topic.Number);
            entries = catalog.Load();
            TopicSearch.Clear();
            FilterTopics(topic.Number);
            Status.Text = "✓ Mavzu nomi saqlandi.";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or JsonException)
        {
            Status.Text = "Nom saqlanmadi. " + ex.Message;
        }
    }

    private async void ImportFile(object sender, RoutedEventArgs e)
    {
        if (busy || SelectedTopic is not { } topic) return;

        string key = Key;
        var dialog = new OpenFileDialog
        {
            Filter = $"Mos fayllar|{ResourceStore.Extensions(key)}",
            Title = "Yuklanadigan fayllarni tanlang",
            Multiselect = true
        };
        if (dialog.ShowDialog(this) != true) return;

        busy = true;
        TopicSearch.IsEnabled = Topics.IsEnabled = Editor.IsEnabled = false;
        int imported = 0;
        var failed = new List<string>();
        try
        {
            foreach (string file in dialog.FileNames)
            {
                Status.Text = $"Yuklanmoqda · {imported + failed.Count + 1}/{dialog.FileNames.Length} · {Path.GetFileName(file)}";
                try
                {
                    await store.ImportAsync(topic.Number, key, file);
                    imported++;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
                {
                    failed.Add($"{Path.GetFileName(file)} — {ex.Message}");
                }
            }
            RefreshFiles();
            Status.Text = $"{imported} ta fayl yuklandi.";
            if (failed.Count > 0) Status.Text += Environment.NewLine + string.Join(Environment.NewLine, failed);
        }
        finally
        {
            busy = false;
            TopicSearch.IsEnabled = Topics.IsEnabled = Editor.IsEnabled = true;
        }
    }

    private void DeleteFile(object sender, RoutedEventArgs e)
    {
        if (busy || SelectedTopic is not { } topic || sender is not Button { Tag: string name }) return;

        string key = Key;
        if (MessageBox.Show(this, $"«{name}» faylini {topic.Number}-mavzudan o‘chirasizmi?\n\nDasturdagi nusxa o‘chiriladi.",
            "Materialni o‘chirish", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) != MessageBoxResult.Yes)
            return;

        try
        {
            store.DeleteFile(topic.Number, key, name);
            Status.Text = $"«{name}» o‘chirildi.";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Status.Text = "Fayl o‘chirilmadi. " + ex.Message;
        }
        RefreshFiles();
    }

    private void ClosePanel(object sender, RoutedEventArgs e) => Close();
}
