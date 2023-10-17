using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IReportRepository
    
    {
        Report GetReport(int id);
        ICollection<Report> GetReports();
        bool ReportExists(int id);
        bool CreateReport(Report report);
        bool UpdateReport(Report report);
        bool DeleteReport(Report report);
        Reply GetReplyToReport(int id);
        bool Save();
    }

}
