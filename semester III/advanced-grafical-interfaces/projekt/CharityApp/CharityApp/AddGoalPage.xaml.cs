using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace CharityApp
{
    public sealed partial class AddGoalPage : Page
    {
        public AddGoalPage()
        {
            this.InitializeComponent();
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SuperAdminPage));
        }

        private void OnAddGoalClick(object sender, RoutedEventArgs e)
        {
            double amount;
            if (!double.TryParse(AmountTextBox.Text, out amount))
            {
                MessageTextBlock.Text = $"Nie udało się dodać przelewu! Niepoprawna kwota!";
                AmountTextBox.Text = "";
                return;
            }

            Goal newGoal = new Goal
            (
                GoalNameTextBox.Text,
                DescriptionTextBox.Text,
                amount,
                BankAccountTextBox.Text
            );

            App.SuperAdmin.AddGoal(newGoal);

            MessageTextBlock.Text = $"Dodano cel: \"{GoalNameTextBox.Text}\" na kwotę: {AmountTextBox.Text} PLN";
            ResetForm();
        }

        private void ResetForm()
        {
            GoalNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            AmountTextBox.Text = "";
            BankAccountTextBox.Text = "";
        }

        private void AmountTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!decimal.TryParse(textBox.Text, out _) && !string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == ',').ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
    }
}
