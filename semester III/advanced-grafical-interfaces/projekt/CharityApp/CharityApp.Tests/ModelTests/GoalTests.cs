using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CharityApp.Tests
{
    [TestClass]
    public class GoalTests
    {
        [TestMethod]
        public void Goal_Constructor_ShouldInitializeProperties()
        {
            string name = "Na ochrone zdrowia";
            string description = "Zakup sprzety dla szpitali";
            double amount = 5000.0;
            string accountNumber = "123456789";
            Goal goal = new Goal(name, description, amount, accountNumber);

            Assert.AreEqual(name, goal.Name);
            Assert.AreEqual(description, goal.Description);
            Assert.AreEqual(amount, goal.Amount);
            Assert.AreEqual(accountNumber, goal.AccountNumber);
            Assert.IsFalse(goal.IsConfirmed);
        }

        [TestMethod]
        public void Goal_Constructor_ShouldInitializeWithIsConfirmed()
        {
            string name = "Wspieranie adukacji";
            string description = "Zakup licencji Visual Studio dla szkol";
            double amount = 10000.0;
            string accountNumber = "987654321";
            bool isConfirmed = true;

            Goal goal = new Goal(name, description, amount, accountNumber, isConfirmed);

            Assert.AreEqual(name, goal.Name);
            Assert.AreEqual(description, goal.Description);
            Assert.AreEqual(amount, goal.Amount);
            Assert.AreEqual(accountNumber, goal.AccountNumber);
            Assert.IsTrue(goal.IsConfirmed);
        }

        [TestMethod]
        public void Goal_Id_ShouldBeUnique()
        {
            Goal goal1 = new Goal("Cel1", "Opis1", 2000.0, "111222333");
            Goal goal2 = new Goal("Cel2", "Opis2", 3000.0, "444555666");
            Assert.AreNotEqual(goal1.Id, goal2.Id);
        }
    }
}