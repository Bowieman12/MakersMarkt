using MakersMarkt.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MakersMarkt.Pages.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new AppDbContext();
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                ErrorTextBlock.Text = "Gebruikersnaam moet ingevuld zijn!";
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordPasswordBox.Password)){
                ErrorTextBlock.Text = "Wachtwoord moet ingevuld zijn!";
            }

            var user = db.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Username == UsernameTextBox.Text);

            if (user == null || !BCrypt.Net.BCrypt.Verify(PasswordPasswordBox.Password, user.Password))
            {
                ErrorTextBlock.Text = "Invalid username or password!";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            App.CurrentUserId = user.Id;

            Frame.Navigate(typeof(HomePage));
        }

        
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
