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
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public User GetUserAge(int id)
        {
            return _context.Users.Where(u => u.Id== id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();

        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }
        public bool JoinCourse(int userId, int courseId)
        {
            var userEntity = _context.Users.Where(u=> u.Id == userId).FirstOrDefault();
            var courseEntity = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (userEntity == null || courseEntity == null)
            {
                return false;
            }
            var userCourse = new UserCourse
            {
                User = userEntity,
                Course = courseEntity,
            };
            _context.Add(userCourse);
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

        public bool Login(string email, string password)
        {
            var user = _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
 
        }

        public ICollection<Course> GetUserCourses(int id)
        {
            return _context.UserCourses
                    .Where(uc => uc.UserId == id)
                    .Select(uc => uc.Course)
                    .ToList();
        }
        public ICollection<Course> GetUserNonParticipatingCourses(int userId)
        {
            var userCourses = _context.UserCourses
                                      .Where(uc => uc.UserId == userId)
                                      .Select(uc => uc.CourseId)
                                      .ToList();

            var nonParticipatingCourses = _context.Courses
                                                  .Where(c => !userCourses.Contains(c.Id))
                                                  .ToList();

            return nonParticipatingCourses;
        }

        public int GetMen()
        {
            return _context.Users.Where(u => u.Gender == "M").Count();
        }

        public int GetWomen()
        {
            return _context.Users.Where(u => u.Gender == "W").Count();
        }

        public double GetAverageAge()
        {
            var today = DateTime.Today;

            var averageAge = _context.Users
                                     .Select(u => today.Year - u.DateOfBirth.Year -
                                                  ((today.Month < u.DateOfBirth.Month ||
                                                   (today.Month == u.DateOfBirth.Month && today.Day < u.DateOfBirth.Day)) ? 1 : 0))
                                     .Average();

            return averageAge;
        }
        public object GetCountriesInfo()
        {
            return _context.Users
                            .GroupBy(u => u.Country)
                            .Select(group => new
                            {
                                Country = group.Key,
                                Count = group.Count()
                            })
                            .ToList();
        }
        public string DetermineAgeGroup(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year - ((today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day)) ? 1 : 0);

            if (age >= 13 && age <= 18) return "13-18";
            if (age >= 19 && age <= 26) return "19-26";
            if (age >= 27 && age <= 40) return "27-40";
            if (age >= 41 && age <= 60) return "41-60";
            if (age >= 61 && age <= 100) return "61-100";
            return "Out of range";
        }
        public object GetUsersByAgeGroup()
        {
            var today = DateTime.Today;

            var ageGroups = new Dictionary<string, int>
    {
        { "13-18", 0 },
        { "19-26", 0 },
        { "27-40", 0 },
        { "41-60", 0 },
        { "61-100", 0 },
        { "Other", 0 }
    };

            var usersByAge = _context.Users
                .Select(u => new
                {
                    DateOfBirth = u.DateOfBirth,
                    Age = today.Year - u.DateOfBirth.Year - ((today.Month < u.DateOfBirth.Month || (today.Month == u.DateOfBirth.Month && today.Day < u.DateOfBirth.Day)) ? 1 : 0)
                })
                .GroupBy(u => u.Age < 19 ? "13-18" :
                              u.Age < 27 ? "19-26" :
                              u.Age < 41 ? "27-40" :
                              u.Age < 61 ? "41-60" :
                              u.Age <= 100 ? "61-100" : "Other")
                .ToList();

            foreach (var group in usersByAge)
            {
                ageGroups[group.Key] = group.Count();
            }

            return ageGroups.Select(group => new
            {
                AgeGroup = group.Key,
                Count = group.Value
            }).ToList();
        }

    }
}

