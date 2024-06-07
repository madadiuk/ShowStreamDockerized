using System;
using System.Collections.Generic;
using System.Data;

public class GenresCollection
{
    public List<Genre> GenresList { get; private set; }

    public GenresCollection()
    {
        GenresList = new List<Genre>();
        LoadGenres();
    }

    private void LoadGenres()
    {
        GenresManager manager = new GenresManager();
        List<Genre> genres = manager.GetAllGenres(); // Ensure this returns List<Genre>
        GenresList.Clear();
        GenresList.AddRange(genres);
    }

    public void AddGenre(Genre genre)
    {
        GenresManager manager = new GenresManager();
        manager.AddGenre(genre.Name, genre.Description);
        LoadGenres(); // Refresh the list
    }

    public void UpdateGenre(Genre genre)
    {
        GenresManager manager = new GenresManager();
        manager.UpdateGenre(genre.GenreID, genre.Name, genre.Description);
        LoadGenres(); // Refresh the list
    }

    public bool CanDeleteGenre(int genreID)
    {
        GenresManager manager = new GenresManager();
        return manager.CanDeleteGenre(genreID);
    }

    public void DeleteGenre(int genreID)
    {
        GenresManager manager = new GenresManager();
        manager.DeleteGenre(genreID);
        LoadGenres(); // Refresh the list
    }
}
