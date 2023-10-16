using lan_back.Models;

namespace lan_back.Dto
{
    public class AdminReplyDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ReportId { get; set; }
    }
}
