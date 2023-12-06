namespace lan_back.Models
{
    public class Sentence
    {
        public int Id { get; set; }
        public string Original { get; set; }
        public string Translated { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
