using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;

namespace TestingUserAccounts
{
    [TestClass]
    public class UserManagementTests
    {
        private UserManager userManager;

        [TestInitialize]
        public void SetUp()
        {
            userManager = new UserManager();

            // Clear the database or set up a known good state
            ClearDatabase();
        }

        private void ClearDatabase()
        {
            try
            {
                clsDataConnection db = new clsDataConnection();
                db.Execute("spDeleteAllUsers");
            }
            catch (Exception ex)
            {
                throw new Exception("Error clearing the database: " + ex.Message);
            }
        }

        [TestMethod]
        public void TestExtremeMinUsername()
        {
            User user = new User
            {
                Username = "",
                Email = "extreme_min@example.com",
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with extreme min username should throw exception.");
        }

        [TestMethod]
        public void TestMinMinusOneUsername()
        {
            User user = new User
            {
                Username = "a", // Assuming the minimum length is 2
                Email = "min_minus_one@example.com",
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with username length of min - 1 should throw exception.");
        }

        [TestMethod]
        public void TestMinBoundaryUsername()
        {
            User user = new User
            {
                Username = "ab", // Assuming the minimum length is 2
                Email = "min_boundary@example.com",
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with min boundary username should be added successfully.");
        }

        [TestMethod]
        public void TestMinPlusOneUsername()
        {
            User user = new User
            {
                Username = "abc", // Assuming the minimum length is 2
                Email = "min_plus_one@example.com",
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with username length of min + 1 should be added successfully.");
        }

        [TestMethod]
        public void TestMaxMinusOneUsername()
        {
            User user = new User
            {
                Username = new string('a', 254), // Assuming the maximum length is 255
                Email = "max_minus_one@example.com",
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with username length of max - 1 should be added successfully.");
        }

        [TestMethod]
        public void TestMaxBoundaryUsername()
        {
            User user = new User
            {
                Username = new string('a', 255), // Assuming the maximum length is 255
                Email = "max_boundary@example.com",
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with max boundary username should be added successfully.");
        }

        [TestMethod]
        public void TestMaxPlusOneUsername()
        {
            User user = new User
            {
                Username = new string('a', 256), // Assuming the maximum length is 255
                Email = "max_plus_one@example.com",
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with username length of max + 1 should throw exception.");
        }

        [TestMethod]
        public void TestMidUsername()
        {
            User user = new User
            {
                Username = new string('a', 127), // Midpoint for username length between 1 and 255
                Email = "mid@example.com",
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with mid-length username should be added successfully.");
        }

        [TestMethod]
        public void TestExtremeMaxUsername()
        {
            User user = new User
            {
                Username = new string('a', 1000), // Extreme case, assuming this is too long
                Email = "extreme_max@example.com",
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with extremely long username should throw exception.");
        }

        [TestMethod]
        public void TestInvalidDataTypeUsername()
        {
            User user = new User
            {
                Username = "validUsername",
                Email = "invalidEmailFormat", // Invalid email format
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with invalid data type should throw exception.");
        }

        // Additional tests can be added here following the same structure

        [TestMethod]
        public void TestExtremeMinEmail()
        {
            User user = new User
            {
                Username = "validUsername",
                Email = "",
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with extreme min email should throw exception.");
        }

        [TestMethod]
        public void TestMinMinusOneEmail()
        {
            User user = new User
            {
                Username = "validUsername",
                Email = "a", // Assuming the minimum valid email format is longer
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with email length of min - 1 should throw exception.");
        }

        [TestMethod]
        public void TestMinBoundaryEmail()
        {
            User user = new User
            {
                Username = "validUsername",
                Email = "a@b.c", // Assuming the minimum valid email format
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with min boundary email should be added successfully.");
        }

        [TestMethod]
        public void TestMinPlusOneEmail()
        {
            User user = new User
            {
                Username = "validUsername",
                Email = "a@bc.de", // Slightly longer valid email
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with email length of min + 1 should be added successfully.");
        }

        [TestMethod]
        public void TestMaxMinusOneEmail()
        {
            string email = new string('a', 243) + "@example.com"; // 255 - "@example.com".Length - 1
            User user = new User
            {
                Username = "validUsername",
                Email = email,
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with email length of max - 1 should be added successfully.");
        }

        [TestMethod]
        public void TestMaxBoundaryEmail()
        {
            string email = new string('a', 243) + "@example.com"; // 255 - "@example.com".Length
            User user = new User
            {
                Username = "validUsername",
                Email = email,
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with max boundary email should be added successfully.");
        }

        [TestMethod]
        public void TestMaxPlusOneEmail()
        {
            string email = new string('a', 244) + "@example.com"; // 255 + 1 - "@example.com".Length
            User user = new User
            {
                Username = "validUsername",
                Email = email,
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with email length of max + 1 should throw exception.");
        }

        [TestMethod]
        public void TestMidEmail()
        {
            string email = new string('a', 122) + "@example.com"; // Midpoint for email length
            User user = new User
            {
                Username = "validUsername",
                Email = email,
                Password = "Password123",
                Role = "User"
            };

            userManager.AddUser(user);
            User addedUser = userManager.GetUserByUsername(user.Username);
            Assert.IsNotNull(addedUser, "User with mid-length email should be added successfully.");
        }

        [TestMethod]
        public void TestExtremeMaxEmail()
        {
            string email = new string('a', 1000) + "@example.com"; // Extreme case, assuming this is too long
            User user = new User
            {
                Username = "validUsername",
                Email = email,
                Password = "Password123",
                Role = "User"
            };

            Assert.ThrowsException<Exception>(() => userManager.AddUser(user), "Adding user with extremely long email should throw exception.");
        }
    }
}
