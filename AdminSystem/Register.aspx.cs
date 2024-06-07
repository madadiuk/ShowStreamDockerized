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
            lblMessage.Text = "Registration successful. You can now log in.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error during registration: " + ex.Message;
        }
    }
}
