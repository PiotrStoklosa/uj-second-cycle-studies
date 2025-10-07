using System;

namespace CharityApp
{
    public class Goal
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Amount { get; private set; }
        public string AccountNumber { get; private set; }
        public bool IsConfirmed { get; set; }

        public Goal(string name, string description, double amount, string accountNumber) : this(name, description, amount, accountNumber, false) { }
       

        public Goal(string name, string description, double amount, string accountNumber, bool isConfirmed)
        {
            Id = Guid.NewGuid();
            this.Name = name;
            Description = description;
            Amount = amount;
            AccountNumber = accountNumber;
            IsConfirmed = isConfirmed;
        }
    }
}

