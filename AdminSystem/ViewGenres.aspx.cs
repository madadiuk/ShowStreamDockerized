using System;
using System.Collections.Generic;

public partial class ViewGenres : System.Web.UI.Page
{
    private GenresCollection genresCollection;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            genresCollection = new GenresCollection();
            Session["GenresCollection"] = genresCollection;
            LoadGenres();
        }
        else
        {
            genresCollection = Session["GenresCollection"] as GenresCollection;
        }
    }

    private void LoadGenres()
    {
        gvGenres.DataSource = genresCollection.GenresList;
        gvGenres.DataBind();
    }

    protected void btnAddGenre_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenresManagementDataEntry.aspx");
    }

    protected void btnMainMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeamMainMenu.aspx");
    }

    protected void gvGenres_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        gvGenres.EditIndex = e.NewEditIndex;
        LoadGenres();
    }

    protected void gvGenres_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        genresCollection = Session["GenresCollection"] as GenresCollection;

        int genreID = Convert.ToInt32(gvGenres.DataKeys[e.RowIndex].Value);
        string name = ((System.Web.UI.WebControls.TextBox)gvGenres.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string description = ((System.Web.UI.WebControls.TextBox)gvGenres.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

        Genre genre = new Genre { GenreID = genreID, Name = name, Description = description };
        genresCollection.UpdateGenre(genre);

        gvGenres.EditIndex = -1;
        LoadGenres();
        Session["GenresCollection"] = genresCollection; // Update session
    }

    protected void gvGenres_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        gvGenres.EditIndex = -1;
        LoadGenres();
    }

    protected void gvGenres_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        genresCollection = Session["GenresCollection"] as GenresCollection;

        int genreID = Convert.ToInt32(gvGenres.DataKeys[e.RowIndex].Value);

        if (genresCollection.CanDeleteGenre(genreID))
        {
            genresCollection.DeleteGenre(genreID);
            LoadGenres();
            Session["GenresCollection"] = genresCollection; // Update session
        }
        else
        {
            Response.Redirect("DeleteError.aspx");
        }
    }
}
