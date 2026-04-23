using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaApplication1.Context;

namespace AvaloniaApplication1;

public partial class SignInWindow : Window
{
    public SignInWindow()
    {
        InitializeComponent();
    }

    private void SignIn(object? sender, RoutedEventArgs e)
    {
        string login = LoginTextBox.Text;
        string password = PasswordTextBox.Text;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Fields cannot be empty.", true);
            return;
        }

        
        using (var context = new PostgresContext())
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == login || u.Name == login);

            if (user != null && user.Password == password)
            {
                ShowMessage($"Login successful! Welcome, {user.Name}.", false);
            }
            else
            {
                ShowMessage("Incorrect login or password.", true);
            }
        }
    }

    private void ShowMessage(string message, bool isError)
    {
        var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
        if (messageTextBlock != null)
        {
            messageTextBlock.Text = message;
            messageTextBlock.Foreground = isError ? Brushes.Red : Brushes.Green;
        }
    }
}   