using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IQuizRepository
    {
        Quiz GetQuiz(int id);
        ICollection<Quiz> GetQuizzes();
        bool QuizExists(int id);
        ICollection<Question> GetQuestionsFromQuiz(int id);
        bool CreateQuiz(Quiz quiz);
        bool UpdateQuiz(Quiz quiz);
        bool DeleteQuiz(Quiz quiz);
        bool Save();
    }
}
