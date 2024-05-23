using System;
using System.Data;

public class GenresManager
{
    private clsDataConnection db;

    public GenresManager()
    {
        db = new clsDataConnection();
    }

    public DataTable GetAllGenres()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllGenres");
        return db.DataTable;
    }

    public void AddGenre(string name, string description)
    {
        db = new clsDataConnection();
        db.AddParameter("@Name", name);
        db.AddParameter("@Description", description);
        db.Execute("spAddGenre");
    }

    public void UpdateGenre(int genreId, string name, string description)
    {
        db = new clsDataConnection();
        db.AddParameter("@GenreID", genreId);
        db.AddParameter("@Name", name);
        db.AddParameter("@Description", description);
        db.Execute("spUpdateGenre");
    }

    public void DeleteGenre(int genreId)
    {
        db = new clsDataConnection();
        db.AddParameter("@GenreID", genreId);
        db.Execute("spDeleteGenre");
    }
}
