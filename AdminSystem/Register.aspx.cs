using ClassLibrary;
using System;

public partial class Register : System.Web.UI.Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Initial page load actions, if any
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();

        try
        {
            User user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = "User"  // Setting the role to "User" by default
            };

            userManager.AddUser(user);
            lblMessage.Text = "Registration successful. Redirecting to main menu...";
            lblMessage.ForeColor = System.Drawing.Color.Green;

            // Redirect to TeamMainMenu.aspx after a short delay
            Response.AddHeader("REFRESH", "3;URL=TeamMainMenu.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error during registration: " + ex.Message;
        }
    }
}
