using System;

public partial class MovieDeleteError : System.Web.UI.Page
{
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMovies.aspx");
    }
}
