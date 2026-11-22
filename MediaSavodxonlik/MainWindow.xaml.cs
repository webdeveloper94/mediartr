using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.IO;
using System.Windows.Input;
using System.Windows.Threading;
using System.Text.Json;

namespace MediaSavodxonlik;

public partial class MainWindow : Window
{
    private List<TopicEntry> allTopics = [];
    private readonly HashSet<int> viewedTopics = [];
    private readonly TopicCatalog catalog;
    private bool adminOpen;
    private List<ResourceFile> inlineVideos = [];
    private string topicList = "topics";
    private sealed record TopicResource(string Key, string Title, string Icon, string Color, string Surface, string Description);
    private readonly List<TopicResource> resources = new()
    {
        new("lecture", "Ma’ruza matni", "\uE8A5", "#5EEAD4", "#20484A", "Nazariy va amaliy mashg‘ulotlar uchun o‘quv materiallari."),
        new("methods", "O‘qitish metodlari", "\uE8FD", "#F4C67A", "#4B4028", "Mashg‘ulot metodlari, ularning maqsadi va qo‘llanish tartibi."),
        new("media", "Media materiallar", "\uE768", "#7DD3FC", "#214458", "Mavzuga oid video, audio va animatsiya materiallari."),
        new("visuals", "Ko‘rgazmali materiallar", "\uEB9F", "#FB923C", "#50352F", "Didaktik vositalar, ko‘rgazmali misollar va izohlar."),
        new("slides", "Taqdimot", "\uE7F4", "#C4B5FD", "#433360", "Mavzuga oid slaydlar va ko‘rgazmali taqdimot materiallari shu yerda ko‘rsatiladi."),
        new("assessment", "Baholash materiallari", "\uE9D5", "#FDA4AF", "#503348", "Nazorat savollari, amaliy topshiriqlar va testlar."),
        new("glossary", "Glossariy", "\uE82D", "#86EFAC", "#284737", "Mavzuga oid atamalar va ularning tushunarli izohlari.")
    };
    private int currentTopic;
    private string currentResource = "";
    private (WindowStyle Style, ResizeMode Resize, WindowState State)? projectionWindow;
    private readonly ResourceStore store;
    private sealed record ResourceFile(string Path)
    {
        public string Name => System.IO.Path.GetFileName(Path);
        public string Format => System.IO.Path.GetExtension(Path).TrimStart('.').ToUpperInvariant();

        private (string Icon, string Accent, string Description) Appearance => Format switch
        {
            "PDF" => ("\uEA90", "#FDA4AF", "Sahifalangan hujjat"),
            "PPTX" => ("\uE7F4", "#C4B5FD", "Slaydli taqdimot"),
            "DOCX" => ("\uE8A5", "#93C5FD", "Word matnli hujjati"),
            "MP4" or "WMV" or "AVI" or "MOV" => ("\uE714", "#5EEAD4", "Ichki pleyerda ijro etiladigan video"),
            "MP3" or "WAV" => ("\uE8D6", "#FCD34D", "Ichki pleyerda tinglanadigan audio"),
            "JPG" or "JPEG" or "PNG" or "GIF" => ("\uEB9F", "#86EFAC", "Ko‘rgazmali tasvir"),
            _ => ("\uE8A5", "#CBD5E1", "Matnli material")
        };
        public string Icon => Appearance.Icon;
        public string Accent => Appearance.Accent;
        public string Description => Appearance.Description;

        public string SizeLabel
        {
            get
            {
                try
                {
                    long bytes = new FileInfo(Path).Length;
                    return bytes switch
                    {
                        >= 1L << 30 => $"{bytes / 1073741824d:0.#} GB",
                        >= 1L << 20 => $"{bytes / 1048576d:0.#} MB",
                        >= 1L << 10 => $"{bytes / 1024d:0.#} KB",
                        _ => $"{bytes} bayt"
                    };
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    return "Hajmi aniqlanmadi";
                }
            }
        }
    }
    private bool motionEnabled = SystemParameters.ClientAreaAnimation;
    private readonly DispatcherTimer welcomeTimer = new() { Interval = TimeSpan.FromSeconds(4) };
    private bool welcomeShown;
    private bool welcomeDocking;
    private readonly Dictionary<string, (string Title, string Description, string Body)> sections = new()
    {
        ["about"] = ("Modul haqida", "30610403 — Kompyuter tarmoqlari va IT-servis", "Fan: Media va axborot savodxonligi.\n\nUKM.4 modulining umumiy yuklamasi 54 soat.\nMediaLab qamrovi 40 soat auditoriya mashg‘uloti (2 soatlik 20 ta mavzu).\nNazariy mashg‘ulotlar 22 soat.\nAmaliy mashg‘ulotlar 18 soat.\nO‘quv rejada mustaqil ta’limga 14 soat ajratilgan.\n\nMustaqil ta’lim bo‘limida 14 ta bir soatlik ish mavjud.\n\nResurs tarkibi: mavzular, ma’ruzalar, o‘qitish metodlari, media, ko‘rgazmali materiallar, taqdimotlar, baholash topshiriqlari va glossariy.\n\nMavzular, yettita resurs, ichki hujjat ko‘ruvchi, media pleyer va Medi yordamchisi mavjud. Baholash materiallari o‘qish uchun taqdim etiladi.")
    };

    public MainWindow() : this(ContentPaths.Root) { }

    public MainWindow(string dataRoot)
    {
        store = new ResourceStore(dataRoot);
        catalog = new TopicCatalog(dataRoot);
        InitializeComponent();
        LectureViewer.UseReadingLayout();
        InlineViewer.UseReadingLayout();
        PreviewKeyDown += HandleProjectionKey;
        try { allTopics = catalog.Load(); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        { allTopics = Enumerable.Range(1, 20).Select(n => new TopicEntry(n, "")).ToList(); Loaded += (_, _) => MessageBox.Show(this, "Mavzular ro‘yxati o‘qilmadi: " + ex.Message); }
        TopicsList.ItemsSource = allTopics;
        TopicResources.ItemsSource = resources;
        Medi.NavigateStep = NavigateGuide;
        Medi.ContextProvider = number => TopicContext.Build(store, number);
        SizeChanged += (_, _) => Medi.ResizeChat();
        Loaded += (_, _) => { UpdateMotion(); AnimateEntrance(HomePanel); };
        ContentRendered += (_, _) => ShowMediWelcome();
        welcomeTimer.Tick += (_, _) => DismissMediWelcome(this, new RoutedEventArgs());
        SizeChanged += (_, _) =>
        {
            if (welcomeDocking) FinishMediWelcome();
            else if (MediWelcomeArea.Visibility == Visibility.Visible) CenterMedi();
        };
        CommandBindings.Add(new CommandBinding(OpenAdminCommand, OpenAdmin));
        InputBindings.Add(new KeyBinding(OpenAdminCommand, Key.A, ModifierKeys.Control | ModifierKeys.Shift));
        Closing += (_, _) => { FinishMediWelcome(); LectureViewer.Reset(); InlineViewer.Reset(); };
    }

    private void ShowMediWelcome()
    {
        if (welcomeShown) return;
        welcomeShown = true;
        ShowMediMessage("Salom! Yangi bilimlarni o‘rganishga tayyormisiz?", "Unda boshladik! →", autoDismiss: true);
    }

    private void ShowMediMessage(string message, string button, bool autoDismiss = false)
    {
        FinishMediWelcome();
        Medi.CloseWelcomeBubble();
        MediMessage.Text = message;
        MediMessageButton.Content = button;
        MediDimOverlay.Visibility = Visibility.Visible;
        MediWelcomeArea.Visibility = Visibility.Visible;
        UpdateLayout();
        CenterMedi();
        Medi.IsHitTestVisible = false;
        MediMessageButton.Focus();
        if (motionEnabled)
        {
            MediDimOverlay.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(250)) { FillBehavior = FillBehavior.Stop });
            Medi.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(220)) { FillBehavior = FillBehavior.Stop });
            MediWelcome.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(220)) { FillBehavior = FillBehavior.Stop });
        }
        if (autoDismiss) welcomeTimer.Start();
    }

    private void CenterMedi()
    {
        Medi.RenderTransform = Transform.Identity;
        Point dockCenter = Medi.TranslatePoint(new Point(Medi.ActualWidth / 2, Medi.ActualHeight / 2), MediWelcomeArea);
        var scale = new ScaleTransform(1.65, 1.65);
        var offset = new TranslateTransform(MediWelcomeArea.ActualWidth / 2 - dockCenter.X,
            MediWelcomeArea.ActualHeight / 2 - dockCenter.Y);
        Medi.RenderTransformOrigin = new Point(.5, .5);
        Medi.RenderTransform = new TransformGroup { Children = { scale, offset } };
    }

    private void DismissMediWelcome(object sender, RoutedEventArgs e)
    {
        if (MediWelcomeArea.Visibility != Visibility.Visible && MediDimOverlay.Visibility != Visibility.Visible) return;
        welcomeTimer.Stop();
        if (!motionEnabled)
        {
            FinishMediWelcome();
            return;
        }
        MediWelcomeArea.Visibility = Visibility.Collapsed;
        var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
        fadeOut.Completed += (_, _) => MediDimOverlay.Visibility = Visibility.Collapsed;
        MediDimOverlay.BeginAnimation(OpacityProperty, fadeOut);
        welcomeDocking = true;
        var transform = (TransformGroup)Medi.RenderTransform;
        var scale = (ScaleTransform)transform.Children[0];
        var offset = (TranslateTransform)transform.Children[1];
        DoubleAnimation DockTo(double target) => new(target, TimeSpan.FromMilliseconds(520))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
        };
        scale.BeginAnimation(ScaleTransform.ScaleXProperty, DockTo(1));
        scale.BeginAnimation(ScaleTransform.ScaleYProperty, DockTo(1));
        offset.BeginAnimation(TranslateTransform.XProperty, DockTo(0));
        var vertical = DockTo(0);
        vertical.Completed += (_, _) => FinishMediWelcome();
        offset.BeginAnimation(TranslateTransform.YProperty, vertical);
    }

    private void FinishMediWelcome()
    {
        welcomeTimer.Stop();
        welcomeDocking = false;
        MediDimOverlay.Visibility = Visibility.Collapsed;
        MediDimOverlay.BeginAnimation(OpacityProperty, null);
        MediWelcomeArea.Visibility = Visibility.Collapsed;
        MediWelcome.BeginAnimation(OpacityProperty, null);
        Medi.BeginAnimation(OpacityProperty, null);
        Medi.RenderTransform = Transform.Identity;
        Medi.IsHitTestVisible = true;
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
        if (key == "author") ShowAuthor();
        else ShowPage(key);
    }

    private bool ShowPage(string key)
    {
        if (MediWelcomeArea.Visibility == Visibility.Visible || welcomeDocking) FinishMediWelcome();
        ExitProjection();
        LectureViewer.Reset();
        InlineViewer.Reset();
        InlineMedia.Visibility = Visibility.Collapsed;
        InlineVideoSelection.ItemsSource = null;
        inlineVideos.Clear();
        LectureReader.Visibility = Visibility.Collapsed;
        LectureFiles.SelectedIndex = -1;
        LectureFiles.ItemsSource = null;
        PageScroll.Visibility = Visibility.Visible;
        bool home = key == "home";
        bool topics = key is "topics" or "theory" or "practice";
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
        UpdateNavSelection(key);
        if (SectionPanel.Visibility == Visibility.Visible)
        {
            SectionContent.Children.Clear();
            SectionOverview.Visibility = key == "about" ? Visibility.Visible : Visibility.Collapsed;
            ModuleProgramCard.Visibility = key == "about" ? Visibility.Visible : Visibility.Collapsed;
        }
        if (home) Breadcrumb.Text = "O‘quv maydoni   /   Bosh sahifa";
        else if (topics)
        {
            if (topicList != key) TopicSearch.Clear();
            topicList = key;
            TopicsHeading.Text = key switch
            {
                "theory" => "Nazariy mashg‘ulotlar",
                "practice" => "Amaliy mashg‘ulotlar",
                _ => "Mavzular"
            };
            TopicsSummary.Text = key == "topics"
                ? "11 ta nazariy va 9 ta amaliy mashg‘ulot"
                : "Har bir mashg‘ulot 2 soat";
            RefreshTopics();
            Breadcrumb.Text = "O‘quv maydoni   /   " + TopicsHeading.Text;
        }
        else if (sections.TryGetValue(key, out var section))
        {
            SectionTitle.Text = section.Title;
            SectionDescription.Text = section.Description;
            SectionBody.Text = section.Body;
            SectionStatus.Text = key == "about" ? "Modul pasporti" : "Materiallar hali yuklanmagan";
            SectionIcon.Text = key == "about" ? "ⓘ" : "▤";
            Breadcrumb.Text = "O‘quv maydoni   /   " + section.Title;
        }
        else if (SectionPanel.Visibility == Visibility.Visible) BuildCourseSection(key);
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

    private void CheckFact(object sender, RoutedEventArgs e)
    {
        bool correct = sender is Button { Tag: "source" };
        FactFeedback.Text = correct
            ? "To‘g‘ri. Ta’lim muassasasining rasmiy kanalini oching va e’lon sanasini tekshiring."
            : "Ko‘p ulashilgan xabar ham tasdiqlanmagan bo‘lishi mumkin. Avval rasmiy e’lonni qidiring.";
        FactResult.Visibility = Visibility.Visible;
        FactResult.BeginAnimation(OpacityProperty, motionEnabled
            ? new DoubleAnimation(0.35, 1, TimeSpan.FromMilliseconds(180)) { FillBehavior = FillBehavior.Stop }
            : null);
        (System.Windows.Automation.Peers.UIElementAutomationPeer.FromElement(FactFeedback)
            ?? System.Windows.Automation.Peers.UIElementAutomationPeer.CreatePeerForElement(FactFeedback))
            ?.RaiseAutomationEvent(System.Windows.Automation.Peers.AutomationEvents.LiveRegionChanged);
    }

    private void OpenFactTopic(object sender, RoutedEventArgs e)
    {
        topicList = "practice";
        TopicSearch.Clear();
        DisplayTopic(10);
    }

    private void ReturnToTopicList(object sender, RoutedEventArgs e) => ShowPage(topicList);

    private void OpenTopic(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: int number }) return;
        DisplayTopic(number);
    }

    private bool DisplayTopic(int number)
    {
        if (!ShowPage("topic")) return false;
        currentTopic = Math.Clamp(number, 1, 20);
        TopicIndependent.Visibility = CourseActivities.Independent.Any(work => work.Topic == currentTopic)
            ? Visibility.Visible : Visibility.Collapsed;
        Medi.SetTopic(currentTopic);
        TopicTitle.Text = allTopics[currentTopic - 1].Title;
        TopicDescription.Text = allTopics[currentTopic - 1].Schedule;
        TopicEyebrow.Text = $"MEDIA VA AXBOROT SAVODXONLIGI  /  MAVZU {currentTopic:00}";
        TopicNumber.Text = $"{currentTopic:00}";
        PreviousTopic.IsEnabled = currentTopic > 1;
        NextTopic.IsEnabled = currentTopic < 20;
        Breadcrumb.Text = $"O‘quv maydoni   /   Mavzular   /   {currentTopic}-mavzu";
        RecordTopicView(currentTopic);
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
        ResourceMode.Text = (ResourceStore.UsesFiles(key) && key != "visuals") ? "FAYL RESURSI" : "SAHIFA KONTENTI";
        FileArea.Visibility = (ResourceStore.UsesFiles(key) && key != "visuals") ? Visibility.Visible : Visibility.Collapsed;
        var lesson = key == "methods" ? TeachingMethods.Find(currentTopic) : null;
        var board = key == "visuals" ? VisualLessons.Find(currentTopic) : null;
        bool interactive = lesson is not null || board is not null || key == "assessment";
        MethodPanel.Visibility = interactive ? Visibility.Visible : Visibility.Collapsed;
        TextArea.Visibility = (ResourceStore.UsesFiles(key) && key != "visuals") || interactive ? Visibility.Collapsed : Visibility.Visible;
        MethodPanel.Children.Clear();
        if (lesson is not null) ShowTeachingMethod(lesson);
        if (board is not null) ShowVisualBoard(board);
        if (key == "assessment") ShowTopicAssessment();
        SaveStatus.Text = "";
        try
        {
            string text = key == "glossary" ? store.ReadGlossary(currentTopic) : TopicPages.Read(currentTopic, key);
            PreviewText.Text = string.IsNullOrWhiteSpace(text) ? "Bu mavzu uchun material hali tayyorlanmagan." : text;
            if (key == "glossary")
            {
                PreviewArea.Visibility = Visibility.Collapsed;
                RenderTopicGlossaryCards(text);
            }
            else
            {
                TextAreaTitle.Text = "O‘quv materiali";
                GlossaryContainer.Visibility = Visibility.Collapsed;
                PreviewArea.Visibility = Visibility.Visible;
            }
            if (ResourceStore.UsesFiles(key)) RefreshFiles();
            if (key == "lecture")
            {
                var files = store.Files(currentTopic, key).Select(p => new ResourceFile(p)).ToList();
                if (files.Count > 0)
                {
                    OpenReader(files, files[0]);
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
        var opening = LectureViewer.OpenAsync(file.Path);
        UpdateProjectionControls();
        await opening;
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
            RefreshTopics();
            if (TopicPanel.Visibility == Visibility.Visible) DisplayTopic(currentTopic);
            if (ResourcePage.Visibility == Visibility.Visible) SelectResource(currentResource);
            if (SectionPanel.Visibility == Visibility.Visible)
            {
                if (SectionTitle.Text == "Glossariy") ShowPage("glossary");
                else if (SectionTitle.Text == "Media kutubxona") ShowPage("media");
                else if (SectionTitle.Text == "Mustaqil ta’lim") ShowPage("independent");
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        { MessageBox.Show(this, "Admin panel ochilmadi: " + ex.Message); }
        finally { adminOpen = false; }
    }
    private void RefreshFiles()
    {
        var files = store.Files(currentTopic, currentResource).Select(p => new ResourceFile(p)).ToList();
        if (currentResource == "media")
        {
            inlineVideos = files.Where(file => file.Format is "MP4" or "WMV" or "AVI" or "MOV").ToList();
            files.RemoveAll(inlineVideos.Contains);
            InlineMedia.Visibility = inlineVideos.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            InlineVideoSelection.ItemsSource = Enumerable.Range(1, inlineVideos.Count).Select(number => $"Video {number}");
            InlineVideoSelection.Visibility = inlineVideos.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
            InlineVideoSelection.SelectedIndex = inlineVideos.Count > 0 ? 0 : -1;
        }
        ResourceFiles.ItemsSource = files;
        FileEmpty.Visibility = files.Count == 0 && inlineVideos.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    private async void SelectInlineVideo(object sender, SelectionChangedEventArgs e)
    {
        int index = InlineVideoSelection.SelectedIndex;
        if (index >= 0 && index < inlineVideos.Count)
            await InlineViewer.OpenAsync(inlineVideos[index].Path, previewVideo: true);
    }

    private void OpenInlineVideoFullscreen(object sender, RoutedEventArgs e)
    {
        int index = InlineVideoSelection.SelectedIndex;
        if (index < 0 || index >= inlineVideos.Count) return;

        OpenReader(inlineVideos, inlineVideos[index]);
        ToggleProjection(sender, e);
    }
    private void OpenResourceFile(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string path }) return;

        var files = store.Files(currentTopic, currentResource).Select(file => new ResourceFile(file)).ToList();
        var selected = new ResourceFile(path);
        if (!files.Contains(selected)) files.Add(selected);
        OpenReader(files, selected);
    }

    private void OpenReader(List<ResourceFile> files, ResourceFile selected)
    {
        InlineViewer.Reset();
        PageScroll.Visibility = Visibility.Collapsed;
        LectureReader.Visibility = Visibility.Visible;
        LectureCaption.Text = allTopics[currentTopic - 1].Title + " · " + (currentResource == "library" ? "Video" : ResourceTitle.Text);
        LectureFiles.ItemsSource = files;
        LectureFiles.Visibility = files.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
        LectureFiles.SelectedIndex = files.IndexOf(selected);
        DownloadLectureButton.Visibility = currentResource == "lecture" ? Visibility.Visible : Visibility.Collapsed;
        ProjectionButton.Focus();
    }

    private void DownloadCurrentLecture(object sender, RoutedEventArgs e)
    {
        if (LectureFiles.SelectedItem is not ResourceFile file || !File.Exists(file.Path))
        {
            MessageBox.Show(this, "Yuklab olish uchun fayl topilmadi.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var saveDialog = new Microsoft.Win32.SaveFileDialog
        {
            FileName = file.Name,
            DefaultExt = Path.GetExtension(file.Path),
            Filter = "Word hujjati (*.docx)|*.docx|Barcha fayllar (*.*)|*.*",
            Title = "Ma’ruza matnini yuklab olish"
        };

        if (saveDialog.ShowDialog() == true)
        {
            try
            {
                File.Copy(file.Path, saveDialog.FileName, true);
                MessageBox.Show(this, "Ma’ruza matni muvaffaqiyatli yuklab olindi!\n\n" + saveDialog.FileName, "Muvaffaqiyatli", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Faylni saqlashda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    
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
                MessageBox.Show(this, "Faylni saqlashda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

private void LeaveReader(object sender, RoutedEventArgs e)
    {
        if (currentResource == "library") ShowPage("media");
        else if (currentResource == "lecture") DisplayTopic(currentTopic);
        else SelectResource(currentResource);
    }

    private void HandleProjectionKey(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F11 && LectureReader.Visibility == Visibility.Visible)
        {
            if (!e.IsRepeat) ToggleProjection(sender, e);
            e.Handled = true;
        }
        else if (e.Key == Key.Escape && projectionWindow is not null)
        {
            ExitProjection();
            e.Handled = true;
        }
        else if (projectionWindow is not null && currentResource == "slides" && Keyboard.Modifiers == ModifierKeys.None
            && !LectureFiles.IsKeyboardFocusWithin && e.Key is Key.Left or Key.Right)
        {
            e.Handled = LectureViewer.MoveSlide(e.Key == Key.Left ? -1 : 1);
        }
    }

    private void UpdateProjectionControls()
    {
        LectureZoom.Visibility = projectionWindow is not null && currentResource == "lecture" && !LectureViewer.IsPdf
            ? Visibility.Visible : Visibility.Collapsed;
        ZoomReset.Content = $"{LectureViewer.DocumentZoom * 100:0}%";
        ProjectionButton.ToolTip = currentResource == "lecture" && LectureViewer.IsPdf
            ? "PDF matnini PDF panelidagi + va − tugmalari bilan kattalashtiring yoki kichraytiring."
            : currentResource == "slides" ? "PPTX slaydlarini ← va → tugmalari bilan almashtiring." : null;
    }

    private void ChangeLectureZoom(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string action }) return;
        double zoom = action == "reset" ? 1 : LectureViewer.DocumentZoom + (action == "+" ? .25 : -.25);
        LectureViewer.SetDocumentZoom(Math.Clamp(zoom, .75, 3));
        UpdateProjectionControls();
    }

    private void ToggleProjection(object sender, RoutedEventArgs e)
    {
        if (projectionWindow is not null)
        {
            ExitProjection();
            return;
        }
        if (LectureReader.Visibility != Visibility.Visible) return;

        projectionWindow = (WindowStyle, ResizeMode, WindowState);
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        WindowState = WindowState.Maximized;
        Grid.SetColumn(LectureReader, 0);
        Grid.SetColumnSpan(LectureReader, 2);
        LectureReader.Margin = new Thickness(0);
        LectureReader.Background = System.Windows.Media.Brushes.Black;
        ReaderToolbar.Margin = new Thickness(12, 4, 12, 4);
        ReaderBack.Visibility = Visibility.Collapsed;
        ProjectionButton.Content = "To‘liq ekrandan chiqish · Esc";
        UpdateProjectionControls();
        ProjectionButton.Focus();
    }

    private void ExitProjection()
    {
        if (projectionWindow is not { } previous) return;

        projectionWindow = null;
        WindowStyle = previous.Style;
        ResizeMode = previous.Resize;
        WindowState = previous.State;
        Grid.SetColumnSpan(LectureReader, 1);
        Grid.SetColumn(LectureReader, 1);
        LectureReader.Margin = new Thickness(24, 90, 24, 18);
        LectureReader.Background = new SolidColorBrush(Color.FromRgb(11, 18, 32));
        ReaderToolbar.Margin = new Thickness(0, 0, 0, 10);
        ReaderBack.Visibility = Visibility.Visible;
        ProjectionButton.Content = "⛶  To‘liq ekran · F11";
        UpdateProjectionControls();
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
        if (TopicsList is not null && NoTopics is not null) RefreshTopics();
    }

    private void RefreshTopics()
    {
        var sectionTopics = allTopics.Where(t => topicList == "topics" || t.IsPractical == (topicList == "practice")).ToList();
        int count = sectionTopics.Count;
        TopicsBadge.Text = $"{count} TA MAVZU";
        HoursBadge.Text = $"{count * 2} SOAT";
        TopicsIllustrationCount.Text = count.ToString();
        string query = TopicSearch.Text.Trim();
        var matches = sectionTopics.Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || t.Number.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        TopicsList.ItemsSource = matches;
        TopicCount.Text = query.Length == 0 ? $"{count} ta mavzu · {count * 2} soat" : $"{matches.Count} ta mavzu topildi / {count}";
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
        var topic = card.DataContext as TopicEntry;
        string highlight = topic?.Accent ?? (card.DataContext is TopicResource resource ? resource.Color : "#2DD4BF");
        bool homeCard = card.Tag is "theory" or "practice" or "tests";
        if (topic is not null && card.Content is Border border)
        {
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active ? highlight : "#354966"));
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active ? "#F0132238" : "#DD111D2E"));
        }
        else
        {
            card.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active
                ? homeCard ? "#BCE5F1FF" : highlight
                : homeCard ? "#59DDE8FF" : "#2C3C54"));
            card.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(homeCard
                ? active ? "#C31D2840" : "#A9131F32"
                : active ? "#19313F" : "#131F32"));
        }
        // Keep the button's hit target stationary. Template transforms can be
        // frozen/shared by WPF; animate a fresh, instance-owned content transform.
        if (card.Content is not FrameworkElement content) return;
        double from = content.RenderTransform is TranslateTransform previous ? previous.Y : 0;
        double to = motionEnabled && active ? -4 : 0;
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
        if (!motionEnabled) FactResult.BeginAnimation(OpacityProperty, null);
        if (!motionEnabled && welcomeDocking) FinishMediWelcome();
    }

    private void RecordTopicView(int topic)
    {
        if (topic >= 1 && topic <= 20)
        {
            viewedTopics.Add(topic);
            CourseProgressBar.Value = viewedTopics.Count;
            int percent = (int)Math.Round((double)viewedTopics.Count / 20.0 * 100);
            ProgressPercentage.Text = $"{percent}%";
            ProgressDetail.Text = $"{viewedTopics.Count} / 20 mavzu ko‘rildi";
        }
    }

    private void UpdateNavSelection(string activeKey)
    {
        var navButtons = new (Button Button, string Key)[]
        {
            (HomeNav, "home"),
            (IndependentNav, "independent"),
            (MediaNav, "media"),
            (TestsNav, "tests"),
            (GlossaryNav, "glossary"),
            (AboutNav, "about"),
            (AuthorNav, "author")
        };

        foreach (var (btn, k) in navButtons)
        {
            bool active = k == activeKey;
            btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(active ? "#17363C" : "#0E1828"));
            btn.Foreground = (Brush)new BrushConverter().ConvertFromString(active ? "#5EEAD4" : "#BACBDD")!;
        }
    }

    private void AskAiAboutTopic(object sender, RoutedEventArgs e)
    {
        if (currentTopic >= 1 && currentTopic <= allTopics.Count)
        {
            Medi.SetTopic(currentTopic);
            Medi.Open();
        }
    }
}
