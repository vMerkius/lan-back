namespace lan_back.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public bool IsReviewed { get; set; } = false;
        public int AdminReplyId { get; set; }
        public AdminReply AdminReply { get; set; }
    }
}
