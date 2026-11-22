with open('MediChat.xaml.cs', 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Add DispatcherTimer for dots animation in MediChat fields
fields_target = '    private readonly Dictionary<int, List<ChatTurn>> history = new();'
fields_replacement = '''    private readonly Dictionary<int, List<ChatTurn>> history = new();
    private System.Windows.Threading.DispatcherTimer? thinkingTimer;
    private int dotsCount = 0;'''

assert fields_target in text, "fields_target not found"
text = text.replace(fields_target, fields_replacement, 1)

# 2. Update SetBusy to handle ThinkingBox and timer
setbusy_target = '''    private void SetBusy(bool busy)
    {
        SendButton.IsEnabled = !busy && topic is not null;
        CancelButton.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
        Question.IsReadOnly = busy;
    }'''

setbusy_replacement = '''    private void SetBusy(bool busy)
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
    }'''

assert setbusy_target in text, "setbusy_target not found"
text = text.replace(setbusy_target, setbusy_replacement, 1)

with open('MediChat.xaml.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("MediChat.xaml.cs updated with thinkingTimer and ThinkingBox animation!")
