using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IReplyRepository
    {
        Reply GetReply(int id);
        ICollection<Reply> GetReplies();
        bool ReplyExists(int id);
        bool CreateReply(Reply reply);
        bool UpdateReply(Reply reply);
        bool DeleteReply(Reply reply);
        bool Save();
    }
}
