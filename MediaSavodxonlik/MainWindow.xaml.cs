using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.IO;
using System.Windows.Input;
using System.Text.Json;

namespace MediaSavodxonlik;

public partial class MainWindow : Window
{
    private List<TopicEntry> allTopics = [];
    private readonly TopicCatalog catalog;
    private bool adminOpen;
    private sealed record TopicResource(string Key, string Title, string Icon, string Color, string Description)
    {
        public string ModeLabel => ResourceStore.UsesFiles(Key) ? "Fayl resursi · ichki ko‘ruvchi" : "O‘quv sahifasi · o‘qish uchun";
    }
    private readonly List<TopicResource> resources = new()
    {
        new("lecture", "Ma’ruza matni", "▤", "#5EEAD4", "Nazariy va amaliy mashg‘ulotlar uchun o‘quv materiallari."),
        new("methods", "O‘qitish metodlari", "⌘", "#F4C67A", "Mashg‘ulot metodlari, ularning maqsadi va qo‘llanish tartibi."),
        new("media", "Media materiallar", "▷", "#7DD3FC", "Mavzuga oid video, audio va animatsiya materiallari."),
        new("visuals", "Ko‘rgazmali materiallar", "◈", "#FDBA74", "Didaktik vositalar, ko‘rgazmali misollar va izohlar."),
        new("slides", "Taqdimot", "▣", "#C4B5FD", "Mavzuga oid slaydlar va ko‘rgazmali taqdimot materiallari shu yerda ko‘rsatiladi."),
        new("assessment", "Baholash materiallari", "✓", "#FDA4AF", "Nazorat savollari, amaliy topshiriqlar va testlar."),
        new("glossary", "Glossariy", "Aa", "#A5B4FC", "Mavzuga oid atamalar va ularning tushunarli izohlari.")
    };
    private int currentTopic;
    private string currentResource = "";
    private readonly ResourceStore store;
    private sealed record ResourceFile(string Path) { public string Name => System.IO.Path.GetFileName(Path); }
    private bool motionEnabled = SystemParameters.ClientAreaAnimation;
    private readonly Dictionary<string, (string Title, string Description, string Body)> sections = new()
    {
        ["theory"] = ("Nazariy mashg‘ulotlar", "24 soat · Media va axborot savodxonligi bo‘yicha nazariy ta’lim.", "Bu bo‘limga tasdiqlangan mavzular ro‘yxati, ma’ruza matnlari, o‘qitish natijalari va tegishli taqdimotlar joylashtiriladi. Hozircha o‘quv materiallari kiritilmagan."),
        ["practice"] = ("Amaliy mashg‘ulotlar", "16 soat · Bilimni amalda qo‘llash va mustaqil tahlil.", "Bu bo‘lim amaliy ish yo‘riqnomalari, vaziyatli topshiriqlar va baholash mezonlari uchun tayyorlanmoqda. Topshiriqlar tasdiqlangan modul dasturi asosida kiritiladi."),
        ["media"] = ("Media kutubxona", "Ko‘ring, tinglang va tahlil qiling.", "Mavzularga mos video, audio, animatsiya va ko‘rgazmali materiallar shu yerda jamlanadi. Hozircha media materiallar yuklanmagan."),
        ["tests"] = ("Bilimni sinash", "O‘rganilgan bilimlarni mustahkamlash uchun baholash maydoni.", "Nazorat savollari, testlar va mustaqil topshiriqlar modul mazmuni bilan birga kiritiladi. Hozircha testlar mavjud emas va baholash boshlanmagan."),
        ["glossary"] = ("Glossariy", "Media va axborot olamining asosiy atamalari.", "Modulga oid atamalar va ularning izohlari shu bo‘limga kiritiladi. Atamalar ro‘yxati hozircha yuklanmagan."),
        ["about"] = ("Modul haqida", "30610403 — Kompyuter tarmoqlari va IT-servis", "Fan: Media va axborot savodxonligi.\n\nJami o‘quv yuklamasi: 40 soat.\nNazariy mashg‘ulotlar: 24 soat.\nAmaliy mashg‘ulotlar: 16 soat.\n\nResurs tarkibi: mavzular, ma’ruzalar, o‘qitish metodlari, media, ko‘rgazmali materiallar, taqdimotlar, baholash topshiriqlari va glossariy.\n\nMavzular, yettita resurs, ichki hujjat ko‘ruvchi, media pleyer va Medi yordamchisi mavjud. Baholash materiallari o‘qish uchun taqdim etiladi.")
    };

    public MainWindow() : this(ContentPaths.Root) { }

    public MainWindow(string dataRoot)
    {
        store = new ResourceStore(dataRoot);
        catalog = new TopicCatalog(dataRoot);
        InitializeComponent();
        LectureViewer.UseReadingLayout();
        try { allTopics = catalog.Load(); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        { allTopics = Enumerable.Range(1, 20).Select(n => new TopicEntry(n, "")).ToList(); Loaded += (_, _) => MessageBox.Show(this, "Mavzular ro‘yxati o‘qilmadi: " + ex.Message); }
        TopicsList.ItemsSource = allTopics;
        TopicResources.ItemsSource = resources;
        Medi.NavigateStep = NavigateGuide;
        Medi.ContextProvider = number => TopicContext.Build(store, number);
        SizeChanged += (_, _) => Medi.ResizeChat();
        Loaded += (_, _) => { UpdateMotion(); AnimateEntrance(HomePanel); };
        CommandBindings.Add(new CommandBinding(OpenAdminCommand, OpenAdmin));
        InputBindings.Add(new KeyBinding(OpenAdminCommand, Key.A, ModifierKeys.Control | ModifierKeys.Shift));
        Closing += (_, _) => { Viewer.Reset(); LectureViewer.Reset(); };
    }

    private bool NavigateGuide(int step)
    {
        if (step == 0) return ShowPage("home");
        if (step == 1) { if (!ShowPage("topics")) return false; TopicSearch.Clear(); return true; }
        if (step == 2) return DisplayTopic(currentTopic > 0 ? currentTopic : 1);
        if (step == 3) return SelectResource("lecture");
        if (step == 4) return SelectResource("methods");
        return true;
    }

    private void Navigate(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string key }) return;
        ShowPage(key);
    }

    private bool ShowPage(string key)
    {
        Viewer.Reset();
        LectureViewer.Reset();
        LectureReader.Visibility = Visibility.Collapsed;
        PageScroll.Visibility = Visibility.Visible;
        bool home = key == "home";
        bool topics = key == "topics";
        bool topic = key == "topic";
        bool resourcePage = key == "resource";
        if (!topic && !resourcePage) Medi.SetTopic(null);
        HomePanel.Visibility = home ? Visibility.Visible : Visibility.Collapsed;
        TopicsPanel.Visibility = topics ? Visibility.Visible : Visibility.Collapsed;
        TopicPanel.Visibility = topic ? Visibility.Visible : Visibility.Collapsed;
        ResourcePage.Visibility = resourcePage ? Visibility.Visible : Visibility.Collapsed;
        SectionPanel.Visibility = home || topics || topic || resourcePage ? Visibility.Collapsed : Visibility.Visible;
        TopicsBackButton.Visibility = Visibility.Collapsed;
        HomeNav.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(home ? "#17363C" : "#0E1828"));
        if (home) Breadcrumb.Text = "O‘quv maydoni   /   Bosh sahifa";
        else if (topics) Breadcrumb.Text = "O‘quv maydoni   /   Mavzular";
        else if (sections.TryGetValue(key, out var section))
        {
            SectionTitle.Text = section.Title;
            SectionDescription.Text = section.Description;
            SectionBody.Text = section.Body;
            SectionStatus.Text = key == "about" ? "Modul pasporti" : "Materiallar hali yuklanmagan";
            SectionIcon.Text = key == "about" ? "ⓘ" : "▤";
            Breadcrumb.Text = "O‘quv maydoni   /   " + section.Title;
        }
        // Reset the shared scroll container when switching sections.
        if (HomePanel.Parent is DependencyObject container)
        {
            while (container is not null && container is not ScrollViewer) container = VisualTreeHelper.GetParent(container);
            (container as ScrollViewer)?.ScrollToTop();
        }
        AnimateEntrance(home ? HomePanel : topics ? TopicsPanel : topic ? TopicPanel : resourcePage ? ResourcePage : SectionPanel);
        if (topics) Dispatcher.InvokeAsync(AnimateTopicCards, System.Windows.Threading.DispatcherPriority.Loaded);
        return true;
    }

    private void OpenTopic(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: int number }) return;
        DisplayTopic(number);
    }

    private bool DisplayTopic(int number)
    {
        if (!ShowPage("topic")) return false;
        currentTopic = Math.Clamp(number, 1, 20);
        Medi.SetTopic(currentTopic);
        TopicTitle.Text = allTopics[currentTopic - 1].Title;
        TopicDescription.Text = allTopics[currentTopic - 1].Subtitle;
        TopicEyebrow.Text = $"MEDIA VA AXBOROT SAVODXONLIGI  /  MAVZU {currentTopic:00}";
        TopicNumber.Text = $"{currentTopic:00}";
        PreviousTopic.IsEnabled = currentTopic > 1;
        NextTopic.IsEnabled = currentTopic < 20;
        Breadcrumb.Text = $"O‘quv maydoni   /   Mavzular   /   {currentTopic}-mavzu";
        return true;
    }

    private void ChangeTopic(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string step } && int.TryParse(step, out int delta)) DisplayTopic(currentTopic + delta);
    }

    private void OpenResource(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string key }) SelectResource(key);
    }

    private void ResourceFocusLost(object sender, RoutedEventArgs e) => SetTopicHover(sender, false);

    private bool SelectResource(string key)
    {
        var resource = resources.First(r => r.Key == key);
        if (!ShowPage("resource")) return false;
        currentResource = key;
        ResourceTitle.Text = resource.Title;
        ResourceIcon.Text = resource.Icon;
        ResourceIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(resource.Color));
        ResourceStatus.Text = $"{currentTopic}-mavzu / {resource.Title}";
        ResourceDescription.Text = resource.Description;
        Breadcrumb.Text = $"O‘quv maydoni / {currentTopic}-mavzu / {resource.Title}";
        ResourceMode.Text = ResourceStore.UsesFiles(key) ? "FAYL RESURSI" : "SAHIFA KONTENTI";
        FileArea.Visibility = ResourceStore.UsesFiles(key) ? Visibility.Visible : Visibility.Collapsed;
        TextArea.Visibility = ResourceStore.UsesFiles(key) ? Visibility.Collapsed : Visibility.Visible;
        SaveStatus.Text = "";
        try
        {
            string text = TopicPages.Read(currentTopic, key);
            PreviewText.Text = string.IsNullOrWhiteSpace(text) ? "Bu mavzu uchun material hali tayyorlanmagan." : text;
            if (ResourceStore.UsesFiles(key)) RefreshFiles();
            if (key == "lecture")
            {
                var files = store.Files(currentTopic, key).Select(p => new ResourceFile(p)).ToList();
                LectureFiles.SelectedIndex = -1;
                LectureFiles.ItemsSource = files;
                LectureFiles.Visibility = files.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
                if (files.Count > 0)
                {
                    PageScroll.Visibility = Visibility.Collapsed;
                    LectureReader.Visibility = Visibility.Visible;
                    LectureCaption.Text = allTopics[currentTopic - 1].Title + " · Ma’ruza";
                    LectureFiles.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            SaveStatus.Text = "Resurs o‘qilmadi: " + ex.Message;
        }
        return true;
    }

    private void ReturnToTopic(object sender, RoutedEventArgs e) => DisplayTopic(currentTopic);
    private async void SelectLectureFile(object sender, SelectionChangedEventArgs e)
    {
        if (LectureFiles.SelectedItem is not ResourceFile file || LectureReader.Visibility != Visibility.Visible) return;
        await LectureViewer.OpenAsync(file.Path);
    }
    private static readonly RoutedUICommand OpenAdminCommand = new("Admin", "OpenAdmin", typeof(MainWindow));
    private void OpenAdmin(object sender, ExecutedRoutedEventArgs e)
    {
        if (adminOpen) return;
        adminOpen = true;
        try
        {
            var login = new AdminLogin(new AdminCredentials(store.Root)) { Owner = this };
            if (login.ShowDialog() != true) return;
            new AdminPanel(catalog, store) { Owner = this }.ShowDialog();
            allTopics = catalog.Load();
            SearchTopics(TopicSearch, new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
            if (TopicPanel.Visibility == Visibility.Visible) DisplayTopic(currentTopic);
            if (ResourcePage.Visibility == Visibility.Visible) SelectResource(currentResource);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        { MessageBox.Show(this, "Admin panel ochilmadi: " + ex.Message); }
        finally { adminOpen = false; }
    }
    private void RefreshFiles()
    {
        var files = store.Files(currentTopic, currentResource).Select(p => new ResourceFile(p)).ToList();
        ResourceFiles.ItemsSource = files;
        FileEmpty.Visibility = files.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
    private async void OpenResourceFile(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string path }) return;
        await Viewer.OpenAsync(path);
        AnimateEntrance(Viewer);
        Viewer.BringIntoView();
    }
    private void AnimateEntrance(UIElement panel)
    {
        panel.BeginAnimation(OpacityProperty, null);
        panel.Opacity = 1;
        var transform = new TranslateTransform();
        panel.RenderTransform = transform;
        if (!motionEnabled) return;
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        panel.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(320)) { FillBehavior = FillBehavior.Stop });
        transform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(24, 0, TimeSpan.FromMilliseconds(430)) { EasingFunction = easing, FillBehavior = FillBehavior.Stop });
    }

    private void SearchTopics(object sender, TextChangedEventArgs e)
    {
        if (TopicsList is null || NoTopics is null) return;
        string query = TopicSearch.Text.Trim();
        var matches = allTopics.Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || t.Number.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        TopicsList.ItemsSource = matches;
        TopicCount.Text = query.Length == 0 ? "20 ta mavzu · o‘z qadamingizda o‘rganing" : $"{matches.Count} ta mavzu topildi / 20";
        NoTopics.Visibility = matches.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ClearTopicSearch(object sender, RoutedEventArgs e) => TopicSearch.Clear();

    private void TopicCardLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is ContentPresenter card) AnimateTopicCard(card);
    }

    private void ResourceCardLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is ContentPresenter card) AnimateEntrance(card);
    }

    private void AnimateTopicCards()
    {
        for (int i = 0; i < TopicsList.Items.Count; i++)
            if (TopicsList.ItemContainerGenerator.ContainerFromIndex(i) is ContentPresenter card) AnimateTopicCard(card);
    }

    private void AnimateTopicCard(ContentPresenter card)
    {
        card.BeginAnimation(OpacityProperty, null);
        card.Opacity = 1;
        var transform = new TranslateTransform();
        card.RenderTransform = transform;
        if (!motionEnabled || TopicsPanel.Visibility != Visibility.Visible) return;
        int index = TopicsList.ItemContainerGenerator.IndexFromContainer(card);
        var delay = TimeSpan.FromMilliseconds(Math.Min(Math.Max(index, 0), 8) * 35);
        card.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300)) { BeginTime = delay, FillBehavior = FillBehavior.Stop });
        transform.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(18, 0, TimeSpan.FromMilliseconds(380)) { BeginTime = delay, EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }, FillBehavior = FillBehavior.Stop });
    }

    private void TopicHover(object sender, RoutedEventArgs e) => SetTopicHover(sender, true);
    private void TopicLeave(object sender, RoutedEventArgs e) => SetTopicHover(sender, false);
    private void SetTopicHover(object sender, bool active)
    {
        if (sender is not Button card) return;
        active = active || card.IsMouseOver || card.IsKeyboardFocusWithin;
        card.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active ? "#2DD4BF" : "#2C3C54"));
        card.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active ? "#19313F" : "#131F32"));
        // Keep the button's hit target stationary. Template transforms can be
        // frozen/shared by WPF; animate a fresh, instance-owned content transform.
        if (card.Content is not FrameworkElement content) return;
        double from = content.RenderTransform is TranslateTransform previous ? previous.Y : 0;
        double to = motionEnabled && active ? -3 : 0;
        var transform = new TranslateTransform(0, to);
        content.RenderTransform = transform;
        if (motionEnabled) transform.BeginAnimation(TranslateTransform.YProperty,
            new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(180))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop
            });
    }

    private void ToggleMotion(object sender, RoutedEventArgs e)
    {
        motionEnabled = !motionEnabled;
        UpdateMotion();
        foreach (FrameworkElement panel in new FrameworkElement[] { HomePanel, TopicsPanel, SectionPanel, TopicPanel, ResourcePage, PreviewArea })
        {
            panel.BeginAnimation(OpacityProperty, null);
            panel.Opacity = 1;
            panel.RenderTransform = new TranslateTransform();
        }
        AnimateTopicCards();
    }

    private void UpdateMotion()
    {
        Medi.SetMotion(motionEnabled);
        MotionButton.Content = motionEnabled ? "◉   Animatsiya: yoqilgan" : "○   Animatsiya: o‘chirilgan";
        int index = 0;
        foreach (var element in new[] { FloatOne, FloatTwo, FloatThree })
        {
            var transform = (TranslateTransform)element.RenderTransform;
            transform.BeginAnimation(TranslateTransform.YProperty, null);
            if (motionEnabled)
            {
                transform.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation
                {
                    From = 0, To = index % 2 == 0 ? -7 : 6,
                    Duration = TimeSpan.FromSeconds(2.4 + index * .4),
                    AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever,
                    EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                });
            }
            index++;
        }
    }
}
