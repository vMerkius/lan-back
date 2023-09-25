using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface ISubjectRepository
    {
        Subject GetSubject(int id);

        ICollection<Subject> GetSubjects();
        bool SubjectExists(int id);
        bool CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(Subject subject);
        bool Save();
    }
}
