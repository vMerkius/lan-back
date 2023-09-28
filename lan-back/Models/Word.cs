namespace lan_back.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string OriginalWord { get; set; }
        public string TranslatedWord { get; set; }
        public int FlashcardId { get; set; } 
        public Flashcard Flashcard { get; set; } 
    }
}
