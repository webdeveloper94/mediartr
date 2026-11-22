with open('AdminPanel.xaml.cs', 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Update constructor to load system key status
old_loaded = 'Loaded += (_, _) => TopicSearch.Focus();'
new_loaded = '''Loaded += (_, _) =>
        {
            TopicSearch.Focus();
            RefreshSystemKeyStatus();
        };'''

assert old_loaded in text, "old_loaded not found"
text = text.replace(old_loaded, new_loaded)

# 2. Add methods for saving and clearing system key
admin_methods = '''
    private void RefreshSystemKeyStatus()
    {
        bool hasKey = ApiKeyStore.HasSystemKey();
        if (hasKey)
        {
            SystemKeyStatusLabel.Text = "✓ Tizim kaliti o‘rnatilgan va faol";
            SystemKeyStatusLabel.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#5EEAD4")!;
        }
        else
        {
            SystemKeyStatusLabel.Text = "Kalit mavjud emas";
            SystemKeyStatusLabel.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#94A3B8")!;
        }
    }

    private void SaveSystemApiKey(object sender, RoutedEventArgs e)
    {
        string newKey = AdminApiKeyInput.Password.Trim();
        if (string.IsNullOrWhiteSpace(newKey))
        {
            MessageBox.Show(this, "Iltimos, avval API kalitini kiriting.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            ApiKeyStore.SaveSystemKey(newKey);
            AdminApiKeyInput.Clear();
            RefreshSystemKeyStatus();
            Status.Text = "Medi AI tizim kaliti muvaffaqiyatli shifrlanib saqlandi!";
            MessageBox.Show(this, "Tizim kaliti muvaffaqiyatli saqlandi! Endi dastur tarqatilganda barcha foydalanuvchilarda Medi yordamchisi avtomatik ishlaydi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Kalitni saqlashda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ClearSystemApiKey(object sender, RoutedEventArgs e)
    {
        if (!ApiKeyStore.HasSystemKey())
        {
            MessageBox.Show(this, "Hozirda tizim kaliti o‘rnatilmagan.", "Ma’lumot", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (MessageBox.Show(this, "Haqiqatan ham tizim API kalitini o‘chirmoqchimisiz?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            try
            {
                ApiKeyStore.SaveSystemKey("");
                AdminApiKeyInput.Clear();
                RefreshSystemKeyStatus();
                Status.Text = "Tizim API kaliti o‘chirildi.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Kalitni o‘chirishda xatolik: " + ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
'''

# Place before ClosePanel
pos = text.rfind('private void ClosePanel')
assert pos != -1, "ClosePanel not found"
text = text[:pos] + admin_methods + "\n    " + text[pos:]

with open('AdminPanel.xaml.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("AdminPanel.xaml.cs updated successfully!")
