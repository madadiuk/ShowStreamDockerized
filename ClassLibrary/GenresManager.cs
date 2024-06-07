using System;
using System.Collections.Generic;
using System.Data;

public class GenresManager
{
    public string AddGenre(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Genre name cannot be empty.";
        }
        if (name.Length > 50)
        {
            return "Genre name cannot exceed 50 characters.";
        }
        if (description.Length > 255)
        {
            return "Genre description cannot exceed 255 characters.";
        }

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Name", name);
            db.AddParameter("@Description", description);
            db.Execute("spAddGenre");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string UpdateGenre(int genreID, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Genre name cannot be empty.";
        }
        if (name.Length > 50)
        {
            return "Genre name cannot exceed 50 characters.";
        }
        if (description.Length > 255)
        {
            return "Genre description cannot exceed 255 characters.";
        }

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@GenreID", genreID);
            db.AddParameter("@Name", name);
            db.AddParameter("@Description", description);
            db.Execute("spUpdateGenre");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeleteGenre(int genreID)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@GenreID", genreID);
            db.Execute("spDeleteGenre");
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public bool CanDeleteGenre(int genreID)
    {
        clsDataConnection db = new clsDataConnection();
        db.AddParameter("@GenreID", genreID);
        db.Execute("spCanDeleteGenre");
        return db.Count == 0; // Return true if no related movies exist
    }

    public List<Genre> GetAllGenres()
    {
        List<Genre> genres = new List<Genre>();
        clsDataConnection db = new clsDataConnection();
        db.Execute("spGetAllGenres");
        foreach (DataRow row in db.DataTable.Rows)
        {
            genres.Add(new Genre
            {
                GenreID = Convert.ToInt32(row["GenreID"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString()
            });
        }
        return genres;
    }
}
