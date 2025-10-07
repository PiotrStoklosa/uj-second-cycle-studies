using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;
using CharityApp;
using System;

namespace CharityApp.Tests
{
    [TestClass]
    public class BoolToVisibilityConverterTests
    {
        private BoolToVisibilityConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new BoolToVisibilityConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnVisibilityVisible_WhenTrue()
        {
            bool input = true;
            var result = _converter.Convert(input, typeof(Visibility), null, null);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnVisibilityCollapsed_WhenFalse()
        {
            bool input = false;
            var result = _converter.Convert(input, typeof(Visibility), null, null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnVisibilityCollapsed_WhenNull()
        {
            object input = null;
            var result = _converter.Convert(input, typeof(Visibility), null, null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            object input = Visibility.Visible;
            _converter.ConvertBack(input, typeof(bool), null, null);
        }
    }
}