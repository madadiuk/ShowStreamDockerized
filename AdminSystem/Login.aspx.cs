using ClassLibrary;
using System;

public partial class Login : System.Web.UI.Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        User user = userManager.AuthenticateUser(username, password);

        if (user != null)
        {
            // User authenticated successfully
            lblMessage.Text = "Login successful. Welcome, " + user.Username + "!";
            // Redirect to another page or do something else
            Response.Redirect("TeamMainMenu.aspx");
        }
        else
        {
            // Authentication failed
            lblMessage.Text = "Invalid username or password.";
        }
    }
}
