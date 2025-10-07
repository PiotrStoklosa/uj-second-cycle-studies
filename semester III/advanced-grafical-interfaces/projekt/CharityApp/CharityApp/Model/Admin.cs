using System.Collections.Generic;

namespace CharityApp
{
    public class Admin
    {
        public UserCredentials Credentials { get; private set; }
        public List<Volunteer> Volunteers { get; private set; }
        public SuperAdmin SuperAdmin { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Pesel { get; private set; }
        public string Region { get; private set; }

        public Admin(string username, string password, SuperAdmin superAdmin, string firstName, string lastName, string pesel, string region)
        {
            Credentials = new UserCredentials(username, password);
            Volunteers = new List<Volunteer>();
            SuperAdmin = superAdmin;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Region = region;
        }

        public void AddVolunteer(Volunteer volunteer)
        {
            if(volunteer != null)
                Volunteers.Add(volunteer);
        }
    }
}

