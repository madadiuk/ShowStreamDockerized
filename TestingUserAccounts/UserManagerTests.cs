using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace TestingUserAccounts
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager userManager;

        [TestInitialize]
        public void SetUp()
        {
            userManager = new UserManager();
        }

        [TestMethod]
        public void TestAddUser()
        {
            // Arrange
            User user = new User
            {
                Username = "testuser" + Guid.NewGuid(), // Ensure unique username
                Email = "testuser@example.com",
                Password = "Password123!",
                Role = "User"
            };

            // Act
            userManager.AddUser(user);

            // Assert
            var users = userManager.GetAllUsers();
            Assert.IsTrue(users.Any(u => u.Username == user.Username && u.Email == user.Email),
                "User should be added in the database.");
        }
    }
}
