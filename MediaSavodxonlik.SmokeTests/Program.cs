using System.Reflection;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MediaSavodxonlik;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        try
        {
            ApiTests.Run().GetAwaiter().GetResult();
            var app = new App();
            app.InitializeComponent();
            string dataRoot = Path.Combine(AppContext.BaseDirectory, "test-data", Guid.NewGuid().ToString("N"));
            var window = new MainWindow(dataRoot);
            var topics = (ItemsControl)window.FindName("TopicsList");
            var search = (TextBox)window.FindName("TopicSearch");
            void Call(string name, params object[] args) => typeof(MainWindow)
                .GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(window, args);
            void Layout()
            {
                var root = (FrameworkElement)window.Content;
                root.Measure(new Size(1440, 920));
                root.Arrange(new Rect(0, 0, 1440, 920));
                root.UpdateLayout();
                var frame = new DispatcherFrame();
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
                timer.Tick += (_, _) => { timer.Stop(); frame.Continue = false; };
                timer.Start();
                Dispatcher.PushFrame(frame);
            }
            static void Require(bool condition, string message)
            {
                if (!condition) throw new Exception(message);
            }
            void Render(string name)
            {
                Layout();
                var root = (FrameworkElement)window.Content;
                var bitmap = new RenderTargetBitmap(1440, 920, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(root);
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using var output = File.Create(Path.Combine(AppContext.BaseDirectory, name));
                encoder.Save(output);
            }

            var medi = (MediAssistant)window.FindName("Medi");
            void Guide(string method) => typeof(MediAssistant).GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(medi, [medi, new RoutedEventArgs()]);
            medi.SetMotion(true);
            Render("medi-greeting.png");
            for (int step = 0; step < 6; step++)
            {
                Guide("Primary");
                Require(((TextBlock)medi.FindName("StepLabel")).Text.StartsWith($"{step + 1} / 6"), "Wrong guide step");
                if (step == 1) Require(((FrameworkElement)window.FindName("TopicsPanel")).Visibility == Visibility.Visible, "Guide did not open topics");
                if (step == 2) Require(((FrameworkElement)window.FindName("TopicPanel")).Visibility == Visibility.Visible, "Guide did not open a topic");
                if (step == 3) Require(((TextBlock)window.FindName("ResourceTitle")).Text == "Ma’ruza matni", "Guide did not open file resource");
                if (step == 4) Require(((TextBlock)window.FindName("ResourceTitle")).Text == "O‘qitish metodlari", "Guide did not open text resource");
            }
            Guide("Secondary");
            Require(((TextBlock)medi.FindName("StepLabel")).Text.StartsWith("5 / 6"), "Guide previous failed");
            Guide("Primary"); Guide("Primary");
            Require(((FrameworkElement)medi.FindName("Bubble")).Visibility == Visibility.Collapsed, "Guide did not finish");
            Guide("RobotClick");
            Require(((FrameworkElement)medi.FindName("Chat")).Visibility == Visibility.Visible, "Robot did not open topic chat");
            Render("medi-chat.png");
            var chat = (MediChat)medi.FindName("Chat");
            typeof(MediChat).GetMethod("ShowGuide", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(chat, [chat, new RoutedEventArgs()]);
            Require(((FrameworkElement)medi.FindName("Bubble")).Visibility == Visibility.Visible, "Robot did not reopen guide");
            Require(((Button)medi.FindName("PrimaryButton")).Content.ToString() == "Ha, ko‘rsating", "Greeting did not reset");
            Guide("Secondary");
            Require(((FrameworkElement)medi.FindName("Bubble")).Visibility == Visibility.Collapsed, "Later button failed");
            medi.SetMotion(false);
            Require(!((TranslateTransform)((FrameworkElement)medi.FindName("RobotBody")).RenderTransform).HasAnimatedProperties, "Robot animation did not stop");

            for (int pass = 0; pass < 3; pass++)
            {
                Call("ShowPage", "topics");
                Layout();
                Require(topics.Items.Count == 20, "Expected 20 topics");
                for (int i = 0; i < 20; i++)
                {
                    var container = (ContentPresenter)topics.ItemContainerGenerator.ContainerFromIndex(i);
                    container.ApplyTemplate();
                    var button = FindButton(container) ?? throw new Exception("Topic button not generated");
                    // Reproduce WPF's frozen DataTemplate transform from the crash.
                    var frozen = new TranslateTransform();
                    frozen.Freeze();
                    button.RenderTransform = frozen;
                    Call("SetTopicHover", button, true);
                    Call("SetTopicHover", button, false);
                    Call("OpenTopic", button, new RoutedEventArgs());
                    Require(((TextBlock)window.FindName("TopicTitle")).Text == $"{i + 1}-mavzu", "Wrong topic opened");
                    if (pass == 0 && i == 0) Render("topic-page.png");
                    Require(((ItemsControl)window.FindName("TopicResources")).Items.Count == 7, "Expected seven resource types");
                    foreach (string key in new[] { "lecture", "methods", "media", "visuals", "slides", "assessment", "glossary" })
                    {
                        Call("OpenResource", new Button { Tag = key }, new RoutedEventArgs());
                        Require(((TextBlock)window.FindName("ResourceStatus")).Text.StartsWith($"{i + 1}-mavzu /"), "Resource lost topic context");
                        Require(((FrameworkElement)window.FindName("ResourcePage")).Visibility == Visibility.Visible, "Resource page missing");
                        Require(((FrameworkElement)window.FindName("TopicPanel")).Visibility == Visibility.Collapsed, "Resource is embedded instead of a separate page");
                        bool files = ResourceStore.UsesFiles(key);
                        Require((((FrameworkElement)window.FindName("FileArea")).Visibility == Visibility.Visible) == files, "Wrong resource input mode");
                        if (!files)
                        {
                            Require(window.FindName("ResourceEditor") is null, "Public resource editor still exists");
                            Require(window.FindName("UploadButton") is null, "Public upload button still exists");
                            string text = TopicPages.Read(i + 1, key);
                            Require(((TextBlock)window.FindName("PreviewText")).Text == (string.IsNullOrWhiteSpace(text) ? "Bu mavzu uchun material hali tayyorlanmagan." : text), "Code-defined content mismatch");
                            if (pass == 0 && i == 0 && key == "methods") Render("resource-page.png");
                        }
                        Call("ReturnToTopic", window, new RoutedEventArgs());
                    }
                    Require(((Button)window.FindName("PreviousTopic")).IsEnabled == (i > 0), "Wrong previous boundary");
                    Require(((Button)window.FindName("NextTopic")).IsEnabled == (i < 19), "Wrong next boundary");
                    Call("ShowPage", "topics");
                }
                search.Text = "20";
                Layout();
                Require(topics.Items.Count == 1, "Search should find topic 20");
                search.Text = "999";
                Require(topics.Items.Count == 0, "Expected empty search");
                Require(((Border)window.FindName("NoTopics")).Visibility == Visibility.Visible, "Missing empty state");
                search.Clear();
                Call("ToggleMotion", window, new RoutedEventArgs());
                Call("ShowPage", "home");
                Layout();
            }
            var storage = new ResourceStore(dataRoot);
            Directory.CreateDirectory(dataRoot);
            string sample = Path.Combine(dataRoot, "sample.pdf");
            File.WriteAllText(sample, "copy-test");
            storage.ImportAsync(1, "lecture", sample).GetAwaiter().GetResult();
            storage.ImportAsync(1, "lecture", sample).GetAwaiter().GetResult();
            Require(storage.Files(1, "lecture").Length == 2, "Duplicate import overwrote a file");
            Require(storage.Files(2, "lecture").Length == 0, "Files leaked across topics");
            Require(storage.Files(1, "lecture").All(p => File.ReadAllText(p) == "copy-test"), "Imported content corrupted");
            foreach (var fileType in new[] { ("slides", ".pptx"), ("media", ".mp4") })
            {
                string source = Path.Combine(dataRoot, "sample" + fileType.Item2);
                File.WriteAllText(source, "copy-test");
                storage.ImportAsync(1, fileType.Item1, source).GetAwaiter().GetResult();
                Require(storage.Files(1, fileType.Item1).Length == 1, "File import missing");
            }
            bool rejected = false;
            try { storage.ImportAsync(1, "media", sample).GetAwaiter().GetResult(); }
            catch (ArgumentException) { rejected = true; }
            Require(rejected, "Invalid media extension accepted");
            AdminTests.Run(dataRoot);
            ViewerTests.Run(dataRoot);
            window.Close();
            Console.WriteLine("PASS: Medi guide, 60 topics, 420 read-only resource pages, no public editor/upload, admin credentials/catalog, code content, 3 file modes, duplicate import, topic isolation, frozen-transform hover, search and motion toggle.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine(error);
            return 1;
        }
    }

    private static Button? FindButton(DependencyObject parent)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is Button button) return button;
            if (FindButton(child) is { } found) return found;
        }
        return null;
    }
}
