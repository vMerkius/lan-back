using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class WordRepository : IWordRepository
    {
        private readonly DataContext _context;

        public WordRepository(DataContext context)
        {
            _context = context;
        }
        public Word GetWord(int id)
        {
            return _context.Words.Where(w => w.Id == id).FirstOrDefault();
        }

        public ICollection<Word> GetWords()
        {
            return _context.Words.ToList();
        }
        public bool WordExists(int id)
        {
            return _context.Words.Any(w => w.Id == id);
        }
        public bool CreateWord(Word word)
        {
            _context.Add(word);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWord(Word word)
        {
            _context.Update(word);
            return Save();
        }

        public bool DeleteWord(Word word)
        {
            _context.Remove(word);
            return Save();
        }

        public bool CreateWords(List<Word> words)
        {
            foreach (var item in words)
            {
                _context.Add(item);
            }
            return Save();
        }
    }
}
