using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }
        public Report GetReport(int id)
        {
            return _context.Reports.Where(r => r.Id == id).FirstOrDefault();
        }
        public ICollection<Report> GetReports()
        {
            return _context.Reports.ToList();
        }
        public bool ReportExists(int id)
        {
            return _context.Reports.Any(r => r.Id == id);
        }

        public bool CreateReport(Report report)
        {
            _context.Add(report);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateReport(Report report)
        {
            _context.Update(report);
            return Save();
        }

        public bool DeleteReport(Report report)
        {
            _context.Remove(report);
            return Save();
        }

        public Reply GetReplyToReport(int id)
        {
            return _context.Replies.Where(r=>r.ReportId == id).FirstOrDefault();
        }
    }
}
