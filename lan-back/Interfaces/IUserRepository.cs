using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string name);
        User GetUserByEmail(string email);
        ICollection<User> GetUsers();
        bool UserExists(int id);
        bool CreateUser(int courseId, User user);
        bool UpdateUser(int courdeId,User user);
        bool DeleteUser(User user);
        bool Save();

    }
}
