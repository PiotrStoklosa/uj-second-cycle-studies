using System.ComponentModel;


public class GoalsListViewModel : INotifyPropertyChanged
{
    private double _totalAmount;
    private double _totalAmountOfGoals;
    private double _totalAmountOfConfirmedGoals;
    private double _remainingAmount;
    private string _amountOfGoalsColor;

    public event PropertyChangedEventHandler PropertyChanged;

    public double TotalAmount
    {
        get => _totalAmount;
        set
        {
            if (_totalAmount != value)
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
    }

    public double TotalAmountOfGoals
    {
        get => _totalAmountOfGoals;
        set
        {
            if (_totalAmountOfGoals != value)
            {
                _totalAmountOfGoals = value;
                OnPropertyChanged(nameof(TotalAmountOfGoals));
            }
        }
    }

    public double TotalAmountOfConfirmedGoals
    {
        get => _totalAmountOfConfirmedGoals;
        set
        {
            if (_totalAmountOfConfirmedGoals != value)
            {
                _totalAmountOfConfirmedGoals = value;
                OnPropertyChanged(nameof(TotalAmountOfConfirmedGoals));
            }
        }
    }

    public double RemainingAmount
    {
        get => _remainingAmount;
        set
        {
            if (_remainingAmount != value)
            {
                _remainingAmount = value;
                OnPropertyChanged(nameof(RemainingAmount));
            }
        }
    }

    public string AmountOfGoalsColor
    {
        get => _amountOfGoalsColor;
        set
        {
            if (_amountOfGoalsColor != value)
            {
                _amountOfGoalsColor = value;
                OnPropertyChanged(nameof(AmountOfGoalsColor));
            }
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
