﻿namespace lan_back.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
