using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetQuestion(int id);
        ICollection<Question> GetQuestions();
        bool QuestionExists(int id);
        ICollection<Answer> GetAnswersFromQuestion(int questionId);
        bool CreateQuestion(Question question);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(Question question);
        bool CheckAnswer(int questionId,int answer);
        bool Save();

    }
}
