using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IFlashcardRepository
    {
        Flashcard GetFlashcard(int id);
        ICollection<Flashcard> GetFlashcards();
        bool FlashcardExists(int id);
        ICollection<Word> GetWordsFromFlashcard(int id);
        bool CreateFlashcard(Flashcard flashcard);
        bool UpdateFlashcard(Flashcard flashcard);
        bool DeleteFlashcard(Flashcard flashcard);
        bool Save();
    }
}
