using AutoMapper;
using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuizRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Quiz GetQuiz(int id)
        {
            return _context.Quizzes.Where(q => q.Id == id).FirstOrDefault();
        }

        public ICollection<Quiz> GetQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public bool QuizExists(int id)
        {
            return _context.Quizzes.Any(q=> q.Id == id);
        }
        public ICollection<Question> GetQuestionsFromQuiz(int id)
        {
            return _context.Questions.Where(q => q.Quiz.Id == id).ToList();
        }

        public bool CreateQuiz(Quiz quiz)
        {
            _context.Add(quiz);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuiz(Quiz quiz)
        {
            _context.Update(quiz);
            return Save();
        }

        public bool DeleteQuiz(Quiz quiz)
        {
            _context.Remove(quiz);
            return Save();
        }
    }
}
