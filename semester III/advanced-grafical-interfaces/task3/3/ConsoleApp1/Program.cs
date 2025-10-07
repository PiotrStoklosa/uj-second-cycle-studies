using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        object obj = new PassengerPlane("Boeing 737", 200);

        ISamolot plane = obj as ISamolot;

        if (plane != null)
        {
            plane.PerformAction();
        }
        else
        {
            Console.WriteLine("Object is not of type Plane.");
        }
    }
}
