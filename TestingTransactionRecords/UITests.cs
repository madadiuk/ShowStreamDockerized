using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestingTransactionRecords
{
    [TestClass]
    public class UITests
    {
        private IWebDriver driver;
        private const string baseUrl = "http://localhost:50298"; // Base URL for your application

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestTeamMainMenu()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TeamMainMenu.aspx");

            // Verify the page title
            Assert.AreEqual("Team Main Menu", driver.Title);

            // Verify the buttons exist
            Assert.IsNotNull(driver.FindElement(By.Id("btnGoToTransactions")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnGoToEpisodesManagement")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnGoToGenresManagement")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnGoToUserManagement")));
        }

        [TestMethod]
        public void TestTransactionRecordsList()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TransactionRecordsList.aspx");

            // Verify the page title
            Assert.AreEqual("Transaction List", driver.Title);

            // Verify the buttons exist
            Assert.IsNotNull(driver.FindElement(By.Id("btnAddNewTransaction")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnFilterTransactions")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnViewStatistics")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnReturnToMainMenu")));
        }

        [TestMethod]
        public void TestTransactionRecordsDataEntry()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TransactionRecordsDataEntry.aspx");

            // Verify the page title
            Assert.AreEqual("Transaction Entry", driver.Title);

            // Verify the form elements exist
            Assert.IsNotNull(driver.FindElement(By.Id("txtAmount")));
            Assert.IsNotNull(driver.FindElement(By.Id("txtTransactionDate")));
            Assert.IsNotNull(driver.FindElement(By.Id("ddlPaymentMethod")));
            Assert.IsNotNull(driver.FindElement(By.Id("ddlStatus")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnSave")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnViewList")));
        }

        [TestMethod]
        public void TestTransactionRecordsEdit()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TransactionRecordsEdit.aspx?TransactionID=1");

            // Verify the page title
            Assert.AreEqual("Edit Transaction", driver.Title);

            // Verify the form elements exist
            Assert.IsNotNull(driver.FindElement(By.Id("txtAmount")));
            Assert.IsNotNull(driver.FindElement(By.Id("txtTransactionDate")));
            Assert.IsNotNull(driver.FindElement(By.Id("ddlPaymentMethod")));
            Assert.IsNotNull(driver.FindElement(By.Id("ddlStatus")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnUpdate")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnReturn")));
        }

        [TestMethod]
        public void TestTransactionRecordsFilter()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TransactionRecordsFilter.aspx");

            // Verify the page title
            Assert.AreEqual("Transaction Filter", driver.Title);

            // Verify the form elements exist
            Assert.IsNotNull(driver.FindElement(By.Id("ddlPaymentMethodFilter")));
            Assert.IsNotNull(driver.FindElement(By.Id("ddlStatusFilter")));
            Assert.IsNotNull(driver.FindElement(By.Id("txtDateFrom")));
            Assert.IsNotNull(driver.FindElement(By.Id("txtDateTo")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnFilter")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnReturnToList")));
        }

        [TestMethod]
        public void TestTransactionStatistics()
        {
            driver.Navigate().GoToUrl(baseUrl + "/TransactionStatistics.aspx");

            // Verify the page title
            Assert.AreEqual("Transaction Statistics", driver.Title);

            // Verify the labels exist
            Assert.IsNotNull(driver.FindElement(By.Id("lblTotalTransactions")));
            Assert.IsNotNull(driver.FindElement(By.Id("lblTotalAmount")));
            Assert.IsNotNull(driver.FindElement(By.Id("lblAverageAmount")));
            Assert.IsNotNull(driver.FindElement(By.Id("lblUniqueUsers")));
            Assert.IsNotNull(driver.FindElement(By.Id("btnReturnToList")));
        }
    }
}
