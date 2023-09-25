﻿namespace lan_back.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}
