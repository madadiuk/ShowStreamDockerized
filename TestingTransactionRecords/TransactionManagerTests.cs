using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;
using ClassLibrary;

namespace TestingTransactionRecords
{
    [TestClass]
    public class TransactionManagerTests
    {
        private TransactionManager transactionManager;

        [TestInitialize]
        public void SetUp()
        {
            transactionManager = new TransactionManager();
        }

        [TestMethod]
        public void TestGetAllTransactions()
        {
            // Act
            var result = transactionManager.GetAllTransactions();

            // Assert
            Assert.IsTrue(result.Rows.Count > 0, "Should return all transactions.");
        }

        [TestMethod]
        public void TestGetFilteredTransactions()
        {
            // Arrange
            string paymentMethod = "Credit Card";
            string status = "Pending";
            DateTime? dateFrom = DateTime.Today.AddDays(-30);
            DateTime? dateTo = DateTime.Today;

            // Act
            var result = transactionManager.GetFilteredTransactions(paymentMethod, status, dateFrom, dateTo);

            // Assert
            Assert.IsTrue(result.Rows.Count > 0, "Should return filtered transactions.");
        }
    }
}
