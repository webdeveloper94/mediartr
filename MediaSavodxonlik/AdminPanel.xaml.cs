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
    private bool busy;
    public AdminPanel(TopicCatalog catalog, ResourceStore store)
    {
        this.catalog = catalog; this.store = store;
        InitializeComponent();
        Topics.ItemsSource = catalog.Load(); Topics.SelectedIndex = 0;
        FolderLabel.Text = "Loyiha ichidagi materiallar: " + store.Root;
        Closing += (_, e) => { if (busy) e.Cancel = true; };
    }
    private string Key => (ResourceType.SelectedItem as ComboBoxItem)?.Tag as string ?? "lecture";
    private int Number => (Topics.SelectedItem as TopicEntry)?.Number ?? 1;
    private void ChooseTopic(object sender, SelectionChangedEventArgs e)
    {
        if (TopicName is null || Files is null) return;
        TopicName.Text = (Topics.SelectedItem as TopicEntry)?.Name ?? "";
        RefreshFiles();
    }
    private void ChooseResource(object sender, SelectionChangedEventArgs e) { if (Files is not null) RefreshFiles(); }
    private void RefreshFiles() => Files.ItemsSource = store.Files(Number, Key).Select(Path.GetFileName).ToList();
    private void SaveTopic(object sender, RoutedEventArgs e)
    {
        try { int number = Number; catalog.SaveName(number, TopicName.Text); Topics.ItemsSource = catalog.Load(); Topics.SelectedIndex = number - 1; Status.Text = "✓ Mavzu nomi saqlandi."; }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or JsonException) { Status.Text = "Saqlanmadi: " + ex.Message; }
    }
    private async void ImportFile(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog { Filter = $"Mos fayllar|{ResourceStore.Extensions(Key)}", Title = "Resursni loyihaga yuklash" };
        if (dialog.ShowDialog(this) != true) return;
        busy = true; ImportButton.IsEnabled = Topics.IsEnabled = ResourceType.IsEnabled = SaveButton.IsEnabled = false;
        Status.Text = "Fayl loyiha ichiga nusxalanmoqda…";
        try { await store.ImportAsync(Number, Key, dialog.FileName); RefreshFiles(); Status.Text = "✓ Fayl loyiha ichiga yuklandi."; }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException) { Status.Text = "Yuklanmadi: " + ex.Message; }
        finally { busy = false; ImportButton.IsEnabled = Topics.IsEnabled = ResourceType.IsEnabled = SaveButton.IsEnabled = true; }
    }
}
