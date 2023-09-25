using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly DataContext _context;

        public LessonRepository(DataContext context)
        {
            _context = context;
        }
        public Lesson GetLesson(int id)
        {
            return _context.Lessons.Where(l=>l.Id==id).FirstOrDefault();
        }

        public ICollection<Lesson> GetLessons()
        {
            return _context.Lessons.ToList();
        }

  
        public bool LessonExists(int id)
        {
            return _context.Lessons.Any(l=>l.Id==id);
        }
        public ICollection<Subject> GetSubjectsFromLesson(int id)
        {
            return _context.Subjects.Where(s => s.Lesson.Id == id).ToList();
        }

        public bool CreateLesson(Lesson lesson)
        {
            _context.Add(lesson);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLesson(Lesson lesson)
        {
            _context.Update(lesson);
            return Save();
        }

        public bool DeleteLesson(Lesson lesson)
        {
            _context.Remove(lesson);
            return Save();
        }
    }
}
