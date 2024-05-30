using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TransactionDateValidationTests
{
    private TransactionManager _transactionManager;

    [TestInitialize]
    public void SetUp()
    {
        _transactionManager = new TransactionManager();
    }

    [TestMethod]
    public void TransactionDateExtremeMin()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        DateTime transactionDate = DateTime.Now.AddYears(-100);
        // invoke the method
        error = _transactionManager.ValidateTransactionDate(transactionDate);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void TransactionDateMinLessOne()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        DateTime transactionDate = DateTime.Now.AddDays(-1);
        // invoke the method
        error = _transactionManager.ValidateTransactionDate(transactionDate);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void TransactionDateMin()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        DateTime transactionDate = DateTime.Now;
        // invoke the method
        error = _transactionManager.ValidateTransactionDate(transactionDate);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void TransactionDateMinPlusOne()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        DateTime transactionDate = DateTime.Now.AddDays(1);
        // invoke the method
        error = _transactionManager.ValidateTransactionDate(transactionDate);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void TransactionDateExtremeMax()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        DateTime transactionDate = DateTime.Now.AddYears(100);
        // invoke the method
        error = _transactionManager.ValidateTransactionDate(transactionDate);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void TransactionDateInvalid()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string invalidDate = "invalid date";
        // invoke the method with invalid date
        error = _transactionManager.ValidateTransactionDate(invalidDate);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }
}
