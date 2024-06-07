using System;
using System.Web.UI;

public partial class TeamMainMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGoToTransactions_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionRecordsList.aspx");
    }
    protected void btnGoToEpisodesManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect("EpisodesManagementDataEntry.aspx");
    }

    protected void btnGoToGenresManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewGenres.aspx");
    }

    protected void btnGoToUserManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserManagement.aspx");
    }
    protected void btnGoToMoviesManagementDataEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMovies.aspx");
    }
}
