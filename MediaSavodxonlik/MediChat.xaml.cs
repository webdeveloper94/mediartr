using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaSavodxonlik;

public partial class MediChat : UserControl
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(60) };
    public MediChatClient Client { get; set; } = new(Http);
    public Func<int, string>? ContextProvider { get; set; }
    public event Action? Closed;
    public event Action? GuideRequested;
    private readonly Dictionary<int, List<ChatTurn>> history = new();
    private System.Windows.Threading.DispatcherTimer? thinkingTimer;
    private int dotsCount = 0;
    private CancellationTokenSource? pending;
    private int? topic;

    public MediChat()
    {
        InitializeComponent();
        Loaded += (_, _) => UpdateKeyStatus();
        Unloaded += (_, _) => pending?.Cancel();
    }

    public void UpdateKeyStatus()
    {
        bool hasSys = ApiKeyStore.HasSystemKey();
        bool hasEnv = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        if (hasSys || hasEnv)
        {
            SystemKeyStatus.Text = "✓ Tizim API kaliti faol ulangan";
            SystemKeyStatus.Foreground = (Brush)new BrushConverter().ConvertFromString("#5EEAD4")!;
            SystemKeyStatus.Visibility = Visibility.Visible;
        }
        else
        {
            SystemKeyStatus.Text = "⚠️ Tizim kaliti kiritilmagan. Shaxsiy kalit kiriting:";
            SystemKeyStatus.Foreground = (Brush)new BrushConverter().ConvertFromString("#FBBF24")!;
            SystemKeyStatus.Visibility = Visibility.Visible;
        }
    }
    public void SetTopic(int? number)
    {
        if (topic == number) return;
        pending?.Cancel();
        pending = null;
        topic = number;
        Question.Clear();
        Status.Text = "Savolingizni yozing.";
        TopicLabel.Text = number is null ? "Avval mavzuni tanlang" : $"{number}-mavzu bo‘yicha suhbat";
        SetBusy(false);
        RenderHistory();
    }
    private void RenderHistory()
    {
        Conversation.Children.Clear();
        if (topic is int number && history.TryGetValue(number, out var turns))
            foreach (var turn in turns) AddBubble(turn.Role == "user" ? "Siz" : "Medi", turn.Text);
        if (Conversation.Children.Count == 0) AddBubble("Medi", "Mavzu haqida savolingizni yozing. Material yetarli bo‘lmasa, qaysi tushunchani muhokama qilishni aniqlashtiramiz.");
        ConversationScroll.ScrollToEnd();
    }
    private void AddBubble(string author, string text)
    {
        var content = new StackPanel();
        content.Children.Add(new TextBlock { Text = author, FontSize = 10, Foreground = Brushes.Turquoise, Margin = new Thickness(0, 0, 0, 5) });
        content.Children.Add(new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, FontSize = 12, Foreground = Brushes.AliceBlue });
        Conversation.Children.Add(new Border { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(author == "Siz" ? "#203D43" : "#1D3046")), CornerRadius = new CornerRadius(10), Padding = new Thickness(12), Margin = new Thickness(0, 0, 5, 9), Child = content });
    }
    private void SetBusy(bool busy)
    {
        SendButton.IsEnabled = !busy && topic is not null;
        CancelButton.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
        Question.IsReadOnly = busy;
        ThinkingBox.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;

        if (busy)
        {
            dotsCount = 0;
            if (thinkingTimer is null)
            {
                thinkingTimer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(400)
                };
                thinkingTimer.Tick += (_, _) =>
                {
                    dotsCount = (dotsCount + 1) % 4;
                    ThinkingDots.Text = new string('•', dotsCount == 0 ? 1 : dotsCount);
                };
            }
            thinkingTimer.Start();
        }
        else
        {
            thinkingTimer?.Stop();
        }
    }
    private async void SendQuestion(object sender, RoutedEventArgs e) => await SendAsync();
    public async Task SendAsync()
    {
        if (pending is not null || topic is not int number) return;
        string question = Question.Text.Trim();
        if (question.Length == 0) { Status.Text = "Avval savolingizni yozing."; return; }
        string key = ApiKeyStore.ResolveEffectiveKey(ApiKey.Password);
        if (string.IsNullOrWhiteSpace(key)) { Status.Text = "API kalitini sozlash bo‘limiga kalit kiriting."; return; }
        var request = new CancellationTokenSource();
        pending = request;
        SetBusy(true);
        Status.Text = "Medi javob tayyorlamoqda…";
        AddBubble("Siz", question);
        ConversationScroll.ScrollToEnd();
        try
        {
            string context = await Task.Run(() => ContextProvider?.Invoke(number) ?? "Materiallar hali kiritilmagan.", request.Token);
            var turns = history.TryGetValue(number, out var existing) ? existing.ToList() : new List<ChatTurn>();
            string answer = await Client.AskAsync(key, number, context, turns, question, request.Token);
            if (pending != request || topic != number || request.IsCancellationRequested) return;
            turns.Add(new("user", question)); turns.Add(new("assistant", answer));
            history[number] = turns.TakeLast(24).ToList();
            Question.Clear();
            RenderHistory();
            Status.Text = "Javobni o‘quv manbalari bilan tekshirib boring.";
        }
        catch (OperationCanceledException) { if (pending == request) { RenderHistory(); Status.Text = request.IsCancellationRequested ? "So‘rov to‘xtatildi. Savolingiz saqlandi." : "Kutish vaqti tugadi. Qayta urinib ko‘ring."; } }
        catch (Exception ex) when (ex is HttpRequestException or IOException or UnauthorizedAccessException or InvalidOperationException or JsonException)
        {
            if (pending == request) { RenderHistory(); Status.Text = ex is InvalidOperationException ? ex.Message : "Ulanish yoki materialni o‘qishda xato. Internetni tekshirib, qayta urinib ko‘ring."; }
        }
        finally { if (pending == request) { pending = null; SetBusy(false); } request.Dispose(); }
    }
    private void CancelRequest(object sender, RoutedEventArgs e) => pending?.Cancel();
    private void QuickQuestion(object sender, RoutedEventArgs e) { if (pending is null && sender is Button { Tag: string text }) Question.Text = text; }
    private void CloseChat(object sender, RoutedEventArgs e) { pending?.Cancel(); Closed?.Invoke(); }
    private void ShowGuide(object sender, RoutedEventArgs e) { pending?.Cancel(); GuideRequested?.Invoke(); }
}
