using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Repository.IRepository;

namespace GradBootcamp_Tolgahaninan.Repository
{
    public class TrendRepository : ITrendRepository
    {
        private readonly ApplicationDbContext _db;

        public TrendRepository(ApplicationDbContext db) // Dependency injection for ApplicationDbContext
        {
            _db = db;
        }
        public ICollection<MovieView> ListMostViewedMovies()
        {
            return _db.movieViews.OrderByDescending(data => data.ClickCounter).Take(10).ToList(); // To get top 10 most viewed movies in database
        }

        public ICollection<Movie> ListTopRatedMovies()
        {
            return _db.Movies.OrderByDescending(data => data.VoteAverage).Take(10).ToList(); // To get top ten top rated movies in database
        }
    }
}
