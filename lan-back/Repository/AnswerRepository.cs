using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DataContext _context;

        public AnswerRepository(DataContext context)
        {
            _context = context;
        }
        public Answer GetAnswer(int id)
        {
            return _context.Answers.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Answer> GetAnswers()
        {
            return _context.Answers.OrderBy(a=> a.Id).ToList();
        }
        public bool AnswerExists(int id)
        {
            return _context.Answers.Any(a => a.Id == id);
        }

        public bool CreateAnswer(Answer answer)
        {
            _context.Add(answer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAnswer(Answer answer)
        {
            _context.Update(answer);
            return Save();
        }

        public bool DeleteAnswer(Answer answer)
        {
            _context.Remove(answer);
            return Save();
        }

        public bool CreateAnswers(List<Answer> answers)
        {
            foreach (var item in answers)
            {
                _context.Add(item);
            }
            return Save();
        }
    }
}
