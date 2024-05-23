using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using AdminSystem;
using System.Collections.Generic;

namespace AdminSystem.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager userManager;

        [TestInitialize]
        public void Setup()
        {
            userManager = new UserManager();
        }

        [TestMethod]
        public void TestAddUser()
        {
            User newUser = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Password123!",
                Role = "user"
            };

            userManager.AddUser(newUser);

            List<User> users = userManager.GetAllUsers();
            User addedUser = users.Find(u => u.Username == "testuser");

            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Username, addedUser.Username);
            Assert.AreEqual(newUser.Email, addedUser.Email);
            // Note: Password will be hashed, so this won't match the plain text password
            Assert.AreEqual(newUser.Role, addedUser.Role);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            User newUser = new User
            {
                Username = "deleteuser",
                Email = "deleteuser@example.com",
                Password = "Password123!",
                Role = "user"
            };

            userManager.AddUser(newUser);

            List<User> users = userManager.GetAllUsers();
            User addedUser = users.Find(u => u.Username == "deleteuser");

            if (addedUser != null)
            {
                userManager.DeleteUser(addedUser.UserID);

                users = userManager.GetAllUsers();
                User deletedUser = users.Find(u => u.Username == "deleteuser");

                Assert.IsNull(deletedUser);
            }
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            User newUser = new User
            {
                Username = "updateuser",
                Email = "updateuser@example.com",
                Password = "Password123!",
                Role = "user"
            };

            userManager.AddUser(newUser);

            List<User> users = userManager.GetAllUsers();
            User addedUser = users.Find(u => u.Username == "updateuser");

            if (addedUser != null)
            {
                addedUser.Email = "updateduser@example.com";
                addedUser.Password = "NewPassword123!";
                userManager.UpdateUser(addedUser);

                users = userManager.GetAllUsers();
                User updatedUser = users.Find(u => u.UserID == addedUser.UserID);

                Assert.IsNotNull(updatedUser);
                Assert.AreEqual("updateduser@example.com", updatedUser.Email);
                // Note: Password will be hashed, so this won't match the plain text password
            }
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            userManager.AddUser(new User { Username = "user1", Email = "user1@example.com", Password = "Password123!", Role = "user" });
            userManager.AddUser(new User { Username = "user2", Email = "user2@example.com", Password = "Password123!", Role = "user" });

            List<User> users = userManager.GetAllUsers();

            Assert.IsTrue(users.Count >= 2);
        }
    }
}
