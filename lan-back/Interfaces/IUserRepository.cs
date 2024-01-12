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
        ICollection<Course> GetUserNonParticipatingCourses(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool JoinCourse(int userId, int courseId);
        bool UpdateUser(int courdeId,User user);
        bool UpdateUserOnly(User user);
        bool DeleteUser(User user);
        bool Login(string email, string password);
        int GetMen();
        int GetWomen();
        double GetAverageAge();
        object GetCountriesInfo();
        string DetermineAgeGroup(DateTime dateOfBirth);
        object GetUsersByAgeGroup();
        bool Save();
        int GetProgress(int courseId, int userId);
        bool UpdateProgress(int userId, int courseId);
        bool ChangePassword(int userId, string newPassword, string oldPassword);

    }
}
