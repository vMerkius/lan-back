namespace lan_back.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }


    }
}
