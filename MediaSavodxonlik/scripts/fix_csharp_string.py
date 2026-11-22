with open('MainWindow.xaml.cs', 'r', encoding='utf-8') as f:
    text = f.read()

bad = '''                MessageBox.Show(this, $"Faylni saqlashda xatolik yuz berdi:\n{ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);'''

good = '''                MessageBox.Show(this, "Faylni saqlashda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);'''

if bad in text:
    text = text.replace(bad, good)
    print("Replaced successfully!")
else:
    # Look for the broken multiline string
    broken = 'MessageBox.Show(this, $"Faylni saqlashda xatolik yuz berdi:\n{ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);'
    # Check if there is actual newline
    broken_nl = 'MessageBox.Show(this, $"Faylni saqlashda xatolik yuz berdi:\r\n{ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);'
    if broken in text:
        text = text.replace(broken, good)
        print("Replaced broken!")
    elif broken_nl in text:
        text = text.replace(broken_nl, good)
        print("Replaced broken_nl!")
    else:
        # Regex replacement
        import re
        text = re.sub(r'MessageBox\.Show\(this,\s*\$\"Faylni saqlashda xatolik yuz berdi:.*?, \"Xatolik\"',
                      'MessageBox.Show(this, "Faylni saqlashda xatolik: " + ex.Message, "Xatolik"',
                      text, flags=re.DOTALL)
        print("Replaced via regex!")

with open('MainWindow.xaml.cs', 'w', encoding='utf-8') as f:
    f.write(text)
