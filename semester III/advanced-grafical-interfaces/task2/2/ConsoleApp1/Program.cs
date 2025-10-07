using System;

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
