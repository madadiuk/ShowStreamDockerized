using System;
using System.Data;
using System.Web.UI;

public partial class TransactionStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStatistics();
        }
    }

    private void LoadStatistics()
    {
        TransactionManager tm = new TransactionManager();
        DataTable stats = tm.GetTransactionStatistics();

        if (stats.Rows.Count > 0)
        {
            DataRow row = stats.Rows[0];
            lblTotalTransactions.Text = "Total Transactions: " + row["TotalTransactions"].ToString();
            lblTotalAmount.Text = "Total Amount: £" + Convert.ToDecimal(row["TotalAmount"]).ToString("N2");
            lblAverageAmount.Text = "Average Amount: £" + Convert.ToDecimal(row["AverageAmount"]).ToString("N2");
            lblUniqueUsers.Text = "Unique Users: " + row["UniqueUsers"].ToString();
        }
    }

    protected void btnReturnToList_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsList.aspx");
    }
}
