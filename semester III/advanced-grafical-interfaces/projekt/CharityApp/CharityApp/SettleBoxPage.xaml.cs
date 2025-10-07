using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CharityApp
{
    public sealed partial class SettleBoxPage : Page
    {
        private Admin _admin;

        public SettleBoxPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _admin = e.Parameter as Admin;

            if (_admin == null)
            {
                MessageTextBlock.Text = "Nie udało się załadować danych administratora.";
                return;
            }

            LoadVolunteers();
        }

        private void LoadVolunteers()
        {
            VolunteersListView.ItemsSource = null;
            VolunteersListView.ItemsSource = _admin.Volunteers.Select(v => new
            {
                Username = v.Credentials.Username,
                Status = v.BoxStatus,
                Amount = v.CollectedAmount,
                IsSettled = v.IsSettled,
                CanSettle = v.BoxStatus == BoxStatus.Returned && !v.IsSettled
            });
        }

        private void OnSettleClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((Button)sender).DataContext as dynamic;
            var volunteer = _admin.Volunteers.FirstOrDefault(v => v.Credentials.Username == selectedItem.Username);

            if (volunteer != null && volunteer.BoxStatus == BoxStatus.Returned && !volunteer.IsSettled)
            {
                var amountTextBox = ((StackPanel)((Button)sender).Parent).Children.OfType<TextBox>().FirstOrDefault();
                if (amountTextBox != null && double.TryParse(amountTextBox.Text, out double amount))
                {
                    volunteer.CollectedAmount += amount;
                    volunteer.IsSettled = true;
                    volunteer.BoxStatus = BoxStatus.Settled;

                    if (_admin.SuperAdmin != null)
                    {
                        _admin.SuperAdmin.AddToTotalAmount(amount);
                    }

                    MessageTextBlock.Text = $"Wolontariusz {volunteer.Credentials.Username} został rozliczony z kwotą {amount}.";
                    LoadVolunteers();
                }
                else
                {
                    MessageTextBlock.Text = $"Wprowadź prawidłową kwotę dla wolontariusza {volunteer.Credentials.Username}.";
                }
            }
            else
            {
                MessageTextBlock.Text = $"Nie można rozliczyć wolontariusza {volunteer.Credentials.Username}.";
            }
        }


        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
