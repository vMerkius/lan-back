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
    public class FlashcardController : Controller
    {
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IMapper _mapper;

        public FlashcardController(IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }
      

        [HttpGet("{flashcardId}")]
        [ProducesResponseType(200, Type = typeof(Flashcard))]
        [ProducesResponseType(400)]
        public IActionResult GetFlashcard(int flashcardId)
        {
            if (!_flashcardRepository.FlashcardExists(flashcardId))
                return NotFound();

            var flashcard = _mapper.Map<FlashcardDto>(_flashcardRepository.GetFlashcard(flashcardId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flashcard);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Flashcard>))]
        public IActionResult getFlashcards()
        {
            var flashcards = _mapper.Map<List<FlashcardDto>>(_flashcardRepository.GetFlashcards());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flashcards);
        }
        
        [HttpGet("all/words/{flashcardId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Word>))]
        [ProducesResponseType(400)]
        public IActionResult GetWordsFromFlashcard(int flashcardId)
        {
            if (!_flashcardRepository.FlashcardExists(flashcardId))
                return NotFound();

            var words = _mapper.Map<List<WordDto>>(_flashcardRepository.GetWordsFromFlashcard(flashcardId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(words);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFlashcard([FromBody] FlashcardDto flashcardCreate)
        {
            if (flashcardCreate == null)
                return BadRequest(ModelState);
          
             var flashcard = _flashcardRepository.GetFlashcards()
                 .Where(f => f.Name.Trim().ToUpper() == flashcardCreate.Name.Trim().ToUpper())
                 .FirstOrDefault();
             if (flashcard != null)
             {
                 ModelState.AddModelError("", "Flashcard already exists");
                 return StatusCode(422, ModelState);
             }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var flashcardMap = _mapper.Map<Flashcard>(flashcardCreate);

            if (!_flashcardRepository.CreateFlashcard(flashcardMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{flashcardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateFlashcard(int flashcardId, [FromBody] FlashcardDto updatedFlashcard)
        {
            if (updatedFlashcard == null)
            {
                return BadRequest(ModelState);
            }
            if (flashcardId != updatedFlashcard.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_flashcardRepository.FlashcardExists(flashcardId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var flashcardMap = _mapper.Map<Flashcard>(updatedFlashcard);
            if (!_flashcardRepository.UpdateFlashcard(flashcardMap))
            {
                ModelState.AddModelError("", "Something went wrong updating flashcard");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{flashcardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFlashcard(int flashcardId)
        {
            if (!_flashcardRepository.FlashcardExists(flashcardId))
            {
                return NotFound();
            }
            var flashcardToDelete = _flashcardRepository.GetFlashcard(flashcardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_flashcardRepository.DeleteFlashcard(flashcardToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting flashcard");
            }
            return NoContent();

        }
    }
}
