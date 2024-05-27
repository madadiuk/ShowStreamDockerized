using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EpisodesManagementDataEntry : Page
{
    private EpisodesManager episodesManager;
    private SeriesManager seriesManager;

    protected void Page_Init(object sender, EventArgs e)
    {
        episodesManager = new EpisodesManager();
        seriesManager = new SeriesManager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEpisodesGrid();
        }
    }

    protected void btnAddEpisode_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text.Trim();
        string description = txtDescription.Text.Trim();
        int seriesId = int.Parse(txtSeriesID.Text.Trim());
        int seasonNumber = int.Parse(txtSeasonNumber.Text.Trim());
        int episodeNumber = int.Parse(txtEpisodeNumber.Text.Trim());
        DateTime releaseDate = DateTime.Parse(txtReleaseDate.Text.Trim());

        if (!string.IsNullOrEmpty(title) && seriesManager.SeriesExists(seriesId))
        {
            episodesManager.AddEpisode(seriesId, seasonNumber, episodeNumber, title, description, releaseDate);
            lblMessage.Text = "Episode added successfully!";
            BindEpisodesGrid();
        }
        else
        {
            lblMessage.Text = "Title is required and Series ID must exist.";
        }
    }

    private void BindEpisodesGrid()
    {
        DataTable dtEpisodes = episodesManager.GetAllEpisodes();
        gvEpisodes.DataSource = dtEpisodes;
        gvEpisodes.DataBind();
    }

    protected void gvEpisodes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvEpisodes.EditIndex = e.NewEditIndex;
        BindEpisodesGrid();
    }

    protected void gvEpisodes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int episodeId = Convert.ToInt32(gvEpisodes.DataKeys[e.RowIndex].Value.ToString());
        int seriesId = int.Parse(((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
        int seasonNumber = int.Parse(((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
        int episodeNumber = int.Parse(((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        string title = ((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
        string description = ((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
        DateTime releaseDate = DateTime.Parse(((TextBox)gvEpisodes.Rows[e.RowIndex].Cells[6].Controls[0]).Text);

        episodesManager.UpdateEpisode(episodeId, seriesId, seasonNumber, episodeNumber, title, description, releaseDate);
        gvEpisodes.EditIndex = -1;
        BindEpisodesGrid();
        lblMessage.Text = "Episode updated successfully!";
    }

    protected void gvEpisodes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int episodeId = Convert.ToInt32(gvEpisodes.DataKeys[e.RowIndex].Value.ToString());
        episodesManager.DeleteEpisode(episodeId);
        BindEpisodesGrid();
        lblMessage.Text = "Episode deleted successfully!";
    }

    protected void btnFilterEpisodes_Click(object sender, EventArgs e)
    {
        int? seriesId = string.IsNullOrWhiteSpace(txtFilterSeriesID.Text) ? (int?)null : int.Parse(txtFilterSeriesID.Text);
        int? seasonNumber = string.IsNullOrWhiteSpace(txtFilterSeasonNumber.Text) ? (int?)null : int.Parse(txtFilterSeasonNumber.Text);
        int? episodeNumber = string.IsNullOrWhiteSpace(txtFilterEpisodeNumber.Text) ? (int?)null : int.Parse(txtFilterEpisodeNumber.Text);
        DateTime? releaseDate = string.IsNullOrWhiteSpace(txtFilterReleaseDate.Text) ? (DateTime?)null : DateTime.Parse(txtFilterReleaseDate.Text);

        DataTable filteredEpisodes = episodesManager.FilterEpisodes(seriesId, seasonNumber, episodeNumber, releaseDate);
        gvEpisodes.DataSource = filteredEpisodes;
        gvEpisodes.DataBind();
    }
}
