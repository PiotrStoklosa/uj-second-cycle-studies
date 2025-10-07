using System.Linq;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CharityApp
{
    public sealed partial class AddTransferPage : Page
    {
        public Array CurrencyCodes { get; set; }
        public CurrencyCode SelectedCurrencyCode{ get; set; }

        public AddTransferPage()
        {
            this.InitializeComponent();
            
            CurrencyCodes = Enum.GetValues(typeof(CurrencyCode));
            SelectedCurrencyCode = CurrencyCode.PLN;
            this.DataContext = this;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SuperAdminPage));
        }

        private void OnAddTransferClick(object sender, RoutedEventArgs e)
        {
            double amount;
            if (!double.TryParse(AmountTextBox.Text, out amount))
            {
                MessageTextBlock.Text = $"Nie udało się dodać przelewu! Niepoprawna kwota!";
                AmountTextBox.Text = "";
                return;
            }

            double convertedAmount = Currency.ConvertToPLN((CurrencyCode)CurrencyComboBox.SelectedItem, amount);
            App.SuperAdmin.AddToTotalAmount(convertedAmount);
            MessageTextBlock.Text = $"Dodano przelew od: \"{SenderTextBox.Text}\", Kwota: {AmountTextBox.Text} {CurrencyComboBox.SelectedItem}";
            ResetForm();
        }

        private void ResetForm()
        {
            SenderTextBox.Text = "";
            AmountTextBox.Text = "";
            CurrencyComboBox.SelectedItem = CurrencyCode.PLN;
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
