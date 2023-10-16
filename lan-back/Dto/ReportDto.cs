using lan_back.Models;

namespace lan_back.Dto
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public bool IsReviewed { get; set; } = false;
        public int AdminReplyId { get; set; }

    }
}
