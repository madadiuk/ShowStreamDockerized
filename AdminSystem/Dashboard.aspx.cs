using ClassLibrary;
using System;
using System.Web.UI;

public partial class Dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                lblUsername.Text = user.Username;
                lblRole.Text = user.Role;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}
