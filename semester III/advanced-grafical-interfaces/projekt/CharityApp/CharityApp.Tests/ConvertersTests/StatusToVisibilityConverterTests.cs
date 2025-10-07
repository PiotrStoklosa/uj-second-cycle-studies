using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;

namespace CharityApp.Tests
{
    [TestClass]
    public class StatusToVisibilityConverterTests
    {
        private StatusToVisibilityConverter _converter;

        [TestInitialize]
        public void TestInitialize()
        {
            _converter = new StatusToVisibilityConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnVisible_WhenStatusIsSettled()
        {
            var status = BoxStatus.Settled;
            var result = _converter.Convert(status, typeof(Visibility), null, string.Empty);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenStatusIsNotSettled()
        {
            var status = BoxStatus.NotPicked;
            var result = _converter.Convert(status, typeof(Visibility), null, string.Empty);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenStatusIsPicked()
        {
            var status = BoxStatus.Picked;
            var result = _converter.Convert(status, typeof(Visibility), null, string.Empty);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenStatusIsReturned()
        {
            var status = BoxStatus.Returned;
            var result = _converter.Convert(status, typeof(Visibility), null, string.Empty);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenStatusIsUnknown()
        {
            var status = (BoxStatus)1000;
            var result = _converter.Convert(status, typeof(Visibility), null, string.Empty);
            Assert.AreEqual(Visibility.Collapsed, result);
        }
    }
}