using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourse(int id);
        ICollection<Course> GetCourses();
        bool CourseExists(int id);
        ICollection<Module> GetModulesFromCourse(int id);
        ICollection<Quiz> GetQuizzesFromCourse(int id);
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(Course course);
        bool Save();

    }
}
