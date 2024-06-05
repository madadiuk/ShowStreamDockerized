using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TransactionAmountValidationTests
{
    private TransactionManager _transactionManager;

    [TestInitialize]
    public void SetUp()
    {
        _transactionManager = new TransactionManager();
    }

    [TestMethod]
    public void AmountMinLessOne()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should fail
        decimal Amount = -1; // assuming negative values are not allowed
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreNotEqual(Error, "");
    }

    [TestMethod]
    public void AmountMin()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should pass
        decimal Amount = 0; // assuming zero is a valid amount
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreEqual(Error, "");
    }

    [TestMethod]
    public void AmountMinPlusOne()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should pass
        decimal Amount = 1;
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreEqual(Error, "");
    }

    [TestMethod]
    public void AmountMaxLessOne()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should pass
        decimal Amount = 9999999.99m; // assuming 10,000,000 is the maximum limit
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreEqual(Error, "");
    }

    [TestMethod]
    public void AmountMax()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should pass
        decimal Amount = 10000000.00m; // assuming 10,000,000 is the maximum limit
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreEqual(Error, "");
    }

    [TestMethod]
    public void AmountMaxPlusOne()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should fail
        decimal Amount = 10000000.01m; // exceeding the maximum limit
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreNotEqual(Error, "");
    }

    [TestMethod]
    public void AmountMid()
    {
        // create an instance of the class we want to create
        clsTransaction transaction = new clsTransaction();
        // string variable to store any error message
        String Error = "";
        // this should pass
        decimal Amount = 5000000.00m; // mid value for the amount
        // invoke the method
        Error = transaction.Valid(Amount);
        // test to see that the result is correct
        Assert.AreEqual(Error, "");
    }
}

public class clsTransaction
{
    // Placeholder class for transaction validation
    public string Valid(decimal amount)
    {
        string error = "";

        if (amount < 0)
        {
            error = "Amount cannot be negative.";
        }
        else if (amount > 10000000.00m)
        {
            error = "Amount exceeds the maximum limit.";
        }

        return error;
    }
}
