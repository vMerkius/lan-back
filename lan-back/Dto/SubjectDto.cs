namespace lan_back.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imageUrl { get; set; }

        public int LessonId { get; set; }

    }
}
