using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private MessageOptions _messageOptions;

        public MessageRepository(IOptions<MessageOptions> messageOptions)
        {
            this._messageOptions = messageOptions.Value;
        }

        public async Task<string> GetHelloMessage()
        {
            Console.WriteLine($"---------------{nameof(GetHelloMessage)}-{DateTime.Now}------------");
            Console.WriteLine("Message Repository GetHelloMessage running");
            ThrowRandomException();

            return _messageOptions.HelloMessage;
        }

        public async Task<string> GetGoodbyeMessage()
        {
            Console.WriteLine($"---------------{nameof(GetGoodbyeMessage)}-{DateTime.Now}------------");

            Console.WriteLine("MessageRepository GetGoodByeMessage running");
            ThrowRandomException();

            return _messageOptions.GoodbyeMessage;

        }

        private void ThrowRandomException()
        {
            var diecRoll = new Random().Next(0, 10);

            if (diecRoll > 5)
            {
                Console.WriteLine("ERROR! throwing exception");
                throw new Exception("Exception in MessageRepository");
            }

        }


    }
}