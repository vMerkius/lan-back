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
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        [HttpGet("{subjectsId}")]
        [ProducesResponseType(200, Type = typeof(Subject))]
        [ProducesResponseType(400)]
        public IActionResult GetSubject(int subjectsId)
        {
            if (!_subjectRepository.SubjectExists(subjectsId))
                return NotFound();

            var subject = _mapper.Map<SubjectDto>(_subjectRepository.GetSubject(subjectsId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(subject);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        public IActionResult getSubjects()
        {
            var subjects = _mapper.Map<SubjectDto>(_subjectRepository.GetSubjects());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(subjects);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSubject([FromBody] SubjectDto subjectCreate)
        {
            if (subjectCreate == null)
                return BadRequest(ModelState);
            var subject = _subjectRepository.GetSubjects()
                .Where(a => a.Name.Trim().ToUpper() == subjectCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (subject != null)
            {
                ModelState.AddModelError("", "Subject already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subjectMap = _mapper.Map<Subject>(subjectCreate);

            if (!_subjectRepository.CreateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");

        }
        [HttpPut("{subjectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSubject(int subjectId, [FromBody] SubjectDto updatedSubject)
        {
            if (updatedSubject == null)
            {
                return BadRequest(ModelState);
            }
            if (subjectId != updatedSubject.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var subjectMap = _mapper.Map<Subject>(updatedSubject);
            if (!_subjectRepository.UpdateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating subject");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{subjectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }
            var subjectToDelete = _subjectRepository.GetSubject(subjectId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_subjectRepository.DeleteSubject(subjectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting subject");
            }
            return NoContent();

        }
    }
}
