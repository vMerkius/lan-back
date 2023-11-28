using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;

        public QuestionRepository(DataContext context)
        {
            _context = context;
        }
 

        public Question GetQuestion(int id)
        {
            return _context.Questions.Where(q => q.Id == id).FirstOrDefault();
        }

        public ICollection<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }
   
        public bool QuestionExists(int id)
        {
            return _context.Questions.Any(a=>a.Id==id);
        }
        public ICollection<Answer> GetAnswersFromQuestion(int questionId)
        {
            return _context.Answers.Where(q => q.Question.Id == questionId).ToList();
        }

        public bool CreateQuestion(Question question)
        {
            _context.Add(question);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuestion(Question question)
        {
            _context.Update(question);
            return Save();
            
        }

        public bool DeleteQuestion(Question question)
        {
            _context.Remove(question);
            return Save();
        }

        public bool CheckAnswer(int questionId,int answer)
        {
            var question = _context.Questions.Where(q => q.Id == questionId).FirstOrDefault();
            if (question.CorrectAnswer == answer)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public ICollection<Question> GetRandomQuestions(int moduleId, int count = 10)
        {
            return _context.Questions
                   .Where(q => q.QuizId == moduleId)
                   .OrderBy(f => Guid.NewGuid())
                   .Take(count)
                   .ToList();
        }
    }
}
