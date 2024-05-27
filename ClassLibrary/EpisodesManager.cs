using System;
using System.Data;

public class EpisodesManager
{
    private clsDataConnection db;

    public EpisodesManager()
    {
        db = new clsDataConnection();
    }

    public DataTable GetAllEpisodes()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllEpisodes");
        return db.DataTable;
    }

    public void AddEpisode(int seriesId, int seasonNumber, int episodeNumber, string title, string description, DateTime releaseDate)
    {
        db = new clsDataConnection();
        db.AddParameter("@SeriesID", seriesId);
        db.AddParameter("@SeasonNumber", seasonNumber);
        db.AddParameter("@EpisodeNumber", episodeNumber);
        db.AddParameter("@Title", title);
        db.AddParameter("@Description", description);
        db.AddParameter("@ReleaseDate", releaseDate);
        db.Execute("spAddEpisode");
    }

    public void UpdateEpisode(int episodeId, int seriesId, int seasonNumber, int episodeNumber, string title, string description, DateTime releaseDate)
    {
        db = new clsDataConnection();
        db.AddParameter("@EpisodeID", episodeId);
        db.AddParameter("@SeriesID", seriesId);
        db.AddParameter("@SeasonNumber", seasonNumber);
        db.AddParameter("@EpisodeNumber", episodeNumber);
        db.AddParameter("@Title", title);
        db.AddParameter("@Description", description);
        db.AddParameter("@ReleaseDate", releaseDate);
        db.Execute("spUpdateEpisode");
    }

    public void DeleteEpisode(int episodeId)
    {
        db = new clsDataConnection();
        db.AddParameter("@EpisodeID", episodeId);
        db.Execute("spDeleteEpisode");
    }

    public DataTable FilterEpisodes(int? seriesId, int? seasonNumber, int? episodeNumber, DateTime? releaseDate)
    {
        db = new clsDataConnection();
        if (seriesId.HasValue)
        {
            db.AddParameter("@SeriesID", seriesId.Value);
        }
        if (seasonNumber.HasValue)
        {
            db.AddParameter("@SeasonNumber", seasonNumber.Value);
        }
        if (episodeNumber.HasValue)
        {
            db.AddParameter("@EpisodeNumber", episodeNumber.Value);
        }
        if (releaseDate.HasValue)
        {
            db.AddParameter("@ReleaseDate", releaseDate.Value);
        }
        db.Execute("spFilterEpisodes");
        return db.DataTable;
    }
}
