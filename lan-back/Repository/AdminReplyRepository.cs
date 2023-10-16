using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class AdminReplyRepository : IAdminReplyRepository
    {
        private readonly DataContext _context;
        public AdminReplyRepository(DataContext context)
        {
            _context = context;
        }

        public Models.AdminReply GetAdminReply(int id)
        {
            return _context.AdminReplies.Where(r => r.Id == id).FirstOrDefault();
        }
        public ICollection<AdminReply> GetAdminReplies()
        {
            return _context.AdminReplies.ToList();
        }
        public bool AdminReplyExists(int id)
        {
            return _context.AdminReplies.Any(s => s.Id == id);
        }

        public bool CreateAdminReply(AdminReply adminreply)
        {
            _context.Add(adminreply);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateAdminReply(AdminReply adminreply)
        {
            _context.Update(adminreply);
            return Save();
        }
        public bool DeleteAdminReply(AdminReply adminreply)
        {
            _context.Remove(adminreply);
            return Save();
        }
    }
}
