using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace TestingTransactionRecords
{
    [TestClass]
    public class TransactionManagerTests
    {
        private TransactionManager transactionManager;
        private clsDataConnection connection;

        [TestInitialize]
        public void SetUp()
        {
            transactionManager = new TransactionManager();
            // Initialize the connection here for reusability
            connection = new clsDataConnection();
            // Optionally, clear and set up the database here
        }

        [TestCleanup]
        public void TearDown()
        {
            // Clean up the database after each test if needed
        }

        [TestMethod]
        public void TestAddTransaction()
        {
            // Arrange
            int userId = 1; // Example user ID
            decimal amount = 100.00m;
            DateTime date = DateTime.Today;
            string paymentMethod = "Credit Card";
            string status = "Pending";

            // Act
            transactionManager.AddTransaction(userId, amount, date, paymentMethod, status);

            // Assert
            var results = transactionManager.GetTransactionsByUser(userId); // Assuming this method is implemented
            Assert.IsTrue(results.AsEnumerable().Any(row =>
                (int)row["UserID"] == userId &&
                (decimal)row["Amount"] == amount &&
                ((DateTime)row["TransactionDate"]).Date == date.Date &&  // Corrected casting here
                (string)row["PaymentMethod"] == paymentMethod &&
                (string)row["Status"] == status),
                "Transaction should be added in the database.");
        }

        // Additional methods to specifically retrieve transactions by user ID, etc., can be implemented in TransactionManager for more targeted queries.
    }
}
