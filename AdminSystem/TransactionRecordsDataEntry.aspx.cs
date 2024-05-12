using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionRecordsDataEntry : System.Web.UI.Page
{
    protected void Application_Start(object sender, EventArgs e)
    {
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/scripts/jquery-1.12.4.min.js",
            DebugPath = "~/scripts/jquery-1.12.4.js",
            CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.12.4.min.js",
            CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.12.4.js"
        });
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateUsers();  // Populate the DropDownList for usernames
            PopulatePaymentMethods(); // Populate the DropDownList for payment methods
            PopulateStatuses(); // Populate the DropDownList for transaction statuses
            LoadTransactions(); // Load existing transactions into the grid
        }
    }

    private void PopulateUsers()
    {
        // Assuming you have a method in TransactionManager to get users
        TransactionManager tm = new TransactionManager();
        ddlUsername.DataSource = tm.GetUsers(); // Should return DataTable or List of users
        ddlUsername.DataBind();
    }

    private void PopulatePaymentMethods()
    {
        ddlPaymentMethod.Items.Add(new ListItem("PayPal", "PayPal"));
        ddlPaymentMethod.Items.Add(new ListItem("Debit Card", "Debit Card"));
        ddlPaymentMethod.Items.Add(new ListItem("Credit Card", "Credit Card"));
    }

    private void PopulateStatuses()
    {
        ddlStatus.Items.Add(new ListItem("Completed", "Completed"));
        ddlStatus.Items.Add(new ListItem("Pending", "Pending"));
        ddlStatus.Items.Add(new ListItem("Failed", "Failed"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int userId = int.Parse(ddlUsername.SelectedValue); // Get selected User ID from DropDownList
        decimal amount = decimal.Parse(txtAmount.Text);
        DateTime transactionDate = DateTime.Parse(txtTransactionDate.Text);
        string paymentMethod = ddlPaymentMethod.SelectedValue; // Get selected payment method from DropDownList
        string status = ddlStatus.SelectedValue; // Get selected status from DropDownList

        TransactionManager tm = new TransactionManager();
        tm.AddTransaction(userId, amount, transactionDate, paymentMethod, status);
        lblMessage.Text = "Transaction saved successfully!";
        LoadTransactions();
    }

    private void LoadTransactions()
    {
        TransactionManager tm = new TransactionManager();
        gvTransactions.DataSource = tm.GetAllTransactionDetails();  // Ensure this method calls the new or updated stored procedure
        gvTransactions.DataBind();
    }
}
