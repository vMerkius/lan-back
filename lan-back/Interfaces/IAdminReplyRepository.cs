using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IAdminReplyRepository
    {
        AdminReply GetAdminReply(int id);
        ICollection<AdminReply> GetAdminReplies();
        bool AdminReplyExists(int id);
        bool CreateAdminReply(AdminReply adminreply);
        bool UpdateAdminReply(AdminReply adminreply);
        bool DeleteAdminReply(AdminReply adminreply);
        bool Save();
    }
}
