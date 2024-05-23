using ClassLibrary;
using System;
using System.Web.UI;

public partial class UserAccountsViewer : Page
{
    private UserManager userManager;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            userManager = new UserManager();
            int userId = Convert.ToInt32(Request.QueryString["UserID"]);
            User user = userManager.GetUserById(userId);
            lblUserIDValue.Text = user.UserID.ToString();
            lblUsernameValue.Text = user.Username;
            lblEmailValue.Text = user.Email;
            lblRoleValue.Text = user.Role;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAccountsList.aspx");
    }
}
