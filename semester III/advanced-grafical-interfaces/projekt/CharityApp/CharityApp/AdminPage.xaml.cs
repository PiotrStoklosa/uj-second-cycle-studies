using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CharityApp
{
    public sealed partial class AdminPage : Page
    {
        private Admin LoggedInAdmin { get; set; }

        public AdminPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoggedInAdmin = e.Parameter as Admin;

            if (LoggedInAdmin == null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void OnAddVolunteerClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddVolunteerPage), LoggedInAdmin);
        }

        private void OnSubmitAmountClick(object sender, RoutedEventArgs e)
        {
            if (LoggedInAdmin == null)
            {
                return;
            }

            this.Frame.Navigate(typeof(SettleBoxPage), LoggedInAdmin);
        }


        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
