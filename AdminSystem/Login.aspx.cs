using ClassLibrary;
using System;
using System.Web.UI;

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Any initialization if needed
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        UserManager userManager = new UserManager();
        User authenticatedUser; // Declare the variable here

        if (userManager.AuthenticateUser(username, password, out authenticatedUser))
        {
            Session["UserID"] = authenticatedUser.UserID;
            Session["Username"] = authenticatedUser.Username;
            Session["Role"] = authenticatedUser.Role;
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            lblMessage.Text = "Invalid username or password.";
        }
    }
}
