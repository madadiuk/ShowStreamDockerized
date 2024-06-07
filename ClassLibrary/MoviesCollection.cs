using System.Collections.Generic;

public class MoviesCollection
{
    public List<Movie> MoviesList { get; private set; }

    public MoviesCollection()
    {
        MoviesList = new List<Movie>();
        LoadMovies();
    }

    private void LoadMovies()
    {
        MoviesManager manager = new MoviesManager();
        MoviesList = manager.GetAllMovies();
    }

    public void AddMovie(Movie movie)
    {
        MoviesManager manager = new MoviesManager();
        string result = manager.AddMovie(movie.Title, movie.Description, movie.GenreID, movie.Director, movie.ReleaseDate, movie.Duration);
        if (string.IsNullOrEmpty(result))
        {
            LoadMovies(); // Refresh the list
        }
        else
        {
            // Handle error (e.g., log it, throw an exception, etc.)
        }
    }

    public void UpdateMovie(Movie movie)
    {
        MoviesManager manager = new MoviesManager();
        string result = manager.UpdateMovie(movie.MovieID, movie.Title, movie.Description, movie.GenreID, movie.Director, movie.ReleaseDate, movie.Duration);
        if (string.IsNullOrEmpty(result))
        {
            LoadMovies(); // Refresh the list
        }
        else
        {
            // Handle error (e.g., log it, throw an exception, etc.)
        }
    }

    public bool TryDeleteMovie(int movieID)
    {
        MoviesManager manager = new MoviesManager();
        if (manager.CanDeleteMovie(movieID))
        {
            string result = manager.DeleteMovie(movieID);
            if (string.IsNullOrEmpty(result))
            {
                LoadMovies(); // Refresh the list
                return true;
            }
            else
            {
                // Handle error (e.g., log it, throw an exception, etc.)
                return false;
            }
        }
        return false;
    }
}
