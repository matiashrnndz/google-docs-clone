using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestUser()
        {
            User user = new User();

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestUserShouldNotBeAdminOnCreation()
        {
            User user = new User();

            Assert.AreNotEqual(user.Administrator, true);
        }

        [TestMethod]
        public void TestUserShouldHaveBlankNameOnCreation()
        {
            User user = new User();

            Assert.AreEqual(user.Name, "");
        }

        [TestMethod]
        public void TestUserShouldHaveBlankLastNameOnCreation()
        {
            User user = new User();

            Assert.AreEqual(user.LastName, "");
        }

        [TestMethod]
        public void TestUserShouldHaveBlankUserNameOnCreation()
        {
            User user = new User();

            Assert.AreEqual(user.UserName, "");
        }

        [TestMethod]
        public void TestUserShouldHaveBlankPasswordOnCreation()
        {
            User user = new User();

            Assert.AreEqual(user.Password, "");
        }

        [TestMethod]
        public void TestUserShouldHaveBlankEmailOnCreation()
        {
            User user = new User();

            Assert.AreEqual(user.Email, "");
        }
    }
}
