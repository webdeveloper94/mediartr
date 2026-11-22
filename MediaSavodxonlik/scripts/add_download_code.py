with open('MainWindow.xaml.cs', 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Update SectionOverview visibility logic to also manage ModuleProgramCard visibility
old_vis = 'SectionOverview.Visibility = key == "about" ? Visibility.Visible : Visibility.Collapsed;'
new_vis = '''SectionOverview.Visibility = key == "about" ? Visibility.Visible : Visibility.Collapsed;
            ModuleProgramCard.Visibility = key == "about" ? Visibility.Visible : Visibility.Collapsed;'''

assert old_vis in text, "old_vis not found"
text = text.replace(old_vis, new_vis, 1)

# 2. Add DownloadModuleProgram method
download_method = '''
    private void DownloadModuleProgram(object sender, RoutedEventArgs e)
    {
        string[] candidates =
        [
            Path.Combine(AppContext.BaseDirectory, "Content", "Media axborot.pdf"),
            Path.Combine(AppContext.BaseDirectory, "Media axborot.pdf"),
            Path.Combine(Directory.GetCurrentDirectory(), "Content", "Media axborot.pdf"),
            Path.Combine(Directory.GetCurrentDirectory(), "Media axborot.pdf")
        ];

        string? sourcePath = candidates.FirstOrDefault(File.Exists);
        if (sourcePath is null)
        {
            MessageBox.Show(this, "Modul dasturi PDF fayli topilmadi.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var saveDialog = new Microsoft.Win32.SaveFileDialog
        {
            FileName = "Media va axborot savodxonligi - Modul dasturi.pdf",
            DefaultExt = ".pdf",
            Filter = "PDF hujjati (*.pdf)|*.pdf|Barcha fayllar (*.*)|*.*",
            Title = "Modul dasturini yuklab olish"
        };

        if (saveDialog.ShowDialog() == true)
        {
            try
            {
                File.Copy(sourcePath, saveDialog.FileName, true);
                MessageBox.Show(this, "Modul dasturi muvaffaqiyatli yuklab olindi!", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Faylni saqlashda xatolik yuz berdi:\n{ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
'''

# Insert DownloadModuleProgram right after DownloadCurrentLecture
pos = text.find('private void DownloadCurrentLecture')
assert pos != -1, "DownloadCurrentLecture not found"
# Find end of DownloadCurrentLecture
pos_leave = text.find('private void LeaveReader', pos)
assert pos_leave != -1, "LeaveReader not found"

text = text[:pos_leave] + download_method + "\n" + text[pos_leave:]

with open('MainWindow.xaml.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("MainWindow.xaml.cs updated successfully!")
