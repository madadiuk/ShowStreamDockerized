using System;

public partial class MoviesManagementDataEntry : System.Web.UI.Page
{
    protected void btnAddMovie_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text;
        string description = txtDescription.Text;
        int genreID = int.Parse(txtGenreID.Text);
        string director = txtDirector.Text;
        DateTime releaseDate = DateTime.Parse(txtReleaseDate.Text);
        int duration = int.Parse(txtDuration.Text);

        Movie movie = new Movie
        {
            Title = title,
            Description = description,
            GenreID = genreID,
            Director = director,
            ReleaseDate = releaseDate,
            Duration = duration
        };

        MoviesCollection moviesCollection = new MoviesCollection();
        moviesCollection.AddMovie(movie);

        lblMessage.Text = "Movie added successfully!";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewMovies.aspx");
    }
}
