using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Repository.IRepository;
using System.Linq;

namespace GradBootcamp_Tolgahaninan.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db) // Dependency injection for ApplicationDbContext
        {
            _db = db;
        }
        public bool AddMovie(Movie movie) // To Add New Movie Record to database
        {
           _db.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie) // To Delete Already exists movie record from database
        {
            _db.Movies.Remove(movie);
            return Save();
        }
        public bool UpdateMovie(Movie movie) // To Update Already exists movie record from database
        {
            _db.Movies.Update(movie);
            return Save();
        }
        public Movie GetMovieDetail(int movieId) // To Get Movie Record which is specified by movie Id
        {
            return _db.Movies.FirstOrDefault(data => data.Id == movieId);
        }

       public ICollection<Movie> GetMovieListByGenreId(int genreId) // To Get Movie Record which is specified by genre Id
        {
          //   var x = _db.Movies.Where(data => data.Genres.Id == genreId).ToList();
             return null ;  
        }
   
        public ICollection<Movie> GetMovieListByReleaseDate(string releaseDate) // To Get Movie Records which is specified by release date
        {
        return _db.Movies.Where(data => data.ReleaseDate == releaseDate).ToList();
         
        }
        public ICollection<Movie> GetMovieListByRateFilter(decimal rateFilter) // To Get Movie Records which is specified by rate filter
        {
            return _db.Movies.Where(data => data.VoteAverage == rateFilter).ToList();
        }

        public bool MovieExists(int movieId) // To check if The Movie exists in database
        {
            return _db.Movies.Any(data => data.Id == movieId);
        }

        public bool Save() // To store changes into database
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

     
        public ICollection<Movie> SearchMovie(string? title, decimal? rate, string? year) // To Get Movie Records which is specified by title , rate , year
        {
            //Each paremeter works seperately so user can specify records with in so many combinations
            ICollection<Movie> temp = _db.Movies.ToList();
            if (title != null)
            {
                temp = temp.Where(data => data.Title == title).ToList();
            }
            if (rate != null)
            {
                temp =temp.Where(data => data.VoteAverage == rate).ToList();
            }
            if (year != null)
            {
                temp =temp.Where(data => data.ReleaseDate == year).ToList();
            }
            return temp;
        }
    }
}
