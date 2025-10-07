using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CharityApp
{
    public sealed partial class ListGoalsPage : Page
    {
        private GoalsListViewModel GoalsListViewModel { get; } = new GoalsListViewModel();
        private SuperAdmin superAdmin;

        public ListGoalsPage()
        {
            this.InitializeComponent();

            this.DataContext = GoalsListViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            superAdmin = App.SuperAdmin;
            LoadGoals();
        }

        private void LoadGoals()
        {
            // Load other properties
            GoalsListViewModel.TotalAmount = superAdmin.TotalAmount;

            GoalsListViewModel.TotalAmountOfGoals = 0.0;
            GoalsListViewModel.TotalAmountOfConfirmedGoals = 0.0;
            foreach (var goal in superAdmin.Goals)
            {
                GoalsListViewModel.TotalAmountOfGoals += goal.Amount;
                if (goal.IsConfirmed)
                {
                    GoalsListViewModel.TotalAmountOfConfirmedGoals += goal.Amount;
                }
            }

            GoalsListViewModel.RemainingAmount = GoalsListViewModel.TotalAmount - GoalsListViewModel.TotalAmountOfConfirmedGoals;
            GoalsListViewModel.AmountOfGoalsColor = GoalsListViewModel.TotalAmount < GoalsListViewModel.TotalAmountOfGoals ? "Red" : "Green";


            // Load goals
            GoalsListView.ItemsSource = null;
            GoalsListView.ItemsSource = superAdmin.Goals
                .OrderBy(g => g.IsConfirmed)
                .ThenBy(g => g.Amount)
                .Select(g => new
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                Amount = g.Amount,
                AccountNumber = g.AccountNumber,
                IsConfirmed = g.IsConfirmed,
                ButtonText = g.IsConfirmed ? "Anuluj" : "Potwierdź",
                ButtonColor = g.IsConfirmed ? "Orange" : "Green",
                IsButtonEnabled = g.IsConfirmed || GoalsListViewModel.RemainingAmount >= g.Amount
            });
        }

        private void OnConfirmClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedItem = button.DataContext as dynamic;

            Goal goal = superAdmin.GetGoalById(selectedItem.Id);
            goal.IsConfirmed = !goal.IsConfirmed;

            LoadGoals();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedItem = button.DataContext as dynamic;

            Goal goal = superAdmin.GetGoalById(selectedItem.Id);
            superAdmin.DeleteGoal(goal);
         
            LoadGoals();
        }


        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
