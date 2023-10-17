using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly DataContext _context;
        public ReplyRepository(DataContext context)
        {
            _context = context;
        }

        public Models.Reply GetReply(int id)
        {
            return _context.Replies.Where(r => r.Id == id).FirstOrDefault();
        }
        public ICollection<Reply> GetReplies()
        {
            return _context.Replies.ToList();
        }
        public bool ReplyExists(int id)
        {
            return _context.Replies.Any(s => s.Id == id);
        }

        public bool CreateReply(Reply reply)
        {
            _context.Add(reply);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateReply(Reply reply)
        {
            _context.Update(reply);
            return Save();
        }
        public bool DeleteReply(Reply reply)
        {
            _context.Remove(reply);
            return Save();
        }
    }
}
