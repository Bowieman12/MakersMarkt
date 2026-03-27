using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MakersMarkt.Data;
using System.Linq; // BELANGRIJK: Zonder dit werkt FirstOrDefault niet!

namespace MakersMarkt
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = "MakersMarkt";

            using var db = new AppDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            contentFrame.Navigate(typeof(HomePage));
            
            // Controleer de status bij het opstarten
            UpdateLoginState(); 
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var tag = args.InvokedItemContainer.Tag?.ToString();

                if (tag == "Home")
                {
                    contentFrame.Navigate(typeof(HomePage));
                }
            }
        }

        // DEZE METHODE VERVANGT UpdateLoginLogoutButtonsVisibility
        public void UpdateLoginState()
        {
            if (App.CurrentUserId > 0)
            {
                // Haal de gebruiker op uit de db
                using var db = new AppDbContext();
                var user = db.Users.FirstOrDefault(u => u.Id == App.CurrentUserId);

                if (user != null)
                {
                    UsernameTextBlock.Text = user.Username; // Zet de naam in de UI
                }

                UserInfoPanel.Visibility = Visibility.Visible;
                LoginButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Visible;
            }
            else
            {
                UserInfoPanel.Visibility = Visibility.Collapsed;
                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Collapsed;
                UsernameTextBlock.Text = ""; // Maak de naam leeg bij uitloggen
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(Pages.Login.LoginPage));
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUserId = 0; 
            
            UpdateLoginState(); 

            contentFrame.Navigate(typeof(HomePage));
        }
    }
}