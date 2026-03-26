using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MakersMarkt.Data;

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
    }
}