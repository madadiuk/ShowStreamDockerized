using System;
using System.Data;

public class VideoFilesManager
{
    private clsDataConnection db;

    public VideoFilesManager()
    {
        db = new clsDataConnection();
    }

    public DataTable GetAllVideoFiles()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllVideoFiles");
        return db.DataTable;
    }

    public void AddVideoFile(int? movieId, int? seriesId, int? episodeId, string videoQuality, string filePath, long fileSize)
    {
        db = new clsDataConnection();
        db.AddParameter("@MovieID", movieId);
        db.AddParameter("@SeriesID", seriesId);
        db.AddParameter("@EpisodeID", episodeId);
        db.AddParameter("@VideoQuality", videoQuality);
        db.AddParameter("@FilePath", filePath);
        db.AddParameter("@FileSize", fileSize);
        db.Execute("spAddVideoFile");
    }

    public void UpdateVideoFile(int videoFileId, int? movieId, int? seriesId, int? episodeId, string videoQuality, string filePath, long fileSize)
    {
        db = new clsDataConnection();
        db.AddParameter("@VideoFileID", videoFileId);
        db.AddParameter("@MovieID", movieId);
        db.AddParameter("@SeriesID", seriesId);
        db.AddParameter("@EpisodeID", episodeId);
        db.AddParameter("@VideoQuality", videoQuality);
        db.AddParameter("@FilePath", filePath);
        db.AddParameter("@FileSize", fileSize);
        db.Execute("spUpdateVideoFile");
    }

    public void DeleteVideoFile(int videoFileId)
    {
        db = new clsDataConnection();
        db.AddParameter("@VideoFileID", videoFileId);
        db.Execute("spDeleteVideoFile");
    }

    public DataTable FilterVideoFilesByQuality(string videoQuality)
    {
        db = new clsDataConnection();
        db.AddParameter("@VideoQuality", videoQuality);
        db.Execute("spFilterVideoFilesByQuality");
        return db.DataTable;
    }

    public DataTable GetAllMovies()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllMovies");
        return db.DataTable;
    }

    public DataTable GetAllSeries()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllSeries");
        return db.DataTable;
    }

    public DataTable GetAllEpisodes()
    {
        db = new clsDataConnection();
        db.Execute("spGetAllEpisodes");
        return db.DataTable;
    }
}
