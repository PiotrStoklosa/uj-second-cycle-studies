using System;
using CharityApp;

public class Volunteer
{
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime DateAndTime { get; set; }
    public UserCredentials Credentials { get; set; }
    public BoxStatus BoxStatus { get; set; } = BoxStatus.NotPicked;
    public double CollectedAmount { get; set; } = 0.0;
    public bool IsSettled { get; set; }


    public Volunteer(string name, string location, DateTime dateAndTime, UserCredentials credentials)
    {
        Name = name;
        Location = location;
        DateAndTime = dateAndTime;
        Credentials = credentials;
    }
}
