using System.Data;
using System;
using ClassLibrary;
using System.Collections.Generic;

public class TransactionManager
{
    private clsDataConnection connection;

    public TransactionManager()
    {
        connection = new clsDataConnection(); // Assuming clsDataConnection handles your DB connections
    }
    public DataTable GetTransactionStatistics()
    {
        connection.Execute("spGetTransactionStatistics");
        return connection.DataTable;
    }
    public DataTable GetFilteredTransactions(string paymentMethod, string status, DateTime? dateFrom, DateTime? dateTo)
    {
        if (!string.IsNullOrEmpty(paymentMethod))
            connection.AddParameter("@PaymentMethod", paymentMethod);
        if (!string.IsNullOrEmpty(status))
            connection.AddParameter("@Status", status);
        if (dateFrom.HasValue)
            connection.AddParameter("@DateFrom", dateFrom.Value);
        if (dateTo.HasValue)
            connection.AddParameter("@DateTo", dateTo.Value);

        connection.Execute("spGetFilteredTransactions");
        return connection.DataTable;
    }


    public DataTable GetTransactionById(int transactionId)
    {
        connection.AddParameter("@TransactionID", transactionId);
        connection.Execute("spGetTransactionById");
        return connection.DataTable;
    }


    public List<User> SearchUsers(string searchText)
    {
        List<User> users = new List<User>();
        try
        {
            connection.AddParameter("@SearchText", searchText);
            connection.Execute("spSearchUsers");  // This should now successfully call the stored procedure

            foreach (DataRow row in connection.DataTable.Rows)
            {
                users.Add(new User()
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception as appropriate
            System.Diagnostics.Debug.WriteLine("Error in SearchUsers: " + ex.Message);
        }
        return users;
    }


    public void AddTransaction(int userId, decimal amount, DateTime transactionDate, string paymentMethod, string status)
    {
        connection.AddParameter("@UserID", userId);
        connection.AddParameter("@Amount", amount);
        connection.AddParameter("@TransactionDate", transactionDate);
        connection.AddParameter("@PaymentMethod", paymentMethod);
        connection.AddParameter("@Status", status);
        connection.Execute("spAddTransaction");
    }

    public DataTable GetUsers()
    {
        connection.Execute("spGetAllUsers"); // Ensure you have a stored procedure named 'spGetUsers' that returns user IDs and names
        return connection.DataTable;
    }

    public void UpdateTransaction(int transactionId, decimal amount, DateTime transactionDate, string status, string paymentMethod, int userId)
    {
        connection.AddParameter("@TransactionID", transactionId);
        connection.AddParameter("@Amount", amount);
        connection.AddParameter("@TransactionDate", transactionDate);
        connection.AddParameter("@Status", status);
        connection.AddParameter("@PaymentMethod", paymentMethod); // Add this line
        connection.AddParameter("@UserID", userId); // Add this line
        connection.Execute("spUpdateTransaction");
    }



    public List<string> GetPaymentMethods()
    {
        return new List<string> { "PayPal", "Debit Card", "Credit Card" };
    }

    public List<string> GetStatuses()
    {
        return new List<string> { "Completed", "Pending", "Failed" };
    }

    public void DeleteTransaction(int transactionId)
    {
        connection.AddParameter("@TransactionID", transactionId);
        connection.Execute("spDeleteTransaction");
    }

    public DataTable GetAllTransactions()
    {
        connection.Execute("spGetAllTransactions");
        return connection.DataTable;
    }

    public DataTable GetAllTransactionDetails()
    {
        connection.Execute("spGetAllTransactionDetails");  // Ensure this stored procedure returns Username along with other transaction details
        return connection.DataTable;
    }
    public int GetUserIdByUsername(string username)
    {
        connection.AddParameter("@Username", username);
        connection.Execute("spFindUserByUsername");
        return connection.DataTable.Rows.Count > 0 ? Convert.ToInt32(connection.DataTable.Rows[0]["UserID"]) : 0;
    }


    // Retrieve transactions within a specific date range
    public DataTable GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
    {
        connection.AddParameter("@StartDate", startDate);
        connection.AddParameter("@EndDate", endDate);
        connection.Execute("spFilterTransactionsByDate");
        return connection.DataTable;
    }

    // Retrieve transactions by amount range
    public DataTable GetTransactionsByAmount(decimal minAmount, decimal maxAmount)
    {
        connection.AddParameter("@MinAmount", minAmount);
        connection.AddParameter("@MaxAmount", maxAmount);
        connection.Execute("spFilterTransactionsByAmount");
        return connection.DataTable;
    }

    // Retrieve transactions for a specific user
    public DataTable GetTransactionsByUser(int userId)
    {
        connection.AddParameter("@UserID", userId);
        connection.Execute("spFilterTransactionsByUser");
        return connection.DataTable;
    }

    // Additional Methods based on your database functionality
    // List transactions that occurred in the last N days
    public DataTable ListRecentTransactions(int recentDays)
    {
        connection.AddParameter("@RecentDays", recentDays);
        connection.Execute("spListRecentTransactions");
        return connection.DataTable;
    }

    // Retrieve transactions for audit within a specific date range
    public DataTable RetrieveTransactionsForAudit(DateTime startDate, DateTime endDate)
    {
        connection.AddParameter("@StartDate", startDate);
        connection.AddParameter("@EndDate", endDate);
        connection.Execute("spRetrieveTransactionsForAudit");
        return connection.DataTable;
    }

    // Dynamic search across multiple tables (Users, Movies, Series)
    public DataTable DynamicSearch(string searchTerm)
    {
        connection.AddParameter("@SearchTerm", searchTerm);
        connection.Execute("spDynamicSearch");
        return connection.DataTable;
    }

    // Generate a comprehensive user report that includes profile details, downloads, and transactions
    public DataSet GenerateUserReport(int userId)
    {
        connection.AddParameter("@UserID", userId);
        connection.Execute("spGenerateUserReport");
        DataSet results = new DataSet();
        results.Tables.Add(connection.DataTable);
        return results;
    }
    public string ValidateTransactionDate(DateTime transactionDate)
    {
        string error = "";

        if (transactionDate < DateTime.Now.Date)
        {
            error = "Transaction date cannot be in the past.";
        }
        else if (transactionDate > DateTime.Now.Date.AddYears(1))
        {
            error = "Transaction date cannot be more than one year in the future.";
        }

        return error;
    }

    public string ValidateTransactionDate(string transactionDate)
    {
        string error = "";
        DateTime dateTemp;

        if (!DateTime.TryParse(transactionDate, out dateTemp))
        {
            error = "Transaction date is not a valid date.";
        }
        else
        {
            error = ValidateTransactionDate(dateTemp);
        }

        return error;
    }

}
