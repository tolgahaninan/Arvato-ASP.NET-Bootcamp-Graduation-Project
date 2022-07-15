using GradBootcamp_Tolgahaninan.Repository.IRepository;
using GradBootcamp_Tolgahaninan.Data;
using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;


        public UserRepository(ApplicationDbContext db) // Dependency injection for ApplicationDbContext
        {
            _db = db;
        }

  
        public bool UserExists(int userId)
        {
            return _db.users.Any(data => data.Id == userId); // To Check is there any user in database with given Id
        }



        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false; // To save database changes
        }

        public bool SignIn(string username, string password) // To check if there is any user in database with given username and password combination
        {
            var user = _db.users.Where(data => data.Username == username && data.Password == password);
            if (user.Any())
            {return true;}else{ return false;};
        }

        public bool SignOut(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
