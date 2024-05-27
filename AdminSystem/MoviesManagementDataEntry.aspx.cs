using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MoviesManagementDataEntry : Page
{
    private MoviesManager moviesManager;

    protected void Page_Init(object sender, EventArgs e)
    {
        // Initialize moviesManager here to ensure it's available for all methods
        moviesManager = new MoviesManager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMoviesGrid();
        }
    }

    protected void btnAddMovie_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text.Trim();
        string description = txtDescription.Text.Trim();
        int genreId = int.Parse(txtGenreID.Text.Trim());
        string director = txtDirector.Text.Trim();
        DateTime releaseDate = DateTime.Parse(txtReleaseDate.Text.Trim());
        int duration = int.Parse(txtDuration.Text.Trim());

        if (!string.IsNullOrEmpty(title))
        {
            moviesManager.AddMovie(title, description, genreId, director, releaseDate, duration);
            lblMessage.Text = "Movie added successfully!";
            BindMoviesGrid();
        }
        else
        {
            lblMessage.Text = "Title is required.";
        }
    }

    private void BindMoviesGrid()
    {
        DataTable dtMovies = moviesManager.GetAllMovies();
        gvMovies.DataSource = dtMovies;
        gvMovies.DataBind();
    }

    protected void gvMovies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvMovies.EditIndex = e.NewEditIndex;
        BindMoviesGrid();
    }

    protected void gvMovies_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int movieId = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value.ToString());
        string title = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string description = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        int genreId = int.Parse(((TextBox)gvMovies.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        string director = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
        DateTime releaseDate = DateTime.Parse(((TextBox)gvMovies.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
        int duration = int.Parse(((TextBox)gvMovies.Rows[e.RowIndex].Cells[6].Controls[0]).Text);

        moviesManager.UpdateMovie(movieId, title, description, genreId, director, releaseDate, duration);
        gvMovies.EditIndex = -1;
        BindMoviesGrid();
        lblMessage.Text = "Movie updated successfully!";
    }

    protected void gvMovies_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int movieId = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value.ToString());
        moviesManager.DeleteMovie(movieId);
        BindMoviesGrid();
        lblMessage.Text = "Movie deleted successfully!";
    }
}
