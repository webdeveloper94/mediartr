with open('MediChat.xaml.cs', 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Update constructor to update status indicator
old_ctor = '''    public MediChat()
    {
        InitializeComponent();
        Unloaded += (_, _) => pending?.Cancel();
    }'''

new_ctor = '''    public MediChat()
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
    }'''

assert old_ctor in text, "old_ctor not found"
text = text.replace(old_ctor, new_ctor)

# 2. Update SendAsync key resolution
old_key = 'string key = string.IsNullOrWhiteSpace(ApiKey.Password) ? Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "" : ApiKey.Password;'
new_key = 'string key = ApiKeyStore.ResolveEffectiveKey(ApiKey.Password);'

assert old_key in text, "old_key not found"
text = text.replace(old_key, new_key)

with open('MediChat.xaml.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("MediChat.xaml.cs updated successfully!")
