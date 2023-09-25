using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }
        public Subject GetSubject(int id)
        {
            return _context.Subjects.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public bool SubjectExists(int id)
        {
            return _context.Subjects.Any(s=> s.Id == id);
        }
        public bool CreateSubject(Subject subject)
        {
            _context.Add(subject);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSubject(Subject subject)
        {
            _context.Update(subject);
            return Save();
        }

        public bool DeleteSubject(Subject subject)
        {
            _context.Remove(subject);
            return Save();
        }
    }
}
