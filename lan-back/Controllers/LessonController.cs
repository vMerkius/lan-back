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
    public class LessonController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonController(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        [HttpGet("{lessonId}")]
        [ProducesResponseType(200, Type = typeof(Lesson))]
        [ProducesResponseType(400)]
        public IActionResult GetLesson(int lessonId)
        {
            if (!_lessonRepository.LessonExists(lessonId))
                return NotFound();

            var lesson = _mapper.Map<LessonDto>(_lessonRepository.GetLesson(lessonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lesson);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lesson>))]
        public IActionResult getLessons()
        {
            var lessons = _mapper.Map<List<LessonDto>>(_lessonRepository.GetLessons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lessons);
        }
        
        [HttpGet("all/{lessonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        [ProducesResponseType(400)]
        public IActionResult GetSubjectsFromLesson(int lessonId)
        {
            if (!_lessonRepository.LessonExists(lessonId))
                return NotFound();

            var subjects = _mapper.Map<List<SubjectDto>>(_lessonRepository.GetSubjectsFromLesson(lessonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(subjects);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLesson([FromBody] LessonDto lessonCreate)
        {
            if (lessonCreate == null)
                return BadRequest(ModelState);
            var lesson = _lessonRepository.GetLessons()
                 .Where(l => l.Name.Trim().ToUpper() == lessonCreate.Name.Trim().ToUpper())
                 .FirstOrDefault();
            if (lesson != null)
            {
                ModelState.AddModelError("", "Lesson already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lessonMap = _mapper.Map<Lesson>(lessonCreate);

            if (!_lessonRepository.CreateLesson(lessonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{lessonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLesson(int lessonId, [FromBody] LessonDto updatedLesson)
        {
            if (updatedLesson == null)
            {
                return BadRequest(ModelState);
            }
            if (lessonId != updatedLesson.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_lessonRepository.LessonExists(lessonId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var lessonMap = _mapper.Map<Lesson>(updatedLesson);
            if (!_lessonRepository.UpdateLesson(lessonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating lesson");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{lessonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLesson(int lessonId)
        {
            if (!_lessonRepository.LessonExists(lessonId))
            {
                return NotFound();
            }
            var lessonToDelete = _lessonRepository.GetLesson(lessonId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_lessonRepository.DeleteLesson(lessonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting lesson");
            }
            return NoContent();

        }
    }
}
