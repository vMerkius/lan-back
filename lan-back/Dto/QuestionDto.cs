namespace lan_back.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuizId { get; set; }

    }
}
