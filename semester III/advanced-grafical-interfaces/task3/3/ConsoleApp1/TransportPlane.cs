using System;
class TransportPlane : ISamolot
{
    public string Model { get; set; }
    public int PayloadCapacity { get; set; }

    public bool IsAvailable { get; set; }


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

    public void AdjustSeating()
    {
        Console.WriteLine($"Adjusting seating arrangements in {Model}.");
    }

    public bool IsReadyForFlight { get; set; }
}
