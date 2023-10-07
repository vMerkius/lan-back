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
    public class WordController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public WordController(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }
        [HttpGet("{wordId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetWord(int wordId)
        {
            if (!_wordRepository.WordExists(wordId))
                return NotFound();

            var word = _mapper.Map<WordDto>(_wordRepository.GetWord(wordId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(word);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult getWords()
        {
            var words = _mapper.Map<List<WordDto>>(_wordRepository.GetWords());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(words);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWord([FromBody] WordDto wordCreate)
        {
            if (wordCreate == null)
                return BadRequest(ModelState);
            var word = _wordRepository.GetWords()
                .Where(a => a.OriginalWord.Trim().ToUpper() == wordCreate.OriginalWord.Trim().ToUpper())
                .FirstOrDefault();
            if (word != null)
            {
                ModelState.AddModelError("", "Word already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wordMap = _mapper.Map<Word>(wordCreate);

            if (!_wordRepository.CreateWord(wordMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{wordId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateWord(int wordId, [FromBody] WordDto updatedWord)
        {
            if (updatedWord == null)
            {
                return BadRequest(ModelState);
            }
            if (wordId != updatedWord.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_wordRepository.WordExists(wordId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var wordMap = _mapper.Map<Word>(updatedWord);
            if (!_wordRepository.UpdateWord(wordMap))
            {
                ModelState.AddModelError("", "Something went wrong updating word");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{wordId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWord(int wordId)
        {
            if (!_wordRepository.WordExists(wordId))
            {
                return NotFound();
            }
            var wordToDelete = _wordRepository.GetWord(wordId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_wordRepository.DeleteWord(wordToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting word");
            }
            return NoContent();

        }

        [HttpPost("multiple")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWords([FromBody] List<WordDto> wordsCreate)
        {
            if (wordsCreate == null || !wordsCreate.Any())
                return BadRequest(ModelState);

            var existingWords = _wordRepository.GetWords().ToList();
            List<Word> wordsToAdd = new List<Word>();

            foreach (var wordCreate in wordsCreate)
            {
                bool wordExists = existingWords.Any(w => w.OriginalWord.Trim().ToUpper() == wordCreate.OriginalWord.Trim().ToUpper());

                if (wordExists)
                {
                    ModelState.AddModelError("", $"Word '{wordCreate.OriginalWord}' already exists");
                    continue;
                }

                if (ModelState.IsValid)
                {
                    var mappedWord = _mapper.Map<Word>(wordCreate);
                    wordsToAdd.Add(mappedWord);
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_wordRepository.CreateWords(wordsToAdd))
            {
                ModelState.AddModelError("", "Something went wrong while saving the words");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
