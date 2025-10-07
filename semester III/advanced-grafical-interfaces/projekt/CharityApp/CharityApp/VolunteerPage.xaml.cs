using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CharityApp
{
    public sealed partial class VolunteerPage : Page
    {
        public Volunteer CurrentVolunteer { get; set; }

        public VolunteerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CurrentVolunteer = e.Parameter as Volunteer ?? GetDummyVolunteer();
            UpdateButtonState();
            LoadVolunteerInfo();
        }

        private void UpdateButtonState()
        {
            switch (CurrentVolunteer.BoxStatus)
            {
                case BoxStatus.NotPicked:
                    PickupButton.Visibility = Visibility.Visible;
                    ReturnButton.Visibility = Visibility.Collapsed;
                    MessageTextBlock.Text = "";
                    break;

                case BoxStatus.Picked:
                    PickupButton.Visibility = Visibility.Collapsed;
                    ReturnButton.Visibility = Visibility.Visible;
                    MessageTextBlock.Text = "";
                    break;

                case BoxStatus.Returned:
                    PickupButton.Visibility = Visibility.Collapsed;
                    ReturnButton.Visibility = Visibility.Collapsed;
                    MessageTextBlock.Text = "Zakończono zbiórkę!";
                    break;
            }
        }

        private void LoadVolunteerInfo()
        {
            this.DataContext = CurrentVolunteer;
        }

        private void OnPickupButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentVolunteer.BoxStatus = BoxStatus.Picked;
            UpdateButtonState();
        }

        private void OnReturnButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentVolunteer.BoxStatus = BoxStatus.Returned;
            UpdateButtonState();
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private Volunteer GetDummyVolunteer()
        {
            return new Volunteer("wolontariusz", "Lokalizacja", new DateTime(2020, 1, 1), new UserCredentials("wolontariusz", "haslo"));
        }
    }
}