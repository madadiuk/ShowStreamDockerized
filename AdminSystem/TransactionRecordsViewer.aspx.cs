using System;
using System.Web.UI;

public partial class TransactionRecordsViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int transactionId;
            if (int.TryParse(Request.QueryString["TransactionID"], out transactionId))
            {
                LoadTransactionDetails(transactionId);
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
            lblAmount.Text = "Amount: £" + Convert.ToDecimal(row["Amount"]).ToString("N2");
            lblTransactionDate.Text = "Transaction Date: " + Convert.ToDateTime(row["TransactionDate"]).ToString("d");
            lblPaymentMethod.Text = "Payment Method: " + row["PaymentMethod"].ToString();
            lblStatus.Text = "Status: " + row["Status"].ToString();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int transactionId;
        if (int.TryParse(Request.QueryString["TransactionID"], out transactionId))
        {
            Response.Redirect("TransactionRecordsEdit.aspx?TransactionID=" + transactionId);
        }
    }
}
