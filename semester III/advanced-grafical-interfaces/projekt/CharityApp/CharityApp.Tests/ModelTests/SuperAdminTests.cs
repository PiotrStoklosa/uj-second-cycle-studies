using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharityApp;

namespace CharityApp.Tests
{
    [TestClass]
    public class SuperAdminTests
    {
        [TestMethod]
        public void AddAdmin_ShouldAddAdminToAdminsList()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            var admin = new Admin("admin1", "password", superAdmin, "Jan", "Kowalski", "12345678901", "Wielkopolska");
            superAdmin.AddAdmin(admin);
            Assert.IsTrue(superAdmin.Admins.Contains(admin));
        }

        [TestMethod]
        public void AddGoal_ShouldAddGoalToGoalsList()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            var goal = new Goal("Cel", "Opis", 1000, "123123123");
            superAdmin.AddGoal(goal);
            Assert.IsTrue(superAdmin.Goals.Contains(goal));
        }

        [TestMethod]
        public void AddGoal_ShouldNotAddNullGoal()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            superAdmin.AddGoal(null);
            Assert.AreEqual(0, superAdmin.Goals.Count);
        }

        [TestMethod]
        public void AddToTotalAmount_ShouldIncreaseTotalAmount()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            superAdmin.AddToTotalAmount(500);
            superAdmin.AddToTotalAmount(300);
            Assert.AreEqual(800, superAdmin.TotalAmount);
        }

        [TestMethod]
        public void GetGoalById_ShouldReturnGoalIfExists()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            var goal = new Goal("Cel", "Opis", 1000, "123123123");
            superAdmin.AddGoal(goal);
            var retrievedGoal = superAdmin.GetGoalById(goal.Id);
            Assert.AreEqual(goal, retrievedGoal);
        }

        [TestMethod]
        public void GetGoalById_ShouldReturnNullIfGoalDoesNotExist()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            var goalId = Guid.NewGuid();
            var retrievedGoal = superAdmin.GetGoalById(goalId);
            Assert.IsNull(retrievedGoal);
        }

        [TestMethod]
        public void DeleteGoal_ShouldRemoveGoalFromList()
        {
            var superAdmin = new SuperAdmin("admin", "password");
            var goal = new Goal("Cel", "Opis", 1000, "123123123");
            superAdmin.AddGoal(goal);
            superAdmin.DeleteGoal(goal);
            Assert.IsFalse(superAdmin.Goals.Contains(goal));
        }
    }
}