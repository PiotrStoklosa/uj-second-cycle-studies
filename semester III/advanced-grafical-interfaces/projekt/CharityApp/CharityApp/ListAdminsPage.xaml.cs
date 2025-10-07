using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CharityApp
{

    public sealed partial class ListAdminsPage : Page
    {
        private List<Admin> Admins;

        public ListAdminsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is List<Admin> admins)
            {
                Admins = admins;
                LoadAdmins();
            }
        }

        private void LoadAdmins()
        {
            AdminsListView.ItemsSource = Admins;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SuperAdminPage));
        }
    }

}




