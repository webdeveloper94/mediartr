with open('MainWindow.Course.cs', 'r', encoding='utf-8') as f:
    text = f.read()

old_show_author = '''    private void ShowAuthor()
    {
        var body = new StackPanel { Margin = new Thickness(24) };
        body.Children.Add(AuthorField("F.I.O.", "Tohirov To‘lqin Tolibjon o‘g‘li", "\\uE77B", Color.FromRgb(94, 234, 212), 23));
        body.Children.Add(AuthorField("Ish joyi", "Buxoro viloyat Peshku tuman 1-son texnikumi", "\\uE80F", Color.FromRgb(196, 181, 253), 16));
        body.Children.Add(AuthorField("Lavozimi", "Maxsus fan o‘qituvchisi", "\\uE821", Color.FromRgb(253, 186, 116), 16));
        var dialog = new Window
        {
            Owner = this, Title = "MediaLab · Muallif", Width = 500, SizeToContent = SizeToContent.Height,
            ResizeMode = ResizeMode.NoResize, WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(Color.FromRgb(14, 24, 40)), Content = body, ShowInTaskbar = false
        };
        var close = CourseButton("Yopish", (_, _) => dialog.Close());
        close.IsCancel = true;
        close.HorizontalAlignment = HorizontalAlignment.Right;
        close.Padding = new Thickness(20, 9, 20, 9);
        body.Children.Add(close);
        dialog.ShowDialog();
    }'''

new_show_author = '''    private void ShowAuthor()
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
        body.Children.Add(AuthorField("F.I.O.", "Tohirov To‘lqin Tolibjon o‘g‘li", "\\uE77B", Color.FromRgb(94, 234, 212), 20));
        body.Children.Add(AuthorField("Ish joyi", "Buxoro viloyat Peshku tuman 1-son texnikumi", "\\uE80F", Color.FromRgb(196, 181, 253), 15));
        body.Children.Add(AuthorField("Lavozimi", "Maxsus fan o‘qituvchisi", "\\uE821", Color.FromRgb(253, 186, 116), 15));
        body.Children.Add(AuthorField("Telefon raqam", "+998 99 562 21 14", "\\uE717", Color.FromRgb(56, 189, 248), 15));
        body.Children.Add(AuthorField("Elektron pochta", "tulkintakhirov@gmail.com", "\\uE715", Color.FromRgb(244, 114, 182), 15));

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
    }'''

assert old_show_author in text, "old_show_author not found"
text = text.replace(old_show_author, new_show_author)

with open('MainWindow.Course.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("MainWindow.Course.cs updated successfully!")
