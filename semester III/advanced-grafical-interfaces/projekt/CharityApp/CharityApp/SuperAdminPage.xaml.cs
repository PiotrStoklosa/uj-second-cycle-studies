using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace CharityApp
{
    public sealed partial class SuperAdminPage : Page
    {
        private SuperAdmin LoggedInSuperAdmin = App.SuperAdmin;
        public string TotalAmount => App.SuperAdmin.TotalAmount.ToString("F2");

        public SuperAdminPage()
        {
            this.InitializeComponent();
        }

        private void OnAddAdminClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddAdminPage), LoggedInSuperAdmin);
        }

        private void OnViewAdminsClick(object sender, RoutedEventArgs e)
        {

            var admins = App.SuperAdmin.Admins;
            this.Frame.Navigate(typeof(ListAdminsPage), admins);
        }

        private void OnAddTransferClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTransferPage));
        }

        private void OnListGoalsClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListGoalsPage));
        }

        private void OnAddGoalClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddGoalPage));
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }

}