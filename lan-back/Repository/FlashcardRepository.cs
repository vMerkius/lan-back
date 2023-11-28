using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly DataContext _context;

        public FlashcardRepository(DataContext context)
        {
            _context = context;
        }
        public Flashcard GetFlashcard(int id)
        {
            return _context.Flashcards.Where(f => f.Id == id).FirstOrDefault();
        }

        public ICollection<Flashcard> GetFlashcards()
        {
            return _context.Flashcards.ToList();
        }

        public bool FlashcardExists(int id)
        {
            return _context.Flashcards.Any(f => f.Id == id);
        }

        public ICollection<Word> GetWordsFromFlashcard(int flashcardId)
        {
            return _context.Words.Where(w => w.Flashcard.Id == flashcardId).ToList();


        }

        public bool CreateFlashcard(Flashcard flashcard)
        {
            _context.Add(flashcard);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFlashcard(Flashcard flashcard)
        {
            _context.Update(flashcard);
            return Save();
        }

        public bool DeleteFlashcard(Flashcard flashcard)
        {
            _context.Remove(flashcard);
            return Save();
        }

        public ICollection<Flashcard> GetRandomFlashcards(int moduleId, int count = 10)
        {
            return _context.Flashcards
                   .Where(f => f.ModuleId == moduleId)
                   .OrderBy(f => Guid.NewGuid()) 
                   .Take(count) 
                   .ToList();
        }
    }
}
