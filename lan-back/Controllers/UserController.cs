using AutoMapper;
using lan_back.Dto;
using lan_back.Interfaces;
using lan_back.Models;
using lan_back.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lan_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration= configuration;
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

        [HttpGet("byemail/{userEmail}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string userEmail)
        {

            var user = _mapper.Map<UserDto>(_userRepository.GetUserByEmail(userEmail));

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
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);
            var userName = _userRepository.GetUsers()
                .Where(a => a.Name.Trim().ToUpper() == userCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            var userEmail = _userRepository.GetUsers()
                .Where(a => a.Email.Trim().ToUpper() == userCreate.Email.Trim().ToUpper())
                .FirstOrDefault();
            if (userName != null)
            {
                ModelState.AddModelError("", "User with this name already exists");
                return StatusCode(422, ModelState);
            }
            if (userEmail != null)
            {
                ModelState.AddModelError("", "User with this email already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreate.Password);
            var userMap = _mapper.Map<User>(userCreate);
            userMap.PasswordHash = hashedPassword;

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPost("join/course")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult JoinCourse( [FromQuery] int userId ,[FromQuery] int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.JoinCourse(userId, courseId))
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
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isUserValid = _userRepository.Login(email, password);
            if (!isUserValid)
            {
                return BadRequest("Invalid email or password.");
            }
            //TODO token
            var user = _userRepository.GetUserByEmail(email);
            string token = CreateToken(user);
            return Ok(token);
        }
       /* public static string GenerateSecretKey()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[32]; 
                randomNumberGenerator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }*/

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("userId", user.Id.ToString())

            };
            //var secret = GenerateSecretKey();
            var secret = _configuration.GetSection("AppSettings:Token").Value!; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        [HttpGet("courses/attended/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult getUserCourses(int userId)
        {
            var courses = _mapper.Map<List<CourseDto>>(_userRepository.GetUserCourses(userId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courses);
        }
        [HttpGet("courses/nonparticipating/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetUserNonParticipatingCourses(int userId)
        {
            var courses = _mapper.Map<List<CourseDto>>(_userRepository.GetUserNonParticipatingCourses(userId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courses);
        }
    }
}
