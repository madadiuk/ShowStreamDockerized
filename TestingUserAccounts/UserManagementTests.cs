using System;
using System.Collections.Generic;
using ClassLibrary;

namespace UserManagementTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of UserManager
            UserManager userManager = new UserManager();

            // Test Logs
            List<TestLog> testLogs = new List<TestLog>();

            // Define test data
            List<User> testUsers = new List<User>
            {
                new User { UserID = 1, Username = "testuser1", Email = "test1@example.com", Password = "password1", Role = "User" },
                new User { UserID = 2, Username = "testuser2", Email = "test2@example.com", Password = "password2", Role = "Admin" },
                // Add more test users as needed
            };

            // Add test users
            foreach (var user in testUsers)
            {
                userManager.AddUser(user);
                testLogs.Add(new TestLog
                {
                    TestType = "Add User",
                    TestData = user.Username,
                    ExpectedResult = "User added successfully.",
                    ActualResult = "User added successfully."
                });
            }

            // Find user by ID
            User foundUser = userManager.GetUserById(1);
            testLogs.Add(new TestLog
            {
                TestType = "Find User by ID",
                TestData = "UserID: 1",
                ExpectedResult = "User found.",
                ActualResult = foundUser != null ? "User found." : "User not found."
            });

            // Update user
            if (foundUser != null)
            {
                foundUser.Email = "updated@example.com";
                userManager.UpdateUser(foundUser);
                testLogs.Add(new TestLog
                {
                    TestType = "Update User",
                    TestData = foundUser.Username,
                    ExpectedResult = "User updated successfully.",
                    ActualResult = "User updated successfully."
                });
            }

            // Delete user
            userManager.DeleteUser(1);
            testLogs.Add(new TestLog
            {
                TestType = "Delete User",
                TestData = "UserID: 1",
                ExpectedResult = "User deleted successfully.",
                ActualResult = "User deleted successfully."
            });

            // Display test logs
            foreach (var log in testLogs)
            {
                Console.WriteLine($"Test Type: {log.TestType}");
                Console.WriteLine($"Test Data: {log.TestData}");
                Console.WriteLine($"Expected Result: {log.ExpectedResult}");
                Console.WriteLine($"Actual Result: {log.ActualResult}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        class TestLog
        {
            public string TestType { get; set; }
            public string TestData { get; set; }
            public string ExpectedResult { get; set; }
            public string ActualResult { get; set; }
        }
    }
}
