using AutoMapper;
using lan_back.Dto;
using lan_back.Interfaces;
using lan_back.Models;
using lan_back.Repository;
using Microsoft.AspNetCore.Mvc;

namespace lan_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizController(IQuizRepository quizRepository, IMapper mapper)
        {
           _quizRepository = quizRepository;
            _mapper = mapper;
        }
        [HttpGet("{quizId}")]
        [ProducesResponseType(200, Type = typeof(Quiz))]
        [ProducesResponseType(400)]
        public IActionResult GetQuiz(int quizId)
        {
            if (!_quizRepository.QuizExists(quizId))
                return NotFound();

            var quiz = _mapper.Map<QuizDto>(_quizRepository.GetQuiz(quizId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quiz);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quiz>))]
        public IActionResult GetQuizzes()
        {
            var quizzes = _mapper.Map<List<QuizDto>>(_quizRepository.GetQuizzes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quizzes);
        }

        [HttpGet("all/questions/{quizId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
        [ProducesResponseType(400)]
        public IActionResult GetQuestionsFromQuiz(int quizId)
        {
            if (!_quizRepository.QuizExists(quizId))
                return NotFound();

            var quizzes = _mapper.Map<List<QuestionDto>>(_quizRepository.GetQuestionsFromQuiz(quizId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quizzes);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuiz([FromBody] QuizDto quizCreate)
        {
            if (quizCreate == null)
                return BadRequest(ModelState);
            var quiz = _quizRepository.GetQuizzes()
                .Where(q => q.Name.Trim().ToUpper() == quizCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (quiz != null)
            {
                ModelState.AddModelError("", "Quiz already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quizMap = _mapper.Map<Quiz>(quizCreate);

            if (!_quizRepository.CreateQuiz(quizMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{quizId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateQuiz(int quizId, [FromBody] QuizDto updatedQuiz)
        {
            if (updatedQuiz == null)
            {
                return BadRequest(ModelState);
            }
            if (quizId != updatedQuiz.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_quizRepository.QuizExists(quizId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var quizMap = _mapper.Map<Quiz>(updatedQuiz);
            if (!_quizRepository.UpdateQuiz(quizMap))
            {
                ModelState.AddModelError("", "Something went wrong updating quiz");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{quizId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteQuiz(int quizId)
        {
            if (!_quizRepository.QuizExists(quizId))
            {
                return NotFound();
            }
            var quizToDelete = _quizRepository.GetQuiz(quizId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_quizRepository.DeleteQuiz(quizToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting quiz");
            }
            return NoContent();

        }
    }
}
