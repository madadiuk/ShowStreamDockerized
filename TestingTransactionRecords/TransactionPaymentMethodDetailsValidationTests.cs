using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TransactionPaymentMethodDetailsValidationTests
{
    private TransactionManager _transactionManager;

    [TestInitialize]
    public void SetUp()
    {
        _transactionManager = new TransactionManager();
    }

    [TestMethod]
    public void PaymentMethodDetailsValid()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethodDetails = "Valid Details"; // assuming valid details
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDetailsEmpty()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethodDetails = ""; // assuming empty string is not valid
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDetailsNull()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethodDetails = null; // assuming null is not valid
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDetailsMin()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethodDetails = "a"; // assuming min length of 1 is valid
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDetailsMax()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethodDetails = new string('a', 255); // assuming max length of 255 is valid
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDetailsMaxPlusOne()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethodDetails = new string('a', 256); // exceeding max length
        // invoke the method
        error = _transactionManager.ValidatePaymentMethodDetails(paymentMethodDetails);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }
}
