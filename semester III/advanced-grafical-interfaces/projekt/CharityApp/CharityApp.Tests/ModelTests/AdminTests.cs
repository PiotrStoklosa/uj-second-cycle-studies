using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharityApp;
using System;

namespace CharityApp.Tests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Admin_Constructor_ShouldInitializeObjectCorrectly()
        {
            var superAdmin = new SuperAdmin("superadmin", "password");
            string username = "admin";
            string password = "admin123";
            string firstName = "Anna";
            string lastName = "Nowak";
            string pesel = "12345678901";
            string region = "Malopolska";

            var admin = new Admin(username, password, superAdmin, firstName, lastName, pesel, region);

            Assert.AreEqual(username, admin.Credentials.Username);
            Assert.AreEqual(password, admin.Credentials.Password);
            Assert.AreEqual(firstName, admin.FirstName);
            Assert.AreEqual(lastName, admin.LastName);
            Assert.AreEqual(pesel, admin.Pesel);
            Assert.AreEqual(region, admin.Region);
            Assert.IsNotNull(admin.Volunteers);
            Assert.AreEqual(0, admin.Volunteers.Count); 
        }

        [TestMethod]
        public void AddVolunteer_ShouldAddVolunteerToList()
        {
            var superAdmin = new SuperAdmin("superadmin", "password");
            var admin = new Admin("admin", "admin123", superAdmin, "Anna", "Nowak", "12345678901", "Malopolska");
            var volunteer1 = new Volunteer("Krzysztof", "Gdansk", DateTime.Now, new UserCredentials("k1", "password123"));
            var volunteer2 = new Volunteer("Maria", "Rzeszow", DateTime.Now, new UserCredentials("m2", "password456"));
            admin.AddVolunteer(volunteer1);
            admin.AddVolunteer(volunteer2);
            Assert.AreEqual(2, admin.Volunteers.Count);

            Assert.AreEqual(volunteer1, admin.Volunteers[0]);
            Assert.AreEqual(volunteer2, admin.Volunteers[1]);
        }

        [TestMethod]
        public void AddVolunteer_ShouldNotAddNullVolunteers()
        {

            var superAdmin = new SuperAdmin("superadmin", "password");
            var admin = new Admin("admin", "admin123", superAdmin, "Anna", "Nowak", "12345678901", "Malopolska");

            admin.AddVolunteer(null);
            Assert.AreEqual(0, admin.Volunteers.Count); 
        }
    }
}