using System.IO;
using System.Text.Json;
using System.Windows;
namespace MediaSavodxonlik;
public partial class AdminLogin : Window
{
    private readonly AdminCredentials credentials;
    private int attempts;
    public AdminLogin(AdminCredentials credentials)
    {
        this.credentials = credentials;
        InitializeComponent();
        bool setup = !credentials.IsConfigured;
        Heading.Text = setup ? "Admin parolini o‘rnating" : "Admin panelga kirish";
        Hint.Text = setup ? "Birinchi kirish. Kamida 8 belgili parol yozing va pastki maydonda takrorlang." : "Davom etish uchun admin parolini kiriting.";
        Confirm.Visibility = setup ? Visibility.Visible : Visibility.Collapsed;
        Loaded += (_, _) => Password.Focus();
    }
    private void Login(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!credentials.IsConfigured)
            {
                if (Password.Password != Confirm.Password) { Status.Text = "Parollar bir xil emas."; return; }
                credentials.Create(Password.Password);
            }
            else if (!credentials.Verify(Password.Password))
            {
                Status.Text = "Parol noto‘g‘ri.";
                Password.Clear();
                if (++attempts >= 5) { Status.Text = "5 ta xato urinish. Oynani yopib qayta kiring."; Submit.IsEnabled = false; }
                return;
            }
            Password.Clear(); Confirm.Clear(); DialogResult = true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or InvalidOperationException or JsonException or FormatException) { Status.Text = "Kirish amalga oshmadi: " + ex.Message; }
    }
}
