using System.Collections.Generic;
using System.Xml.Linq;
using Windows.Devices.Enumeration;
using Windows.Networking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CharityApp
{
    public sealed partial class AddAdminPage : Page
    {
        private SuperAdmin currentSuperAdmin;
        public AddAdminPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentSuperAdmin = e.Parameter as SuperAdmin;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SuperAdminPage), currentSuperAdmin);
        }

        private void OnAddAdminClick(object sender, RoutedEventArgs e)
        {
            string firstname = FirstNameTextBox.Text;
            string lastname = LastNameTextBox.Text;
            string region = RegionTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string pesel = PeselTextBox.Text;
            MessageTextBlock.Text = $"Dodano admina: {FirstNameTextBox.Text} {LastNameTextBox.Text}, PESEL: {PeselTextBox.Text}, Okręg: {RegionTextBox.Text}, Login: {LoginTextBox.Text}";
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            PeselTextBox.Text = "";
            RegionTextBox.Text = "";
            LoginTextBox.Text = "";
            PasswordBox.Password = "";

            var admin = new Admin(login, password, currentSuperAdmin, firstname, lastname, pesel, region);

            currentSuperAdmin.AddAdmin(admin);
        }
    }
}