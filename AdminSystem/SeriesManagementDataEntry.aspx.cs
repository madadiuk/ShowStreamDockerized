using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SeriesManagementDataEntry : Page
{
    private SeriesManager seriesManager;

    protected void Page_Init(object sender, EventArgs e)
    {
        // Initialize seriesManager here to ensure it's available for all methods
        seriesManager = new SeriesManager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSeriesGrid();
        }
    }

    protected void btnAddSeries_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text.Trim();
        int genreId = int.Parse(txtGenreID.Text.Trim());
        DateTime startYear = DateTime.Parse(txtStartYear.Text.Trim());
        DateTime endYear = DateTime.Parse(txtEndYear.Text.Trim());
        string country = txtCountry.Text.Trim();

        if (!string.IsNullOrEmpty(title))
        {
            seriesManager.AddSeries(title, genreId, startYear, endYear, country);
            lblMessage.Text = "Series added successfully!";
            BindSeriesGrid();
        }
        else
        {
            lblMessage.Text = "Title is required.";
        }
    }

    private void BindSeriesGrid()
    {
        DataTable dtSeries = seriesManager.GetAllSeries();
        gvSeries.DataSource = dtSeries;
        gvSeries.DataBind();
    }

    protected void gvSeries_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSeries.EditIndex = e.NewEditIndex;
        BindSeriesGrid();
    }

    protected void gvSeries_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int seriesId = Convert.ToInt32(gvSeries.DataKeys[e.RowIndex].Value.ToString());
        string title = ((TextBox)gvSeries.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        int genreId = int.Parse(((TextBox)gvSeries.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
        DateTime startYear = DateTime.Parse(((TextBox)gvSeries.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        DateTime endYear = DateTime.Parse(((TextBox)gvSeries.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
        string country = ((TextBox)gvSeries.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

        seriesManager.UpdateSeries(seriesId, title, genreId, startYear, endYear, country);
        gvSeries.EditIndex = -1;
        BindSeriesGrid();
        lblMessage.Text = "Series updated successfully!";
    }

    protected void gvSeries_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int seriesId = Convert.ToInt32(gvSeries.DataKeys[e.RowIndex].Value.ToString());
        seriesManager.DeleteSeries(seriesId);
        BindSeriesGrid();
        lblMessage.Text = "Series deleted successfully!";
    }
}
