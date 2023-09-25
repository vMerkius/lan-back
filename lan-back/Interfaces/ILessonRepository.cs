using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface ILessonRepository
    {
        Lesson GetLesson(int id);
        ICollection<Lesson> GetLessons();
        bool LessonExists(int id);
        ICollection<Subject> GetSubjectsFromLesson(int id);
        bool CreateLesson(Lesson lesson);
        bool UpdateLesson(Lesson lesson);
        bool DeleteLesson(Lesson lesson);
        bool Save();

    }
}
