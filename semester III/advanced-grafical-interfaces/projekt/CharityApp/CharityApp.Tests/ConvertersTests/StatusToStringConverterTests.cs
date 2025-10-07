using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharityApp;

namespace CharityApp.Tests
{
    [TestClass]
    public class StatusToStringConverterTests
    {
        private StatusToStringConverter _converter;

        [TestInitialize]
        public void TestInitialize()
        {
            _converter = new StatusToStringConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnCorrectString_WhenStatusIsNotPicked()
        {
            var status = BoxStatus.NotPicked;
            var result = _converter.Convert(status, typeof(string), null, null);
            Assert.AreEqual("niepobrana", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCorrectString_WhenStatusIsPicked()
        { 
            var status = BoxStatus.Picked;
            var result = _converter.Convert(status, typeof(string), null, null);
            Assert.AreEqual("pobrana", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCorrectString_WhenStatusIsReturned()
        {
            var status = BoxStatus.Returned;
            var result = _converter.Convert(status, typeof(string), null, null);
            Assert.AreEqual("zwrócona", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCorrectString_WhenStatusIsSettled()
        {
            var status = BoxStatus.Settled;
            var result = _converter.Convert(status, typeof(string), null, null);
            Assert.AreEqual("rozliczona", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnUnknownStatus_WhenStatusIsUnknown()
        {
            var status = (BoxStatus)10000;
            var result = _converter.Convert(status, typeof(string), null, null);
            Assert.AreEqual("Unknown Status", result);
        }

        [TestMethod]
        public void Convert_ShouldReturnUnknown2_WhenValueIsNotBoxStatus()
        {
            var nonStatusValue = "abc";
            var result = _converter.Convert(nonStatusValue, typeof(string), null, null);
            Assert.AreEqual("Unknown2", result);
        }
    }
}