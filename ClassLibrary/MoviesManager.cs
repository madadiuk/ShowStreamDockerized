using System;
using System.Data;

public class MoviesManager
{
    private clsDataConnection db;

    public MoviesManager()
    {
        db = new clsDataConnection();
    }

    public DataTable GetAllMovies()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllMovies");
        return db.DataTable;
    }

    public void AddMovie(string title, string description, int genreId, string director, DateTime releaseDate, int duration)
    {
        db = new clsDataConnection();
        db.AddParameter("@Title", title);
        db.AddParameter("@Description", description);
        db.AddParameter("@GenreID", genreId);
        db.AddParameter("@Director", director);
        db.AddParameter("@ReleaseDate", releaseDate);
        db.AddParameter("@Duration", duration);
        db.Execute("spAddMovie");
    }

    public void UpdateMovie(int movieId, string title, string description, int genreId, string director, DateTime releaseDate, int duration)
    {
        db = new clsDataConnection();
        db.AddParameter("@MovieID", movieId);
        db.AddParameter("@Title", title);
        db.AddParameter("@Description", description);
        db.AddParameter("@GenreID", genreId);
        db.AddParameter("@Director", director);
        db.AddParameter("@ReleaseDate", releaseDate);
        db.AddParameter("@Duration", duration);
        db.Execute("spUpdateMovie");
    }

    public void DeleteMovie(int movieId)
    {
        db = new clsDataConnection();
        db.AddParameter("@MovieID", movieId);
        db.Execute("spDeleteMovie");
    }
}
