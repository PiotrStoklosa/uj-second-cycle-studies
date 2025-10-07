using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        List<ISamolot> planes = new List<ISamolot>
        {
            new PassengerPlane("Boeing 737", 200),
            new TransportPlane("C-130 Hercules", 20000),
            new MilitaryPlane("F-16", "missiles")
        };

        foreach (var plane in planes)
        {
            Console.WriteLine($"The type of this object is: {plane.GetType().Name}");
        }
    }
}

