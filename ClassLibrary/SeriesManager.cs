using System;
using System.Data;

public class SeriesManager
{
    private clsDataConnection db;

    public SeriesManager()
    {
        db = new clsDataConnection();
    }

    public DataTable GetAllSeries()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllSeries");
        return db.DataTable;
    }

    public void AddSeries(string title, int genreId, DateTime startYear, DateTime endYear, string country)
    {
        db = new clsDataConnection();
        db.AddParameter("@Title", title);
        db.AddParameter("@GenreID", genreId);
        db.AddParameter("@StartYear", startYear);
        db.AddParameter("@EndYear", endYear);
        db.AddParameter("@Country", country);
        db.Execute("spAddSeries");
    }

    public void UpdateSeries(int seriesId, string title, int genreId, DateTime startYear, DateTime endYear, string country)
    {
        db = new clsDataConnection();
        db.AddParameter("@SeriesID", seriesId);
        db.AddParameter("@Title", title);
        db.AddParameter("@GenreID", genreId);
        db.AddParameter("@StartYear", startYear);
        db.AddParameter("@EndYear", endYear);
        db.AddParameter("@Country", country);
        db.Execute("spUpdateSeries");
    }

    public void DeleteSeries(int seriesId)
    {
        db = new clsDataConnection();
        db.AddParameter("@SeriesID", seriesId);
        db.Execute("spDeleteSeries");
    }
}
