using System.Threading.Tasks;

namespace Services
{
    public interface IMessageService
    {
        Task<string> GetHelloMessage();

        Task<string> GetGoodbyMessage();

        void WriteMessage(string message);

        Task<string> Read();
    }
}