using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Models;
using GradBootcamp_Tolgahaninan.Repository.IRepository;

namespace GradBootcamp_Tolgahaninan.Repository
{
    public class GenreRepository : IGenreRepository
    {

        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db) // Dependency injection for ApplicationDbContext
        {
            _db = db;
        }
        public bool AddGenre(Genre genre) // To Add New Genre Record to database
        {
            _db.genres.Add(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre) // To Delete Already exists genre record from database
        {
            _db.genres.Remove(genre);
            return Save();
        }
        public bool UpdateGenre(Genre genre) // To Update Already exists genre record from database
        {
            _db.genres.Update(genre);
            return Save();
        }
        public Genre GetGenre(int genreId) // To Get genre Record which is specified by genre Id
        {
            return _db.genres.FirstOrDefault(data => data.Id == genreId);
        }

        public ICollection<Genre> ListGenres()  // To Get all genre Records from database
        {
            return _db.genres.OrderBy(data => data.Id).ToList();
        }

        public bool Save() // To store changes into database
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool GenreExists(int genreId) // To check if The genre exists in database
        {
            return _db.genres.Any(data => data.Id == genreId);
        }
    }
}
