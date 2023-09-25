namespace lan_back.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Word> Words { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}
