using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenresManagementDataEntry : Page
{
    private GenresManager genresManager;

    protected void Page_Init(object sender, EventArgs e)
    {
        // Initialize genresManager here to ensure it's available for all methods
        genresManager = new GenresManager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGenresGrid();
        }
    }

    protected void btnAddGenre_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string description = txtDescription.Text.Trim();

        if (!string.IsNullOrEmpty(name))
        {
            genresManager.AddGenre(name, description);
            lblMessage.Text = "Genre added successfully!";
            BindGenresGrid();
        }
        else
        {
            lblMessage.Text = "Name is required.";
        }
    }

    private void BindGenresGrid()
    {
        DataTable dtGenres = genresManager.GetAllGenres();
        gvGenres.DataSource = dtGenres;
        gvGenres.DataBind();
    }

    protected void gvGenres_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvGenres.EditIndex = e.NewEditIndex;
        BindGenresGrid();
    }

    protected void gvGenres_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int genreId = Convert.ToInt32(gvGenres.DataKeys[e.RowIndex].Value.ToString());
        string name = ((TextBox)gvGenres.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string description = ((TextBox)gvGenres.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

        genresManager.UpdateGenre(genreId, name, description);
        gvGenres.EditIndex = -1;
        BindGenresGrid();
        lblMessage.Text = "Genre updated successfully!";
    }

    protected void gvGenres_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int genreId = Convert.ToInt32(gvGenres.DataKeys[e.RowIndex].Value.ToString());
        genresManager.DeleteGenre(genreId);
        BindGenresGrid();
        lblMessage.Text = "Genre deleted successfully!";
    }
}
