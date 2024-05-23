using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

public partial class UserAccountsList : Page
{
    private UserManager userManager = new UserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUsers();
        }
    }

    private void LoadUsers()
    {
        List<User> users = userManager.GetAllUsers();
        gvUsers.DataSource = users;
        gvUsers.DataBind();
    }

    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int userId = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ViewUser")
        {
            Response.Redirect("UserAccountsViewer.aspx?UserID=" + userId);
        }
        else if (e.CommandName == "DeleteUser")
        {
            userManager.DeleteUser(userId);
            LoadUsers();
        }
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        LoadUsers();
    }
}
