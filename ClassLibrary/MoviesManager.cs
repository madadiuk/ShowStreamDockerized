using System;
using System.Collections.Generic;
using System.Data;

public class MoviesManager
{
    public string AddMovie(string title, string description, int genreID, string director, DateTime releaseDate, int duration)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return "Title cannot be empty.";
        }
        if (title.Length > 100)
        {
            return "Title cannot exceed 100 characters.";
        }
        if (description.Length > 1000)
        {
            return "Description cannot exceed 1000 characters.";
        }
        if (string.IsNullOrWhiteSpace(director))
        {
            return "Director cannot be empty.";
        }
        if (director.Length > 100)
        {
            return "Director cannot exceed 100 characters.";
        }
        if (duration <= 0)
        {
            return "Duration must be positive.";
        }

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Title", title);
            db.AddParameter("@Description", description);
            db.AddParameter("@GenreID", genreID);
            db.AddParameter("@Director", director);
            db.AddParameter("@ReleaseDate", releaseDate);
            db.AddParameter("@Duration", duration);
            db.Execute("spAddMovie");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string UpdateMovie(int movieID, string title, string description, int genreID, string director, DateTime releaseDate, int duration)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return "Title cannot be empty.";
        }
        if (title.Length > 100)
        {
            return "Title cannot exceed 100 characters.";
        }
        if (description.Length > 1000)
        {
            return "Description cannot exceed 1000 characters.";
        }
        if (string.IsNullOrWhiteSpace(director))
        {
            return "Director cannot be empty.";
        }
        if (director.Length > 100)
        {
            return "Director cannot exceed 100 characters.";
        }
        if (duration <= 0)
        {
            return "Duration must be positive.";
        }

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@MovieID", movieID);
            db.AddParameter("@Title", title);
            db.AddParameter("@Description", description);
            db.AddParameter("@GenreID", genreID);
            db.AddParameter("@Director", director);
            db.AddParameter("@ReleaseDate", releaseDate);
            db.AddParameter("@Duration", duration);
            db.Execute("spUpdateMovie");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeleteMovie(int movieID)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@MovieID", movieID);
            db.Execute("spDeleteMovie");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public bool CanDeleteMovie(int movieID)
    {
        clsDataConnection db = new clsDataConnection();
        db.AddParameter("@MovieID", movieID);
        db.Execute("spCanDeleteMovie");
        return db.Count == 0; // Return true if no related video files exist
    }

    public List<Movie> GetAllMovies()
    {
        List<Movie> movies = new List<Movie>();
        clsDataConnection db = new clsDataConnection();
        db.Execute("spGetAllMovies");
        foreach (DataRow row in db.DataTable.Rows)
        {
            movies.Add(new Movie
            {
                MovieID = Convert.ToInt32(row["MovieID"]),
                Title = row["Title"].ToString(),
                Description = row["Description"].ToString(),
                GenreID = Convert.ToInt32(row["GenreID"]),
                Director = row["Director"].ToString(),
                ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]),
                Duration = Convert.ToInt32(row["Duration"])
            });
        }
        return movies;
    }
}
