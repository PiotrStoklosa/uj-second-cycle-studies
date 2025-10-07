using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharityApp.Tests
{
    [TestClass]
    public class DoubleToStringConverterTests
    {
        private DoubleToStringConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new DoubleToStringConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnFormattedString_WhenValueIsDouble()
        {
            var result = _converter.Convert(123.40, typeof(string), null, null);
            Assert.AreEqual("123,40", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnDefault_WhenValueIsNotDouble()
        {
            var result = _converter.Convert("Not a number", typeof(string), null, null);
            Assert.AreEqual("0.00", result);
        }

        [TestMethod]
        public void ConvertBack_ShouldReturnDouble_WhenValidString()
        {
            var result = _converter.ConvertBack("123,45", typeof(double), null, null);
            Assert.AreEqual(123.45, result);
        }

        [TestMethod]
        public void ConvertBack_ShouldReturnZero_WhenInvalidString()
        {
            var result = _converter.ConvertBack("Invalid", typeof(double), null, null);
            Assert.AreEqual(0.0, result);
        }
    }
}