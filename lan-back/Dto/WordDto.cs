namespace lan_back.Dto
{
    public class WordDto
    {
        public int Id { get; set; }
        public string OriginalWord { get; set; }
        public string TranslatedWord { get; set; }
        public string ImageUrl { get; set; }

        public int FlashcardId { get; set; }

    }
}
