using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CharityApp
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (App.SuperAdmin.Credentials.Username == username && App.SuperAdmin.Credentials.Password == password)
            {
                MessageTextBlock.Text = "Logowanie udane jako SuperAdmin!";
                this.Frame.Navigate(typeof(SuperAdminPage));
                return;
            }

            var admin = App.SuperAdmin.Admins.FirstOrDefault(a =>
                a.Credentials.Username == username && a.Credentials.Password == password);
            if (admin != null)
            {
                MessageTextBlock.Text = "Logowanie udane jako Admin!";
                this.Frame.Navigate(typeof(AdminPage), admin);
                return;
            }

            var volunteer = App.SuperAdmin.Admins
                .SelectMany(a => a.Volunteers)
                .FirstOrDefault(v => v.Credentials.Username == username && v.Credentials.Password == password);
            if (volunteer != null)
            {
                MessageTextBlock.Text = "Logowanie udane jako Wolontariusz!";
                this.Frame.Navigate(typeof(VolunteerPage), volunteer);
                return;
            }

            MessageTextBlock.Text = "Niepoprawne dane logowania.";
        }
    }
}
