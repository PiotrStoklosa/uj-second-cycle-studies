using System;

class PassengerPlane
{
    public string Model { get; set; }
    public int SeatCount { get; set; }

    public PassengerPlane(string model, int seatCount)
    {
        Model = model;
        SeatCount = seatCount;
    }

    public void PerformAction()
    {
        Console.WriteLine($"{Model} is preparing for its next flight.");
    }

    public void AdjustSeating()
    {
        Console.WriteLine($"Adjusting seating arrangements in {Model}.");
    }

    public bool IsAvailable { get; set; }
}

class TransportPlane
{
    public string Model { get; set; }
    public int PayloadCapacity { get; set; }

    public TransportPlane(string model, int payloadCapacity)
    {
        Model = model;
        PayloadCapacity = payloadCapacity;
    }

    public void PerformAction()
    {
        Console.WriteLine($"{Model} is getting ready for loadout.");
    }

    public void CheckCapacity()
    {
        Console.WriteLine($"Checking payload capacity of {Model}: {PayloadCapacity} kg.");
    }

    public bool IsReadyForFlight { get; set; }
}

class MilitaryPlane
{
    public string Model { get; set; }
    public string WeaponType { get; set; }

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

    public bool IsOnMission { get; set; }
}

class Program
{
    static void Main()
    {
        PassengerPlane passengerPlane = new PassengerPlane("Boeing 737", 200);
        TransportPlane transportPlane = new TransportPlane("C-130 Hercules", 20000);
        MilitaryPlane militaryPlane = new MilitaryPlane("F-16", "missiles");

        passengerPlane.IsAvailable = true;
        transportPlane.IsReadyForFlight = false;
        militaryPlane.IsOnMission = true;

        passengerPlane.PerformAction();
        passengerPlane.AdjustSeating();

        transportPlane.PerformAction();
        transportPlane.CheckCapacity();

        militaryPlane.PerformAction();
        militaryPlane.ConductMaintenance();
    }
}
