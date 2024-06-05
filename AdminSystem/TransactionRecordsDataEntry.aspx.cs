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
            
            PopulatePaymentMethods(); // Populate the DropDownList for payment methods
            PopulateStatuses(); // Populate the DropDownList for transaction statuses
            LoadTransactions(); // Load existing transactions into the grid
        }
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
        int userId = Convert.ToInt32(Request.Form["usernameInput"]); // Read the value from hidden input
        decimal amount = decimal.Parse(txtAmount.Text);
        DateTime transactionDate = DateTime.Parse(txtTransactionDate.Text);
        string paymentMethod = ddlPaymentMethod.SelectedValue;
        string status = ddlStatus.SelectedValue;

        TransactionManager tm = new TransactionManager();
        tm.AddTransaction(userId, amount, transactionDate, paymentMethod, status);
        lblMessage.Text = "Transaction saved successfully!";

        // Clear the form fields
        ClearFormFields();

        // Reload transactions
        LoadTransactions();
    }

    private void ClearFormFields()
    {
        txtAmount.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        ddlPaymentMethod.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        // Clear the hidden input field for username
        ScriptManager.RegisterStartupScript(this, this.GetType(), "clearUsernameInput", "document.getElementById('usernameInput').value = '';", true);
    }
    private void LoadTransactions()
    {
        TransactionManager tm = new TransactionManager();
        gvTransactions.DataSource = tm.GetAllTransactionDetails();  // Ensure this method calls the new or updated stored procedure
        gvTransactions.DataBind();
    }
    protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;
        LoadTransactions();
    }
    protected void btnViewList_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsList.aspx");
    }
}
