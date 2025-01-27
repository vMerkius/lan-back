﻿using lan_back.Models;

namespace lan_back.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public bool isAdmin { get; set; }


    }
}
