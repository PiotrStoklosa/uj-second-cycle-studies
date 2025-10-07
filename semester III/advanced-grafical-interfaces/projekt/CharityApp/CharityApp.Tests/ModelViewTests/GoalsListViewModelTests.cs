using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace CharityApp.Tests
{
    [TestClass]
    public class GoalsListViewModelTests
    {
        [TestMethod]
        public void TestTotalAmountPropertyChanged()
        {
            var viewModel = new GoalsListViewModel();
            bool propertyChangedCalled = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.TotalAmount))
                {
                    propertyChangedCalled = true;
                }
            };
            viewModel.TotalAmount = 100;
            Assert.IsTrue(propertyChangedCalled, "PropertyChanged for TotalAmount should be triggered.");
            Assert.AreEqual(100, viewModel.TotalAmount, "TotalAmount should be set to 100.");
        }

        [TestMethod]
        public void TestTotalAmountOfGoalsPropertyChanged()
        {
            var viewModel = new GoalsListViewModel();
            bool propertyChangedCalled = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.TotalAmountOfGoals))
                {
                    propertyChangedCalled = true;
                }
            };
            viewModel.TotalAmountOfGoals = 200;
            Assert.IsTrue(propertyChangedCalled, "PropertyChanged for TotalAmountOfGoals should be triggered.");
            Assert.AreEqual(200, viewModel.TotalAmountOfGoals, "TotalAmountOfGoals should be set to 200.");
        }

        [TestMethod]
        public void TestTotalAmountOfConfirmedGoalsPropertyChanged()
        {
            var viewModel = new GoalsListViewModel();
            bool propertyChangedCalled = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.TotalAmountOfConfirmedGoals))
                {
                    propertyChangedCalled = true;
                }
            };
            viewModel.TotalAmountOfConfirmedGoals = 300;
            Assert.IsTrue(propertyChangedCalled, "PropertyChanged for TotalAmountOfConfirmedGoals should be triggered.");
            Assert.AreEqual(300, viewModel.TotalAmountOfConfirmedGoals, "TotalAmountOfConfirmedGoals should be set to 300.");
        }

        [TestMethod]
        public void TestRemainingAmountPropertyChanged()
        {
            var viewModel = new GoalsListViewModel();
            bool propertyChangedCalled = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.RemainingAmount))
                {
                    propertyChangedCalled = true;
                }
            };
            viewModel.RemainingAmount = 50;
            Assert.IsTrue(propertyChangedCalled, "PropertyChanged for RemainingAmount should be triggered.");
            Assert.AreEqual(50, viewModel.RemainingAmount, "RemainingAmount should be set to 50.");
        }

        [TestMethod]
        public void TestAmountOfGoalsColorPropertyChanged()
        {
            var viewModel = new GoalsListViewModel();
            bool propertyChangedCalled = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.AmountOfGoalsColor))
                {
                    propertyChangedCalled = true;
                }
            };
            viewModel.AmountOfGoalsColor = "Red";
            Assert.IsTrue(propertyChangedCalled, "PropertyChanged for AmountOfGoalsColor should be triggered.");
            Assert.AreEqual("Red", viewModel.AmountOfGoalsColor, "AmountOfGoalsColor should be set to 'Red'.");
        }
    }
}