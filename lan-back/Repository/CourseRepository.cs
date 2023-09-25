using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }
        public Course GetCourse(int id)
        {
            return _context.Courses.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }
        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c=> c.Id == id);
        }

        public ICollection<Module> GetModulesFromCourse(int id)
        {
            return _context.Modules.Where(m => m.Course.Id == id).ToList();
        }

        public ICollection<Quiz> GetQuizzesFromCourse(int id)
        {
            return _context.Quizzes.Where(q => q.Course.Id == id).ToList();
        }

        public bool CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCourse(Course course)
        {
            _context.Update(course);
            return Save();
        }

        public bool DeleteCourse(Course course)
        {
            _context.Remove(course);
            return Save();
        }
    }
}
