using lan_back.Models;

namespace lan_back.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Age { get; set; }
        public string Country { get; set; }

    }
}
