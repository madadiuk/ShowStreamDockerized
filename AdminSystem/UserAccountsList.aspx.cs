using ClassLibrary;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccountsList : Page
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

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            string username = (row.FindControl("txtUsername") as TextBox).Text;
            string email = (row.FindControl("txtEmail") as TextBox).Text;
            string role = (row.FindControl("ddlRole") as DropDownList).SelectedValue;

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
}
