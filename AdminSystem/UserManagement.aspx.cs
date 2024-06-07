using ClassLibrary;
using System;
using System.Web.UI.WebControls;

public partial class UserManagement : System.Web.UI.Page
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

    protected void btnFindUser_Click(object sender, EventArgs e)
    {
        try
        {
            int userId = Convert.ToInt32(txtUserID.Text);
            User user = userManager.GetUserById(userId);

            if (user != null)
            {
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                ddlRole.SelectedValue = user.Role;
                lblMessage.Text = "User found.";
            }
            else
            {
                lblMessage.Text = "User not found.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error finding user: " + ex.Message;
        }
    }

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindUserGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error editing user: " + ex.Message;
        }
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

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            TextBox txtUsername = row.FindControl("txtUsername") as TextBox;
            TextBox txtEmail = row.FindControl("txtEmail") as TextBox;
            DropDownList ddlRole = row.FindControl("ddlRole") as DropDownList;

            if (txtUsername != null && txtEmail != null && ddlRole != null)
            {
                string username = txtUsername.Text;
                string email = txtEmail.Text;
                string role = ddlRole.SelectedValue;

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
            else
            {
                lblMessage.Text = "Error updating user: One or more fields are null.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error updating user: " + ex.Message;
        }
    }

    protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvUsers.EditIndex = -1;
            BindUserGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error canceling edit: " + ex.Message;
        }
    }
}
