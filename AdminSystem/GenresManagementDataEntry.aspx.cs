using System;

public partial class GenresManagementDataEntry : System.Web.UI.Page
{
    protected void btnAddGenre_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string description = txtDescription.Text;

        Genre genre = new Genre { Name = name, Description = description };
        GenresCollection genresCollection = new GenresCollection();
        genresCollection.AddGenre(genre);

        lblMessage.Text = "Genre added successfully!";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewGenres.aspx");
    }
}
