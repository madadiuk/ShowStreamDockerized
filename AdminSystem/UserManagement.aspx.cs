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

        try
        {
            userManager.AddUser(user);
            lblMessage.Text = "User added successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        BindUserGrid();
    }

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        BindUserGrid();

        GridViewRow row = gvUsers.Rows[e.NewEditIndex];
        DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
        if (ddlRole != null)
        {
            ddlRole.Items.Add(new ListItem("Admin", "Admin"));
            ddlRole.Items.Add(new ListItem("User", "User"));
        }
    }

    protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUsers.EditIndex = -1;
        BindUserGrid();
    }

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
        GridViewRow row = gvUsers.Rows[e.RowIndex];

        string username = ((TextBox)row.FindControl("txtUsername")).Text;
        string email = ((TextBox)row.FindControl("txtEmail")).Text;
        string role = ((DropDownList)row.FindControl("ddlRole")).SelectedValue;

        User user = new User
        {
            UserID = userId,
            Username = username,
            Email = email,
            Role = role
        };

        try
        {
            userManager.UpdateUser(user);
            lblMessage.Text = "User updated successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        gvUsers.EditIndex = -1;
        BindUserGrid();
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

        try
        {
            userManager.DeleteUser(userId);
            lblMessage.Text = "User deleted successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        BindUserGrid();
    }
}
