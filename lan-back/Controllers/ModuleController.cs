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
    public class ModuleController : Controller
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleController(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }
        [HttpGet("{moduleId}")]
        [ProducesResponseType(200, Type = typeof(Module))]
        [ProducesResponseType(400)]
        public IActionResult GetModule(int moduleId)
        {
            if (!_moduleRepository.ModuleExists(moduleId))
                return NotFound();

            var module = _mapper.Map<ModuleDto>(_moduleRepository.GetModule(moduleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(module);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Module>))]
        public IActionResult GetModules()
        {
            var modules = _mapper.Map<List<ModuleDto>>(_moduleRepository.GetModules());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(modules);
        }

        [HttpGet("all/flashcards/{moduleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Flashcard>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlashcardsFromLesson(int moduleId)
        {
            if (!_moduleRepository.ModuleExists(moduleId))
                return NotFound();

            var flashcards = _mapper.Map<List<FlashcardDto>>(_moduleRepository.GetFlashCardsFromModule(moduleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flashcards);
        }
        [HttpGet("all/lessons/{moduleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lesson>))]
        [ProducesResponseType(400)]
        public IActionResult GetLessonFromModule(int moduleId)
        {
            if (!_moduleRepository.ModuleExists(moduleId))
                return NotFound();

            var lessons = _mapper.Map<List<LessonDto>>(_moduleRepository.GetLessonsFromModule(moduleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lessons);
        }
        [HttpGet("all/sentences/{moduleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sentence>))]
        [ProducesResponseType(400)]
        public IActionResult GetSentencesFromModule(int moduleId)
        {
            if (!_moduleRepository.ModuleExists(moduleId))
                return NotFound();

            var sentences = _mapper.Map<List<SentenceDto>>(_moduleRepository.GetSentencesFromModule(moduleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sentences);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateModule([FromBody] ModuleDto moduleCreate)
        {
            if (moduleCreate == null)
                return BadRequest(ModelState);
            var module = _moduleRepository.GetModules()
                 .Where(m => m.Name.Trim().ToUpper() == moduleCreate.Name.Trim().ToUpper())
                 .FirstOrDefault();
            if (module != null)
            {
                ModelState.AddModelError("", "Module already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moduleMap = _mapper.Map<Module>(moduleCreate);

            if (!_moduleRepository.CreateModule(moduleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{moduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateModule(int moduleId, [FromBody] ModuleDto updatedModule)
        {
            if (updatedModule == null)
            {
                return BadRequest(ModelState);
            }
            if (moduleId != updatedModule.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_moduleRepository.ModuleExists(moduleId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var moduleMap = _mapper.Map<Module>(updatedModule);
            if (!_moduleRepository.UpdateModule(moduleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating module");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{moduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteModule(int moduleId)
        {
            if (!_moduleRepository.ModuleExists(moduleId))
            {
                return NotFound();
            }
            var moduleToDelete = _moduleRepository.GetModule(moduleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_moduleRepository.DeleteModule(moduleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting module");
            }
            return NoContent();

        }
    }
}
