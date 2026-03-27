using MakersMarkt.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            using var db = new AppDbContext();
            var roles = db.Roles.ToList();
            RoleComboBox.ItemsSource = roles;
            RoleComboBox.SelectedValue = roles.FirstOrDefault(r => r.Name == "Player")?.Id;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            var db = new AppDbContext();
            if (string.IsNullOrWhiteSpace(NameTextBlock.Text))
            {
                ErrorTextBlock.Text = "Je moet een naam invullen!";
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBlock.Text))
            {
                ErrorTextBlock.Text = "Je moet een Email invullen!";
                return;
            }

            if (!(RoleComboBox.SelectedValue is int roleId))
            {
                ErrorTextBlock.Text = "Je moet een rol kiezen";
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordPasswordBox.Password))
            {
                ErrorTextBlock.Text = "Je moet een wachtwoord invullen!";
                return;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                ErrorTextBlock.Text = "Bevestig je wachtwoord";
                return;
            }

            if (ConfirmPasswordBox.Password != PasswordPasswordBox.Password)
            {
                ErrorTextBlock.Text = "Wachtwoorden komen niet overeen";
                return;
            }

            var myUser = new MakersMarkt.Data.Models.User()
            {
                Username = NameTextBlock.Text,
                Email = EmailTextBlock.Text,
                RoleId = roleId,
                Password = BCrypt.Net.BCrypt.HashPassword(PasswordPasswordBox.Password)
            };
            db.Add(myUser);
            db.SaveChanges();

            Frame.Navigate(typeof(LoginPage));

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }


    }
}
