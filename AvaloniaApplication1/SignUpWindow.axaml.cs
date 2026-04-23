using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Linq;
using Avalonia.Media;
using AvaloniaApplication1.Context;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1;

public partial class SignUpWindow : Window
{
    public SignUpWindow()
    {
        InitializeComponent();
    }

    private void SignUp(object? sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text;
        string mail = MailTextBox.Text;
        string password = PasswordTextBox.Text;

        
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
        {
            ShowMessage("All fields must be filled in.", true);
            return;
        }

        
        if (!mail.Contains("@"))
        {
            ShowMessage("Incorrect mail format.", true);
            return;
        }

        
        if (password.Length < 6)
        {
            ShowMessage("The password must be at least 6 characters long.", true);
            return;
        }

        
        using (var context = new PostgresContext())
        {
            if (context.Users.Any(u => u.Mail == mail))
            {
                ShowMessage("A user with this email already exists.", true);
                return;
            }

            if (context.Users.Any(u => u.Name == name))
            {
                ShowMessage("A user with this name already exists.", true);
                return;
            }

            
            var user = new User { Name = name, Mail = mail, Password = password };
            context.Users.Add(user);
            context.SaveChanges();
        }

        ShowMessage("Registration successful! You can now log in.", false);

        
        var signInWindow = new SignInWindow();
        signInWindow.Show();
        this.Close();
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