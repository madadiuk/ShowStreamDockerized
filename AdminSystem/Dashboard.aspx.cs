using System;
using System.Web.UI;

public partial class Dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Username"] != null && Session["Role"] != null)
            {
                lblUsername.Text = Session["Username"].ToString();
                lblRole.Text = Session["Role"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Login.aspx");
    }
}
