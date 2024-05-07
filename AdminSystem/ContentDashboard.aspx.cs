using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContentDashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnTestConnection_Click(object sender, EventArgs e)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.TestConnection();
            lblMessage.Text = "Connection successful!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Connection failed: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
