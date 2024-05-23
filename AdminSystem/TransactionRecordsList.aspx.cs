using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionRecordsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTransactions();
        }
    }

    private void LoadTransactions()
    {
        TransactionManager tm = new TransactionManager();
        gvTransactions.DataSource = tm.GetAllTransactionDetails();
        gvTransactions.DataBind();
    }

    protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;
        LoadTransactions();
    }

    protected void gvTransactions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTransactions.EditIndex = e.NewEditIndex;
        LoadTransactions();
    }

    protected void gvTransactions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvTransactions.Rows[e.RowIndex];
            int transactionId = Convert.ToInt32(gvTransactions.DataKeys[e.RowIndex].Values[0]);
            decimal amount = Convert.ToDecimal(((TextBox)row.FindControl("txtAmount")).Text);
            DateTime transactionDate = DateTime.Parse(((TextBox)row.FindControl("txtTransactionDate")).Text);
            string paymentMethod = ((DropDownList)row.FindControl("ddlPaymentMethod")).SelectedValue;
            string status = ((DropDownList)row.FindControl("ddlStatus")).SelectedValue;

            TransactionManager tm = new TransactionManager();
            tm.UpdateTransaction(transactionId, amount, transactionDate, paymentMethod, status);

            gvTransactions.EditIndex = -1;
            LoadTransactions();
            lblMessage.Text = "Transaction updated successfully!";
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error updating transaction: " + ex.Message;
            lblMessage.Text = string.Empty;
        }
    }

    protected void gvTransactions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTransactions.EditIndex = -1;
        LoadTransactions();
    }

    protected void gvTransactions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int transactionId = Convert.ToInt32(gvTransactions.DataKeys[e.RowIndex].Values[0]);

            TransactionManager tm = new TransactionManager();
            tm.DeleteTransaction(transactionId);

            LoadTransactions();
            lblMessage.Text = "Transaction deleted successfully!";
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error deleting transaction: " + ex.Message;
            lblMessage.Text = string.Empty;
        }
    }
    protected void gvTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            DropDownList ddlPaymentMethod = (DropDownList)e.Row.FindControl("ddlPaymentMethod");
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

            TransactionManager tm = new TransactionManager();

            // Populate Payment Methods
            ddlPaymentMethod.DataSource = tm.GetPaymentMethods();
            ddlPaymentMethod.DataBind();

            // Set the selected value for payment method
            string paymentMethod = DataBinder.Eval(e.Row.DataItem, "PaymentMethod").ToString();
            if (ddlPaymentMethod.Items.FindByValue(paymentMethod) != null)
            {
                ddlPaymentMethod.SelectedValue = paymentMethod;
            }
            else
            {
                // Handle the case where the value is not found
                ddlPaymentMethod.SelectedIndex = -1;
            }

            // Populate Statuses
            ddlStatus.DataSource = tm.GetStatuses();
            ddlStatus.DataBind();

            // Set the selected value for status
            string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
            if (ddlStatus.Items.FindByValue(status) != null)
            {
                ddlStatus.SelectedValue = status;
            }
            else
            {
                // Handle the case where the value is not found
                ddlStatus.SelectedIndex = -1;
            }
            // Debugging: Print the SelectedValue
            System.Diagnostics.Debug.WriteLine("Status: " + status);
            Response.Write("<script>console.log('Status: " + status + "');</script>");
        }
    }

}
