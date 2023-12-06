namespace lan_back.Dto
{
    public class SentenceDto
    {
        public int Id { get; set; }
        public string Original { get; set; }
        public string Translated { get; set; }
        public int ModuleId { get; set; }
    }
}
