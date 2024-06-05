using ClassLibrary;
using System;
using System.Web.UI;

public partial class UserAccountsViewer : Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userId;
            if (int.TryParse(Request.QueryString["UserID"], out userId))
            {
                User user = userManager.GetUserById(userId);
                if (user != null)
                {
                    lblUserID.Text = user.UserID.ToString();
                    lblUsername.Text = user.Username;
                    lblEmail.Text = user.Email;
                    lblRole.Text = user.Role;
                }
                else
                {
                    lblUserID.Text = "N/A";
                    lblUsername.Text = "N/A";
                    lblEmail.Text = "N/A";
                    lblRole.Text = "N/A";
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAccountsList.aspx");
    }
}
