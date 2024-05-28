using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibrary;

namespace TestingTransactionRecords
{
    [TestClass]
    public class UserManagementTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Placeholder for user management tests
        }

        [TestMethod]
        public void InstanceOK()
        {
            clsDataConnection aDataConnection = new clsDataConnection();
            Assert.IsNotNull(aDataConnection);
        }
    }
}
