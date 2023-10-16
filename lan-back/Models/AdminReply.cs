namespace lan_back.Models
{
    public class AdminReply
    { 
        public int Id { get; set; }
        public string Message { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }
}
