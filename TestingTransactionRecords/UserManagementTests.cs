using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace TestingTransactionRecords
{
    [TestClass]
    public class UserManagementTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void InstanceOK()
        {
            clsDataConnection aDataConnection = new clsDataConnection();
            Assert.IsNotNull(aDataConnection);
        }
    }
}
