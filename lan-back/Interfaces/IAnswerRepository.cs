using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IAnswerRepository
    {
        Answer GetAnswer(int id);
        ICollection<Answer> GetAnswers();
        bool AnswerExists(int id);
        bool CreateAnswer (Answer answer);
        bool CreateAnswers(List<Answer> answers);
        bool UpdateAnswer(Answer answer);
        bool DeleteAnswer(Answer answer);
        bool Save();
    }
}
