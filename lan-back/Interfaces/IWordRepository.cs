using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IWordRepository
    {
        Word GetWord(int id);
        ICollection<Word> GetWords();
        bool WordExists(int id);
        bool CreateWord(Word word);
        bool CreateWords(List<Word> words);
        bool UpdateWord(Word word);
        bool DeleteWord(Word word);
        bool Save();
    }
}
