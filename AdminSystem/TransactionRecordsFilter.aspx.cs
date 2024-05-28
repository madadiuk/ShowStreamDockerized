using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionRecordsFilter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // You can optionally populate the filter dropdowns here if needed
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadFilteredTransactions();
    }

    private void LoadFilteredTransactions()
    {
        string paymentMethod = ddlPaymentMethodFilter.SelectedValue;
        string status = ddlStatusFilter.SelectedValue;
        DateTime? dateFrom = string.IsNullOrEmpty(txtDateFrom.Text) ? (DateTime?)null : DateTime.Parse(txtDateFrom.Text);
        DateTime? dateTo = string.IsNullOrEmpty(txtDateTo.Text) ? (DateTime?)null : DateTime.Parse(txtDateTo.Text);

        TransactionManager tm = new TransactionManager();
        gvFilteredTransactions.DataSource = tm.GetFilteredTransactions(paymentMethod, status, dateFrom, dateTo);
        gvFilteredTransactions.DataBind();
    }

    protected void gvFilteredTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFilteredTransactions.PageIndex = e.NewPageIndex;
        LoadFilteredTransactions();
    }

    protected void gvFilteredTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int transactionId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("TransactionRecordsViewer.aspx?TransactionID=" + transactionId);
        }
        else if (e.CommandName == "Delete")
        {
            int transactionId = Convert.ToInt32(e.CommandArgument);
            DeleteTransaction(transactionId);
        }
    }

    protected void gvFilteredTransactions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // This method is required to handle the GridView's deleting event, but the actual deletion is handled in gvFilteredTransactions_RowCommand
    }

    private void DeleteTransaction(int transactionId)
    {
        try
        {
            TransactionManager tm = new TransactionManager();
            tm.DeleteTransaction(transactionId);
            LoadFilteredTransactions();
            lblMessage.Text = "Transaction deleted successfully!";
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error deleting transaction: " + ex.Message;
            lblMessage.Text = string.Empty;
        }
    }

    protected void btnReturnToList_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsList.aspx");
    }
}
