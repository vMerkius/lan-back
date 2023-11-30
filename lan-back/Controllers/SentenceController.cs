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
    public class SentenceController : Controller
    {
        private readonly ISentenceRepository _sentenceRepository;
        private readonly IMapper _mapper;

        public SentenceController(ISentenceRepository sentenceRepository, IMapper mapper)
        {
            _sentenceRepository = sentenceRepository;
            _mapper = mapper;
        }
        [HttpGet("{sentenceId}")]
        [ProducesResponseType(200, Type = typeof(Sentence))]
        [ProducesResponseType(400)]
        public IActionResult GetSentence(int sentenceId)
        {
            if (!_sentenceRepository.SentenceExists(sentenceId))
                return NotFound();

            var sentence = _mapper.Map<SentenceDto>(_sentenceRepository.GetSentence(sentenceId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sentence);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sentence>))]
        public IActionResult getSentences()
        {
            var sentences = _mapper.Map<List<SentenceDto>>(_sentenceRepository.GetSentences());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sentences);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSentence([FromBody] SentenceDto sentenceCreate)
        {
            if (sentenceCreate == null)
                return BadRequest(ModelState);
            var sentence = _sentenceRepository.GetSentences()
                .Where(s => s.Content.Trim().ToUpper() == sentenceCreate.Content.Trim().ToUpper())
                .FirstOrDefault();
            if (sentence != null)
            {
                ModelState.AddModelError("", "Sentence already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sentenceMap = _mapper.Map<Sentence>(sentenceCreate);

            if (!_sentenceRepository.CreateSentence(sentenceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{sentenceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateSentence(int sentenceId, [FromBody] SentenceDto updatedSentence)
        {
            if (updatedSentence == null)
            {
                return BadRequest(ModelState);
            }
            if (sentenceId != updatedSentence.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_sentenceRepository.SentenceExists(sentenceId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var sentenceMap = _mapper.Map<Sentence>(updatedSentence);
            if (!_sentenceRepository.UpdateSentence(sentenceMap))
            {
                ModelState.AddModelError("", "Something went wrong updating sentence");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{sentenceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSentence(int sentenceId)
        {
            if (!_sentenceRepository.SentenceExists(sentenceId))
            {
                return NotFound();
            }
            var sentenceToDelete = _sentenceRepository.GetSentence(sentenceId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_sentenceRepository.DeleteSentence(sentenceToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting sentence");
            }
            return NoContent();

        }

        [HttpPost("multiple")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSentences([FromBody] List<SentenceDto> sentencesCreate)
        {
            if (sentencesCreate == null || !sentencesCreate.Any())
                return BadRequest(ModelState);

            var existingSentences = _sentenceRepository.GetSentences().ToList();
            List<Sentence> sentencesToAdd = new List<Sentence>();

            foreach (var sentenceCreate in sentencesCreate)
            {
                bool sentenceExists = existingSentences.Any(s => s.Content.Trim().ToUpper() == sentenceCreate.Content.Trim().ToUpper());

                if (sentenceExists)
                {
                    ModelState.AddModelError("", $"Sentence '{sentenceCreate.Content}' already exists");
                    continue;
                }

                if (ModelState.IsValid)
                {
                    var mappedSentence = _mapper.Map<Sentence>(sentenceCreate);
                    sentencesToAdd.Add(mappedSentence);
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sentenceRepository.CreateSentences(sentencesToAdd))
            {
                ModelState.AddModelError("", "Something went wrong while saving the sentences");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
