using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Repository.IRepository
{
    public interface IUserRepository // Interface For User Repository
    {

        bool SignIn(string username , string password);
        bool SignOut(string username, string password);
        bool UserExists(int userId);
        bool Save();
    }
}
