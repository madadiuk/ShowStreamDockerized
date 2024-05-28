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
        gvTransactions.DataKeyNames = new string[] { "TransactionID" };
        gvTransactions.DataBind();
    }

    protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;
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

    protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            int transactionId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("TransactionRecordsViewer.aspx?TransactionID=" + transactionId);
        }
    }
    protected void btnAddNewTransaction_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsDataEntry.aspx");
    }
    protected void btnFilterTransactions_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsFilter.aspx");
    }
    protected void btnViewStatistics_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionStatistics.aspx");
    }
    protected void btnReturnToMainMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeamMainMenu.aspx");
    }

}
