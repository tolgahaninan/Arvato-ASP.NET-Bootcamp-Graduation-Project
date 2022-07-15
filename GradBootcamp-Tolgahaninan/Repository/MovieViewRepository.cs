using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Repository.IRepository;

namespace GradBootcamp_Tolgahaninan.Repository
{
    public class MovieViewRepository : IMovieViewRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieViewRepository(ApplicationDbContext db) // Dependency injection for ApplicationDbContext
        {
            _db = db;
        }

        public bool AddMovieView(MovieView movieView) // To Add New MovieView Record to database
        {
            _db.movieViews.Add(movieView);
            return Save();
        }

        public MovieView GetMovieViewDetail(int movieId) // To Get MovieView Record which is specified by movie Id
        {
            return _db.movieViews.FirstOrDefault(data => data.movie.Id == movieId);
        }

        public ICollection<MovieView> GetMovieViewList() // To Get MovieView List from database
        {
            return _db.movieViews.OrderBy(data => data.movie.Id).ToList();
        }

        public bool IncrementMovieView(MovieView movieView) // To Increment MovieView Click Counter Attribute
        {
            _db.movieViews.Update(movieView);
            return Save();
        }

        public bool MovieViewExists(int movieId) // To check if The MovieView exists in database
        {
            return _db.movieViews.Any(data => data.movie.Id == movieId);
        }

        public bool Save() // To store changes into database
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}
