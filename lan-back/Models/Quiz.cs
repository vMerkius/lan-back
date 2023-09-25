﻿namespace lan_back.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
