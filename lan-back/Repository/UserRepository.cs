using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;
using System.Security.Cryptography;

namespace lan_back.Repository
{
    public class UserRepository : IUserRepository

    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string name)
        {
            return _context.Users.Where(u => u.Name == name).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u=>u.Email==email).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u=>u.Id).ToList();

        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
        public bool CreateUser(int courseId, User user)
        {
            var userCourseEntity = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            var userCourse = new UserCourse
            {
                User = user,
                Course = userCourseEntity,
            };
            _context.Add(userCourse);
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(int courdeId, User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}
