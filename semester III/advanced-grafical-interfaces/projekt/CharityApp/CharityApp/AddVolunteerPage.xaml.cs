using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CharityApp
{
    public sealed partial class AddVolunteerPage : Page
    {
        private Admin currentAdmin;

        public AddVolunteerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentAdmin = e.Parameter as Admin;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage), currentAdmin);
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string location = LocationTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            DateTime date = DateBox.Date.Date;
            TimeSpan time = TimeBox.Time;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageTextBlock.Text = "Wszystkie pola są wymagane.";
                return;
            }

            var credentials = new UserCredentials(username, password);
            var volunteer = new Volunteer(name, location, ConcatDateTime(date, time), credentials);

            currentAdmin.AddVolunteer(volunteer);

            MessageTextBlock.Text = $"Dodano wolontariusza: {name} ({username})";
            ClearForm();
        }

        private void ClearForm()
        {
            NameTextBox.Text = "";
            LocationTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
        }

        private DateTime ConcatDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
        }
    }
}
