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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type=typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("byname/{userName}")]
        [ProducesResponseType(200,Type=typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string userName)
        {

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("age/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserAge(int userId)
        {

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var today = DateTime.Today;
            var age = today.Year - user.DateOfBirth.Year;
            if (user.DateOfBirth.Date > today.AddYears(-age)) age--;

            return Ok(age);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult getUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] int courseId,[FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);
            var user = _userRepository.GetUsers()
                .Where(a => a.Name.Trim().ToUpper() == userCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(courseId, userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromQuery] int courseId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(ModelState);
            }
            if (userId != updatedUser.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userMap = _mapper.Map<User>(updatedUser);
            if (!_userRepository.UpdateUser(courseId, userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var userToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();

        }
    }
}
