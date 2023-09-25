namespace lan_back.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Flashcard> Flashcards { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
