using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TransactionPaymentMethodValidationTests
{
    private TransactionManager _transactionManager;

    [TestInitialize]
    public void SetUp()
    {
        _transactionManager = new TransactionManager();
    }

    [TestMethod]
    public void PaymentMethodValid()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethod = "PayPal"; // assuming "PayPal" is a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodInvalid()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethod = "Cash"; // assuming "Cash" is not a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodEmpty()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethod = ""; // assuming empty string is not a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodNull()
    {
        // string variable to store any error message
        String error = "";
        // this should fail
        string paymentMethod = null; // assuming null is not a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreNotEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodDebitCard()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethod = "Debit Card"; // assuming "Debit Card" is a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

    [TestMethod]
    public void PaymentMethodCreditCard()
    {
        // string variable to store any error message
        String error = "";
        // this should pass
        string paymentMethod = "Credit Card"; // assuming "Credit Card" is a valid method
        // invoke the method
        error = _transactionManager.ValidatePaymentMethod(paymentMethod);
        // test to see that the result is correct
        Assert.AreEqual(error, "");
    }

}
