using System.Threading.Tasks;

namespace Repositories
{
    public interface IMessageRepository
    {
        Task<string>GetHelloMessage();
        Task<string>GetGoodbyeMessage();
    }
}