using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository.IRepository
{
    public interface IMovieRepository// Interface For Movie Repository
    {

        bool AddMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool MovieExists(int movieId);
        Movie GetMovieDetail(int movieId);
        ICollection<Movie> GetMovieListByGenreId(int genreId);
        ICollection<Movie> GetMovieListByReleaseDate(string releaseDate);
        ICollection<Movie> GetMovieListByRateFilter(decimal rateFilter);
        ICollection<Movie> SearchMovie(string? title, decimal? rate, string? year);

        bool Save();

    }
}
