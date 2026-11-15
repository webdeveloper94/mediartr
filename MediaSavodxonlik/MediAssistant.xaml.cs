using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MediaSavodxonlik;

public partial class MediAssistant : UserControl
{
    public Func<int, bool>? NavigateStep { get; set; }
    private bool motion;
    private int step = -1;
    private int? chatTopic;
    public Func<int, string>? ContextProvider { get => Chat.ContextProvider; set => Chat.ContextProvider = value; }
    public void Open()
    {
        Bubble.Visibility = Visibility.Collapsed;
        Chat.Visibility = Visibility.Visible;
    }

    public void SetTopic(int? number)
    {
        chatTopic = number;
        Chat.SetTopic(number);
        if (number is null) Chat.Visibility = Visibility.Collapsed;
        else if (step < 0) Bubble.Visibility = Visibility.Collapsed;
    }
    private readonly (string Title, string Text)[] steps =
    [
        ("1. Bosh sahifadan boshlaymiz", "Bu yerda modul haqida ma’lumot va 40 soatlik yuklama ko‘rsatilgan. «Mavzular · 20 ta» tugmasi o‘quv xaritasini ochadi."),
        ("2. Mavzuni toping", "20 ta mavzu kartochkasi bor. Yuqoridagi qidiruvga mavzu raqamini yozing. Kartochkani bosib uning resurslariga kirasiz."),
        ("3. Yetti resurs bir joyda", "Har bir mavzuda ma’ruza, metodlar, media, ko‘rgazmalar, taqdimot, baholash va glossariy bor. Oldingi/keyingi tugmalari bilan mavzuni almashtirasiz."),
        ("4. Fayllarni ichkarida oching", "Ma’ruza, taqdimot va media sahifasida materialni tanlab, «Ochish»ni bosing. Hujjat yoki video dastur ichida ochiladi. Video uchun o‘ynatish, pauza va vaqtni tanlash tugmalari bor."),
        ("5. O‘quv sahifalarini o‘qing", "Metodlar, ko‘rgazmalar, baholash va glossariy tayyor o‘quv sahifalari sifatida ochiladi. Bu sahifalarda matn kiritish shart emas. Mavzu bo‘yicha savol tug‘ilsa, Medi bilan suhbatlashing."),
        ("Tayyor! Endi o‘rganishni boshlang", "«Mavzu resurslariga qaytish» tugmasi bilan ortga qaytasiz. Mavzu tanlanganida meni bosib savol-javobni oching. Suhbatdagi «Yo‘riqnoma» tugmasi tanishtirishni qayta ochadi.")
    ];

    public MediAssistant()
    {
        InitializeComponent();
        Loaded += (_, _) => ResizeChat();
        Chat.Closed += () => Chat.Visibility = Visibility.Collapsed;
        Chat.GuideRequested += () => { Chat.Visibility = Visibility.Collapsed; ResetGreeting(); };
        Loaded += (_, _) => ShowBubble();
        Unloaded += (_, _) => SetMotion(false);
    }

    public void ResizeChat() => Chat.MaxHeight = Math.Max(240, (Window.GetWindow(this)?.ActualHeight ?? 920) - 205);

    internal void CloseWelcomeBubble() => Dismiss(this, new RoutedEventArgs());

    public void SetMotion(bool enabled)
    {
        motion = enabled;
        var body = new TranslateTransform();
        RobotBody.RenderTransform = body;
        var arm = new RotateTransform(-135);
        WaveArm.RenderTransform = arm;
        foreach (var eye in new[] { LeftEye, RightEye })
        {
            var scale = new ScaleTransform(1, 1);
            eye.RenderTransform = scale;
            if (enabled) scale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimationUsingKeyFrames
            {
                RepeatBehavior = RepeatBehavior.Forever,
                KeyFrames = new DoubleKeyFrameCollection
                {
                    new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                    new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.5))),
                    new LinearDoubleKeyFrame(.12, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.6))),
                    new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.75))),
                    new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)))
                }
            });
        }
        if (!enabled) return;
        body.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, -4, TimeSpan.FromSeconds(2.2)) { AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever, EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut } });
        Wave();
    }

    private void Wave()
    {
        if (!motion) return;
        var arm = new RotateTransform(-135);
        WaveArm.RenderTransform = arm;
        arm.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation(-135, -165, TimeSpan.FromMilliseconds(260)) { AutoReverse = true, RepeatBehavior = new RepeatBehavior(3), FillBehavior = FillBehavior.Stop });
    }

    private void ShowBubble()
    {
        Bubble.Visibility = Visibility.Visible;
        Bubble.BeginAnimation(OpacityProperty, null);
        Bubble.Opacity = 1;
        var translate = new TranslateTransform();
        Bubble.RenderTransform = translate;
        if (!motion) return;
        Bubble.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(240)) { FillBehavior = FillBehavior.Stop });
        translate.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(10, 0, TimeSpan.FromMilliseconds(320)) { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }, FillBehavior = FillBehavior.Stop });
    }

    private void Primary(object sender, RoutedEventArgs e)
    {
        if (step == steps.Length - 1) { Dismiss(sender, e); return; }
        MoveTo(step + 1);
    }
    private void Secondary(object sender, RoutedEventArgs e)
    {
        if (step > 0) MoveTo(step - 1);
        else Dismiss(sender, e);
    }
    private void MoveTo(int next)
    {
        if (NavigateStep?.Invoke(next) != true) return;
        step = next;
        Heading.Text = steps[step].Title;
        Explanation.Text = steps[step].Text;
        ProgressArea.Visibility = Visibility.Visible;
        StepLabel.Text = $"{step + 1} / {steps.Length} · FOYDALANISH YO‘RIQNOMASI";
        TourProgress.Value = step + 1;
        PrimaryButton.Content = step == steps.Length - 1 ? "Tushunarli ✓" : "Keyingi →";
        SecondaryButton.Content = step > 0 ? "← Oldingi" : "Yopish";
        ShowBubble();
    }
    private void Dismiss(object sender, RoutedEventArgs e) { Bubble.Visibility = Visibility.Collapsed; step = -1; }
    private void RobotClick(object sender, RoutedEventArgs e)
    {
        if (Bubble.Visibility == Visibility.Visible) { Dismiss(sender, e); return; }
        if (chatTopic is not null)
        {
            Chat.Visibility = Chat.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            return;
        }
        ResetGreeting();
    }
    private void ResetGreeting()
    {
        step = -1;
        Heading.Text = "Salom! Men Medi — yordamchingizman.";
        Explanation.Text = "Dasturdan qanday foydalanishni ko‘rsatib beraymi?";
        PrimaryButton.Content = "Ha, ko‘rsating";
        SecondaryButton.Content = "Keyinroq";
        ProgressArea.Visibility = Visibility.Collapsed;
        ShowBubble(); Wave();
    }
}
