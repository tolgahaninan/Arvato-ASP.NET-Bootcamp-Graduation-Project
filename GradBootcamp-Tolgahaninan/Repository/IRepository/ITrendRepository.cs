using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository.IRepository
{
    public interface ITrendRepository // Interface For Trend Repository
    {
        ICollection<MovieView> ListMostViewedMovies();
        ICollection<Movie> ListTopRatedMovies();
    }
}
