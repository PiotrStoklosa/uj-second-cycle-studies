using System;


class PassengerPlane : ISamolot
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
