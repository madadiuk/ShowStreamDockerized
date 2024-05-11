using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionRecordsDataEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTransactions();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int userId = int.Parse(txtUserId.Text);
        decimal amount = decimal.Parse(txtAmount.Text);
        DateTime transactionDate = DateTime.Parse(txtTransactionDate.Text);
        string paymentMethod = txtPaymentMethod.Text;
        string status = txtStatus.Text;

        TransactionManager tm = new TransactionManager();
        tm.AddTransaction(userId, amount, transactionDate, paymentMethod, status);
        lblMessage.Text = "Transaction saved successfully!";
        LoadTransactions();
    }

    private void LoadTransactions()
    {
        TransactionManager tm = new TransactionManager();
        gvTransactions.DataSource = tm.GetAllTransactions();
        gvTransactions.DataBind();
    }
}