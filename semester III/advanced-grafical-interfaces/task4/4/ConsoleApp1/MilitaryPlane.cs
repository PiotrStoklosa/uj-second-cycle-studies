using System;

class MilitaryPlane : ISamolot
{
    public string Model { get; set; }
    public string WeaponType { get; set; }
    public bool IsAvailable { get; set; }

    public MilitaryPlane(string model, string weaponType)
    {
        Model = model;
        WeaponType = weaponType;
    }

    public void PerformAction()
    {
        Console.WriteLine($"{Model} is performing a system check.");
    }

    public void ConductMaintenance()
    {
        Console.WriteLine($"Conducting maintenance on the {Model} for upcoming operations.");
    }

    public void AdjustSeating()
    {
        Console.WriteLine($"Adjusting seating arrangements in {Model}.");
    }

    public bool IsOnMission { get; set; }
}
