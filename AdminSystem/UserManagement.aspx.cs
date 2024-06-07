using ClassLibrary;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManagement : Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserGrid();
        }
    }

    private void BindUserGrid()
    {
        try
        {
            gvUsers.DataSource = userManager.GetAllUsers();
            gvUsers.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error binding user grid: " + ex.Message;
        }
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        try
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
            lblMessage.Text = "User added successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error adding user: " + ex.Message;
        }
    }

    protected void btnEditUser_Click(object sender, EventArgs e)
    {
        try
        {
            int userId = Convert.ToInt32(txtUserID.Text);
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
            lblMessage.Text = "User updated successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error updating user: " + ex.Message;
        }
    }

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        BindUserGrid();
    }

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            string username = ((TextBox)row.Cells[1].Controls[0]).Text;
            string email = ((TextBox)row.Cells[2].Controls[0]).Text;
            string role = ((DropDownList)row.FindControl("ddlRoleEdit")).SelectedValue;

            User user = new User
            {
                UserID = userId,
                Username = username,
                Email = email,
                Role = role
            };

            userManager.UpdateUser(user);
            gvUsers.EditIndex = -1;
            BindUserGrid();
            lblMessage.Text = "User updated successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error updating user: " + ex.Message;
        }
    }

    protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUsers.EditIndex = -1;
        BindUserGrid();
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            userManager.DeleteUser(userId);
            BindUserGrid();
            lblMessage.Text = "User deleted successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error deleting user: " + ex.Message;
        }
    }



protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PromoteToAdmin")
        {
            int userId = Convert.ToInt32(e.CommandArgument);
            try
            {
                User user = userManager.GetUserById(userId);
                if (user != null)
                {
                    user.Role = "Admin";
                    userManager.UpdateUser(user);
                    BindUserGrid();
                    lblMessage.Text = "User promoted to admin successfully.";
                }
                else
                {
                    lblMessage.Text = "User not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error promoting user: " + ex.Message;
            }
        }
    }
}
