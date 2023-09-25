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
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        [HttpGet("{answerId}")]
        [ProducesResponseType(200, Type = typeof(Answer))]
        [ProducesResponseType(400)]
        public IActionResult GetAnswer(int answerId)
        {
            if (!_answerRepository.AnswerExists(answerId))
                return NotFound();

            var user = _mapper.Map<AnswerDto>(_answerRepository.GetAnswer(answerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Answer>))]
        public IActionResult getAnswers()
        {
            var answers = _mapper.Map<List<AnswerDto>>(_answerRepository.GetAnswers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(answers);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAnswer([FromBody] AnswerDto answerCreate)
        {
            if (answerCreate == null)
                return BadRequest(ModelState);
            var answer = _answerRepository.GetAnswers()
                .Where(a => a.Name.Trim().ToUpper() == answerCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (answer != null)
            {
                ModelState.AddModelError("", "Answer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var answerMap = _mapper.Map<Answer>(answerCreate);

            if (!_answerRepository.CreateAnswer(answerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{answerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateAnswer(int answerId, [FromBody] AnswerDto updatedAnswer)
        {
            if (updatedAnswer == null)
            {
                return BadRequest(ModelState);
            }
            if (answerId != updatedAnswer.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_answerRepository.AnswerExists(answerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var answerMap = _mapper.Map<Answer>(updatedAnswer);
            if (!_answerRepository.UpdateAnswer(answerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating answer");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{answerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAnswer(int answerId)
        {
            if (!_answerRepository.AnswerExists(answerId))
            {
                return NotFound();
            }
            var answerToDelete = _answerRepository.GetAnswer(answerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_answerRepository.DeleteAnswer(answerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting answer");
            }
            return NoContent();

        }
        [HttpPost("multiple")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAnswers([FromBody] List<AnswerDto> answersCreate)
        {
            if (answersCreate == null || !answersCreate.Any())
                return BadRequest(ModelState);

            var existingAnswers = _answerRepository.GetAnswers().ToList();
            List<Answer> answersToAdd = new List<Answer>();

            foreach (var answerCreate in answersCreate)
            {
                bool answerExists = existingAnswers.Any(a => a.Name.Trim().ToUpper() == answerCreate.Name.Trim().ToUpper());

                if (answerExists)
                {
                    ModelState.AddModelError("", $"Answer '{answerCreate.Name}' already exists");
                    continue;
                }

                if (ModelState.IsValid)
                {
                    var mappedAnswer = _mapper.Map<Answer>(answerCreate);
                    answersToAdd.Add(mappedAnswer);
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_answerRepository.CreateAnswers(answersToAdd))
            {
                ModelState.AddModelError("", "Something went wrong while saving the answers");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
