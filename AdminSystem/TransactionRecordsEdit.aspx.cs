using System;
using System.Web.UI;

public partial class TransactionRecordsEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int transactionId;
            if (int.TryParse(Request.QueryString["TransactionID"], out transactionId))
            {
                LoadTransactionDetails(transactionId);
                LoadStatuses();
                LoadPaymentMethods(); // Load the payment methods
            }
        }
    }

    private void LoadTransactionDetails(int transactionId)
    {
        TransactionManager tm = new TransactionManager();
        var dt = tm.GetTransactionById(transactionId);

        if (dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];
            lblTransactionID.Text = "Transaction ID: " + row["TransactionID"].ToString();
            lblUsername.Text = "Username: " + row["Username"].ToString();
            txtAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("N2");
            txtTransactionDate.Text = Convert.ToDateTime(row["TransactionDate"]).ToString("yyyy-MM-dd");
            ddlStatus.SelectedValue = row["Status"].ToString();
            ddlPaymentMethod.SelectedValue = row["PaymentMethod"].ToString(); // Set the payment method
            ViewState["UserID"] = row["UserID"]; // Store UserID in ViewState for later use
        }
    }

    private void LoadStatuses()
    {
        TransactionManager tm = new TransactionManager();
        ddlStatus.DataSource = tm.GetStatuses();
        ddlStatus.DataBind();
    }

    private void LoadPaymentMethods()
    {
        TransactionManager tm = new TransactionManager();
        ddlPaymentMethod.DataSource = tm.GetPaymentMethods();
        ddlPaymentMethod.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int transactionId = Convert.ToInt32(Request.QueryString["TransactionID"]);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            DateTime transactionDate = DateTime.Parse(txtTransactionDate.Text);
            string status = ddlStatus.SelectedValue;
            string paymentMethod = ddlPaymentMethod.SelectedValue; // Get the selected payment method
            int userId = (int)ViewState["UserID"]; // Retrieve UserID from ViewState

            TransactionManager tm = new TransactionManager();
            tm.UpdateTransaction(transactionId, amount, transactionDate, status, paymentMethod, userId); // Pass PaymentMethod

            lblMessage.Text = "Transaction updated successfully!";
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error updating transaction: " + ex.Message;
            lblMessage.Text = string.Empty;
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsList.aspx");
    }
}
