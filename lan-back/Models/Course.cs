namespace lan_back.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }




    }
}
