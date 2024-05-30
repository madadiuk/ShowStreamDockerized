using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TransactionStatusValidationTests
{
    private TransactionManager _transactionManager;

    [TestInitialize]
    public void SetUp()
    {
        _transactionManager = new TransactionManager();
    }

    [TestMethod]
    public void StatusValidCompleted()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string status = "Completed"; // assuming "Completed" is a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void StatusValidPending()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string status = "Pending"; // assuming "Pending" is a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void StatusValidFailed()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string status = "Failed"; // assuming "Failed" is a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void StatusInvalid()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string status = "Unknown"; // assuming "Unknown" is not a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void StatusEmpty()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string status = ""; // assuming empty string is not a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void StatusNull()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string status = null; // assuming null is not a valid status
        // invoke the method
        error = _transactionManager.ValidateStatus(status);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }
}
