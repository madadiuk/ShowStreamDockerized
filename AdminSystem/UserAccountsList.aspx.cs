using ClassLibrary;
using System;
using System.Web.UI.WebControls;

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
        gvUsers.DataSource = userManager.GetAllUsers();
        gvUsers.DataBind();
    }

    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        BindUserGrid();
    }

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvUsers.Rows[e.RowIndex];
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Values[0]);
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

        userManager.UpdateUser(user);
        gvUsers.EditIndex = -1;
        BindUserGrid();
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Values[0]);
        userManager.DeleteUser(userId);
        BindUserGrid();
    }
}
