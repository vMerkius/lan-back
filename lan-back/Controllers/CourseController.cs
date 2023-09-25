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
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        [HttpGet("{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult GetModule(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var course = _mapper.Map<CourseDto>(_courseRepository.GetCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(course);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetCourses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courses);
        }

        [HttpGet("all/modules/{courseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Module>))]
        [ProducesResponseType(400)]
        public IActionResult GetModulesFromCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var modules = _mapper.Map<List<ModuleDto>>(_courseRepository.GetModulesFromCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(modules);
        }

        [HttpGet("all/quizzes/{courseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quiz>))]
        [ProducesResponseType(400)]
        public IActionResult GetQuizzesFromCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var quizzes = _mapper.Map<List<QuizDto>>(_courseRepository.GetQuizzesFromCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quizzes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);
            var course = _courseRepository.GetCourses()
                .Where(a => (a.Language.Trim().ToUpper() == courseCreate.Language.Trim().ToUpper())&&(a.Level.Trim().ToUpper()== courseCreate.Level.Trim().ToUpper()))
                .FirstOrDefault();
            if (course != null)
            {
                ModelState.AddModelError("", "Course already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Course>(courseCreate);

            if (!_courseRepository.CreateCourse(courseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCourse(int courseId, [FromBody] CourseDto updatedCourse)
        {
            if (updatedCourse == null)
            {
                return BadRequest(ModelState);
            }
            if (courseId != updatedCourse.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_courseRepository.CourseExists(courseId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var courseMap = _mapper.Map<Course>(updatedCourse);
            if (!_courseRepository.UpdateCourse(courseMap))
            {
                ModelState.AddModelError("", "Something went wrong updating course");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
            {
                return NotFound();
            }
            var courseToDelete = _courseRepository.GetCourse(courseId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_courseRepository.DeleteCourse(courseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting course");
            }
            return NoContent();

        }
    }
}
