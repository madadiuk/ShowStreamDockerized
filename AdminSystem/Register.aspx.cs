using ClassLibrary;
using System;
using System.Web.UI;

public partial class Register : Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;
        string role = ddlRole.SelectedValue;

        UserManager userManager = new UserManager();
        User user = new User
        {
            Username = username,
            Email = email,
            Password = password,
            Role = role
        };

        try
        {
            userManager.AddUser(user);
            lblMessage.Text = "User registered successfully!";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}
