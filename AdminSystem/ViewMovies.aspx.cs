using System;
using System.Web.UI.WebControls;

public partial class ViewMovies : System.Web.UI.Page
{
    private MoviesCollection moviesCollection;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            moviesCollection = new MoviesCollection();
            Session["MoviesCollection"] = moviesCollection;
            LoadMovies();
        }
        else
        {
            moviesCollection = Session["MoviesCollection"] as MoviesCollection;
        }
    }

    private void LoadMovies()
    {
        gvMovies.DataSource = moviesCollection.MoviesList;
        gvMovies.DataBind();
    }

    protected void btnAddMovie_Click(object sender, EventArgs e)
    {
        Response.Redirect("MoviesManagementDataEntry.aspx");
    }

    protected void btnMainMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeamMainMenu.aspx");
    }

    protected void gvMovies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvMovies.EditIndex = e.NewEditIndex;
        LoadMovies();
    }

    protected void gvMovies_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        moviesCollection = Session["MoviesCollection"] as MoviesCollection;

        int movieID = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value);
        string title = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string description = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        int genreID = Convert.ToInt32(((TextBox)gvMovies.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        string director = ((TextBox)gvMovies.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
        DateTime releaseDate = Convert.ToDateTime(((TextBox)gvMovies.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
        int duration = Convert.ToInt32(((TextBox)gvMovies.Rows[e.RowIndex].Cells[6].Controls[0]).Text);

        Movie movie = new Movie { MovieID = movieID, Title = title, Description = description, GenreID = genreID, Director = director, ReleaseDate = releaseDate, Duration = duration };
        moviesCollection.UpdateMovie(movie);

        gvMovies.EditIndex = -1;
        LoadMovies();
        Session["MoviesCollection"] = moviesCollection; // Update session
    }

    protected void gvMovies_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvMovies.EditIndex = -1;
        LoadMovies();
    }

    protected void gvMovies_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        moviesCollection = Session["MoviesCollection"] as MoviesCollection;

        int movieID = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value);

        if (!moviesCollection.TryDeleteMovie(movieID))
        {
            Response.Redirect("MovieDeleteError.aspx");
        }
        else
        {
            LoadMovies();
            Session["MoviesCollection"] = moviesCollection; // Update session
        }
    }
}
