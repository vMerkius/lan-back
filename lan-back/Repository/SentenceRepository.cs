using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class SentenceRepository : ISentenceRepository
    {
        private readonly DataContext _context;

        public SentenceRepository(DataContext context)
        {
            _context = context;
        }
        public Sentence GetSentence(int id)
        {
            return _context.Sentences.Where(w => w.Id == id).FirstOrDefault();
        }

        public ICollection<Sentence> GetSentences()
        {
            return _context.Sentences.ToList();
        }
        public bool SentenceExists(int id)
        {
            return _context.Sentences.Any(w => w.Id == id);
        }
        public bool CreateSentence(Sentence sentence)
        {
            _context.Add(sentence);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSentence(Sentence sentence)
        {
            _context.Update(sentence);
            return Save();
        }

        public bool DeleteSentence(Sentence sentence)
        {
            _context.Remove(sentence);
            return Save();
        }

        public bool CreateSentences(List<Sentence> sentences)
        {
            foreach (var item in sentences)
            {
                _context.Add(item);
            }
            return Save();
        }
        public ICollection<Sentence> GetRandomSentences(int moduleId, int count)
        {
            return _context.Sentences
                   .Where(s => s.ModuleId == moduleId)
                   .OrderBy(f => Guid.NewGuid())
                   .Take(count)
                   .ToList();
        }
    }
}
