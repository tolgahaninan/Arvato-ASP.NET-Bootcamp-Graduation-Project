using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository.IRepository
{
    public interface IMovieViewRepository // Interface For MovieView Repository
    {
        bool MovieViewExists(int movieId);

        bool AddMovieView(MovieView movieView);
        MovieView GetMovieViewDetail(int movieId);
        bool IncrementMovieView(MovieView movieView);

        ICollection<MovieView> GetMovieViewList();
        bool Save();
    }
}
