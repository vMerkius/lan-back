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
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        [HttpGet("{questionId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetQuestion(int questionId)
        {
            if (!_questionRepository.QuestionExists(questionId))
                return NotFound();

            var question = _mapper.Map<QuestionDto>(_questionRepository.GetQuestion(questionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(question);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
        public IActionResult getQuestions()
        {
            var questions = _mapper.Map<List<QuestionDto>>(_questionRepository.GetQuestions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(questions);
        }
        
        [HttpGet("all/answers/{questionId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetAnswersFromQuestion(int questionId)
        {
            if (!_questionRepository.QuestionExists(questionId))
                return NotFound();

            var answers = _mapper.Map<List<AnswerDto>>(_questionRepository.GetAnswersFromQuestion(questionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(answers);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuestion([FromBody] QuestionDto questionCreate)
        {
            if (questionCreate == null)
                return BadRequest(ModelState);
            var question = _questionRepository.GetQuestions()
                 .Where(q => q.Description.Trim().ToUpper() == questionCreate.Description.Trim().ToUpper())
                 .FirstOrDefault();
             if (question != null)
             {
                 ModelState.AddModelError("", "Question already exists");
                 return StatusCode(422, ModelState);
             }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionMap = _mapper.Map<Question>(questionCreate);

            if (!_questionRepository.CreateQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction(nameof(CreateQuestion), new { id = questionMap.Id }, questionMap);

        }
        [HttpPut("{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateQuestion(int questionId, [FromBody] QuestionDto updatedQuestion)
        {
            if (updatedQuestion == null)
            {
                return BadRequest(ModelState);
            }
            if (questionId != updatedQuestion.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_questionRepository.QuestionExists(questionId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var questionMap = _mapper.Map<Question>(updatedQuestion);
            if (!_questionRepository.UpdateQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating question");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteQuestion(int questionId)
        {
            if (!_questionRepository.QuestionExists(questionId))
            {
                return NotFound();
            }
            var questionToDelete = _questionRepository.GetQuestion(questionId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_questionRepository.DeleteQuestion(questionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting question");
            }
            return NoContent();

        }
        [HttpPut("{questionId}/{answer}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CheckAnswer(int questionId, int answer)
        {
            var check = _questionRepository.CheckAnswer(questionId, answer);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (check)
            {
                return (StatusCode(200, ModelState));
            }
            else
            {
                return NoContent();
            }

        }

    }
}
