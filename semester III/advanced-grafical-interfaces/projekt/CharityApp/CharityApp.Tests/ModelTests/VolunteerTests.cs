using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CharityApp;

namespace CharityApp.Tests
{
    [TestClass]
    public class VolunteerTests
    {
        [TestMethod]
        public void Volunteer_Constructor_ShouldInitializeObjectProperly()
        {
            string name = "Jan Kowalski";
            string location = "Krakow";
            DateTime dateAndTime = DateTime.Now;
            UserCredentials credentials = new UserCredentials("jan_kowalski", "password123");
            Volunteer volunteer = new Volunteer(name, location, dateAndTime, credentials);

            Assert.AreEqual(name, volunteer.Name);
            Assert.AreEqual(location, volunteer.Location);
            Assert.AreEqual(dateAndTime, volunteer.DateAndTime);
            Assert.AreEqual(credentials, volunteer.Credentials);
            Assert.AreEqual(BoxStatus.NotPicked, volunteer.BoxStatus);
            Assert.AreEqual(0.0, volunteer.CollectedAmount);
            Assert.IsFalse(volunteer.IsSettled);
        }

        [TestMethod]
        public void Volunteer_DefaultBoxStatus_ShouldBeNotPicked()
        {
            Volunteer volunteer = new Volunteer("Ania", "Wriclaw", DateTime.Now, new UserCredentials("ania", "password"));
            Assert.AreEqual(BoxStatus.NotPicked, volunteer.BoxStatus);
        }

        [TestMethod]
        public void Volunteer_DefaultCollectedAmount_ShouldBeZero()
        {
            Volunteer volunteer = new Volunteer("Karol", "Poznan", DateTime.Now, new UserCredentials("karol", "password123"));
            Assert.AreEqual(0.0, volunteer.CollectedAmount);
        }

        [TestMethod]
        public void Volunteer_DefaultIsSettled_ShouldBeFalse()
        {
            Volunteer volunteer = new Volunteer("Ala", "Warszawa", DateTime.Now, new UserCredentials("ala", "password"));
            Assert.IsFalse(volunteer.IsSettled);
        }
    }
}