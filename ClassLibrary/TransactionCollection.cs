using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }

    public class TransactionCollection
    {
        private List<Transaction> transactions = new List<Transaction>();

        public void Add(Transaction transaction) => transactions.Add(transaction);

        public void Delete(int transactionID) => transactions.RemoveAll(t => t.TransactionID == transactionID);

        public void Edit(Transaction transaction)
        {
            var existingTransaction = transactions.FirstOrDefault(t => t.TransactionID == transaction.TransactionID);
            if (existingTransaction != null)
            {
                existingTransaction.UserID = transaction.UserID;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.TransactionDate = transaction.TransactionDate;
                existingTransaction.PaymentMethod = transaction.PaymentMethod;
                existingTransaction.Status = transaction.Status;
            }
        }

        public List<Transaction> Filter(Func<Transaction, bool> predicate) => transactions.Where(predicate).ToList();

        public Transaction Find(int transactionID) => transactions.FirstOrDefault(t => t.TransactionID == transactionID);
    }
}
