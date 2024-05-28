using ClassLibrary;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManagement : Page
{
    private UserManager userManager;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            userManager = new UserManager();
            BindUserGrid();
        }
    }

    private void BindUserGrid()
    {
        gvUsers.DataSource = userManager.GetAllUsers();
        gvUsers.DataBind();
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        User user = new User
        {
            Username = txtUsername.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text,
            Role = ddlRole.SelectedValue
        };
        userManager.AddUser(user);
        BindUserGrid();
    }

    protected void btnEditUser_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(hfUserID.Value);
        User user = new User
        {
            UserID = userId,
            Username = txtUsername.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text,
            Role = ddlRole.SelectedValue
        };
        userManager.UpdateUser(user);
        BindUserGrid();
    }

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.NewEditIndex].Value);
        User user = userManager.GetUserById(userId);
        hfUserID.Value = user.UserID.ToString();
        txtUsername.Text = user.Username;
        txtEmail.Text = user.Email;
        ddlRole.SelectedValue = user.Role;
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
        userManager.DeleteUser(userId);
        BindUserGrid();
    }
}
