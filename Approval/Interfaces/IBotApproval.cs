using System.Threading.Tasks;

namespace Approval.Interfaces
{
    public interface IBotApproval
    {
         Task SendMessageAllUser(string message);
         Task SendApproveOrder(int chat, string message);
         Task SendMessageToUser(string message);
    }
}
