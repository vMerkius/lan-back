namespace lan_back.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string PolishWord { get; set; }
        public string EnglishWord { get; set; }
        public int FlashcardId { get; set; } 
        public Flashcard Flashcard { get; set; } 
    }
}
