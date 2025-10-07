using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharityApp;

namespace CharityApp.Tests
{
    [TestClass]
    public class UserCredentialsTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var username = "user";
            var password = "password";
            var userCredentials = new UserCredentials(username, password);
            Assert.AreEqual(username, userCredentials.Username);
            Assert.AreEqual(password, userCredentials.Password);
        }

        [TestMethod]
        public void Properties_ShouldBeSetAndGetCorrectly()
        {
            var userCredentials = new UserCredentials("user", "password");
            userCredentials.Username = "user2";
            userCredentials.Password = "password2";
            Assert.AreEqual("user2", userCredentials.Username);
            Assert.AreEqual("password2", userCredentials.Password);
        }
    }
}