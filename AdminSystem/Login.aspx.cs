using ClassLibrary;
using System;

public partial class Login : System.Web.UI.Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Initial page load actions, if any
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        try
        {
            User user = userManager.AuthenticateUser(username, password);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["Role"] = user.Role;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error during login: " + ex.Message;
        }
    }
}
