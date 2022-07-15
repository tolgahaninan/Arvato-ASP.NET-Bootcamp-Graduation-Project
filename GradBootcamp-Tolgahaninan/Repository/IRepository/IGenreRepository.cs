using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository.IRepository
{
    public interface IGenreRepository // Interface For Genre Repository
    {
        bool AddGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool GenreExists(int genreId);
        Genre GetGenre(int genreId);
        ICollection<Genre> ListGenres();

        bool Save();
    }
}
