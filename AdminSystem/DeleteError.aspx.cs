using System;

public partial class DeleteError : System.Web.UI.Page
{
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewGenres.aspx");
    }
}
