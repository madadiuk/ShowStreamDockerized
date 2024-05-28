using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VideoFilesManagementDataEntry : Page
{
    private VideoFilesManager videoFilesManager;

    protected void Page_Init(object sender, EventArgs e)
    {
        videoFilesManager = new VideoFilesManager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdownLists();
            PopulateVideoQualityFilter();
            BindVideoFilesGrid();
        }
    }

    protected void btnAddVideoFile_Click(object sender, EventArgs e)
    {
        int? movieId = ddlMovieID.SelectedValue == "" ? (int?)null : int.Parse(ddlMovieID.SelectedValue);
        int? seriesId = ddlSeriesID.SelectedValue == "" ? (int?)null : int.Parse(ddlSeriesID.SelectedValue);
        int? episodeId = ddlEpisodeID.SelectedValue == "" ? (int?)null : int.Parse(ddlEpisodeID.SelectedValue);
        string videoQuality = txtVideoQuality.Text.Trim();
        string filePath = txtFilePath.Text.Trim();
        long fileSize = string.IsNullOrEmpty(txtFileSize.Text.Trim()) ? 0 : long.Parse(txtFileSize.Text.Trim());

        if (!string.IsNullOrEmpty(videoQuality) && !string.IsNullOrEmpty(filePath))
        {
            videoFilesManager.AddVideoFile(movieId, seriesId, episodeId, videoQuality, filePath, fileSize);
            lblMessage.Text = "Video file added successfully!";
            BindVideoFilesGrid();
        }
        else
        {
            lblMessage.Text = "Video quality and file path are required.";
        }
    }

    private void BindVideoFilesGrid(string filterVideoQuality = null)
    {
        DataTable dtVideoFiles;
        if (string.IsNullOrEmpty(filterVideoQuality))
        {
            dtVideoFiles = videoFilesManager.GetAllVideoFiles();
        }
        else
        {
            dtVideoFiles = videoFilesManager.FilterVideoFilesByQuality(filterVideoQuality);
        }

        gvVideoFiles.DataSource = dtVideoFiles;
        gvVideoFiles.DataBind();
    }

    private void PopulateDropdownLists()
    {
        // Populate MovieID dropdown
        DataTable dtMovies = videoFilesManager.GetAllMovies();
        ddlMovieID.Items.Clear();
        ddlMovieID.Items.Add(new ListItem("Select Movie", ""));
        foreach (DataRow row in dtMovies.Rows)
        {
            ddlMovieID.Items.Add(new ListItem(row["Title"].ToString(), row["MovieID"].ToString()));
        }

        // Populate SeriesID dropdown
        DataTable dtSeries = videoFilesManager.GetAllSeries();
        ddlSeriesID.Items.Clear();
        ddlSeriesID.Items.Add(new ListItem("Select Series", ""));
        foreach (DataRow row in dtSeries.Rows)
        {
            ddlSeriesID.Items.Add(new ListItem(row["Title"].ToString(), row["SeriesID"].ToString()));
        }

        // Populate EpisodeID dropdown
        DataTable dtEpisodes = videoFilesManager.GetAllEpisodes();
        ddlEpisodeID.Items.Clear();
        ddlEpisodeID.Items.Add(new ListItem("Select Episode", ""));
        foreach (DataRow row in dtEpisodes.Rows)
        {
            ddlEpisodeID.Items.Add(new ListItem(row["Title"].ToString(), row["EpisodeID"].ToString()));
        }
    }

    private void PopulateVideoQualityFilter()
    {
        DataTable dtVideoFiles = videoFilesManager.GetAllVideoFiles();
        ddlFilterVideoQuality.Items.Clear();
        ddlFilterVideoQuality.Items.Add(new ListItem("Select Quality", ""));

        foreach (DataRow row in dtVideoFiles.Rows)
        {
            string quality = row["VideoQuality"].ToString();
            if (!ddlFilterVideoQuality.Items.Cast<ListItem>().Any(item => item.Value == quality))
            {
                ddlFilterVideoQuality.Items.Add(new ListItem(quality, quality));
            }
        }
    }

    protected void gvVideoFiles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvVideoFiles.EditIndex = e.NewEditIndex;
        BindVideoFilesGrid();
    }

    protected void gvVideoFiles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int videoFileId = Convert.ToInt32(gvVideoFiles.DataKeys[e.RowIndex].Value.ToString());
        int? movieId = string.IsNullOrEmpty(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[1].Controls[0]).Text) ? (int?)null : int.Parse(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
        int? seriesId = string.IsNullOrEmpty(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[2].Controls[0]).Text) ? (int?)null : int.Parse(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
        int? episodeId = string.IsNullOrEmpty(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[3].Controls[0]).Text) ? (int?)null : int.Parse(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        string videoQuality = ((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
        string filePath = ((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
        long fileSize = string.IsNullOrEmpty(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[6].Controls[0]).Text) ? 0 : long.Parse(((TextBox)gvVideoFiles.Rows[e.RowIndex].Cells[6].Controls[0]).Text);

        videoFilesManager.UpdateVideoFile(videoFileId, movieId, seriesId, episodeId, videoQuality, filePath, fileSize);
        gvVideoFiles.EditIndex = -1;
        BindVideoFilesGrid();
        lblMessage.Text = "Video file updated successfully!";
    }

    protected void gvVideoFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int videoFileId = Convert.ToInt32(gvVideoFiles.DataKeys[e.RowIndex].Value.ToString());
        videoFilesManager.DeleteVideoFile(videoFileId);
        BindVideoFilesGrid();
        lblMessage.Text = "Video file deleted successfully!";
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindVideoFilesGrid(ddlFilterVideoQuality.SelectedValue);
    }
}
