using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface ISentenceRepository
    {
        Sentence GetSentence(int id);
        ICollection<Sentence> GetSentences();
        bool SentenceExists(int id);
        bool CreateSentence(Sentence sentence);
        bool CreateSentences(List<Sentence> sentences);
        bool UpdateSentence(Sentence sentence);
        bool DeleteSentence(Sentence sentence);
        ICollection<Sentence> GetRandomSentences(int moduleId, int count);
        bool Save();
    }
}
