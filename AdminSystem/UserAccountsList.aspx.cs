using ClassLibrary;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class UserAccountsList : System.Web.UI.Page
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

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        BindUserGrid();
    }

    protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUsers.EditIndex = -1;
        BindUserGrid();
    }

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvUsers.Rows[e.RowIndex];
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
        string username = (row.FindControl("txtUsername") as TextBox).Text;
        string email = (row.FindControl("txtEmail") as TextBox).Text;
        string role = (row.FindControl("ddlRole") as DropDownList).SelectedValue;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(role))
        {
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
                gvUsers.EditIndex = -1;
                BindUserGrid();
                lblMessage.Text = "User updated successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating user: " + ex.Message;
            }
        }
        else
        {
            lblMessage.Text = "Error updating user: One or more fields are null.";
        }
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

        try
        {
            userManager.DeleteUser(userId);
            BindUserGrid();
            lblMessage.Text = "User deleted successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error deleting user: " + ex.Message;
        }
    }
}
