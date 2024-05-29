using System;
using System.Collections.Generic;
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
        BindPager();
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

    private void BindPager()
    {
        int pageCount = gvTransactions.PageCount;
        int pageIndex = gvTransactions.PageIndex;
        int startPage = Math.Max(1, pageIndex - 5);
        int endPage = Math.Min(pageCount, pageIndex + 5);

        List<int> pages = new List<int>();
        for (int i = startPage; i <= endPage; i++)
        {
            pages.Add(i);
        }

        Repeater rptPager = (Repeater)gvTransactions.BottomPagerRow.FindControl("rptPager");
        if (rptPager != null)
        {
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
    }

    protected void rptPager_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkPage = e.Item.FindControl("lnkPage") as LinkButton;
        if (lnkPage != null)
        {
            int pageNumber = (int)e.Item.DataItem;
            if (pageNumber == gvTransactions.PageIndex + 1)
            {
                lnkPage.CssClass += " active";
            }
        }
    }
}
