using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string name);
        User GetUserByEmail(string email);
        User GetUserAge(int id);
        ICollection<User> GetUsers();
        ICollection<Course> GetUserCourses(int id);
        bool UserExists(int id);
        bool CreateUser(int courseId, User user);
        bool UpdateUser(int courdeId,User user);
        bool DeleteUser(User user);
        int GetMen();
        int GetWomen();
        double GetAverageAge();
        object GetCountriesInfo();
        string DetermineAgeGroup(DateTime dateOfBirth);
        object GetUsersByAgeGroup();
        bool Save();

    }
}
