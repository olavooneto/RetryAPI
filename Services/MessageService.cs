using System;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Repositories;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using System.Threading;


namespace Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessageService> _logger;
        private AsyncRetryPolicy _retryPolicy;
        private AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        private ChannelReader<string> _reader;

        private ChannelWriter<string> _writer;

        private readonly Channel<string> _channel;

        public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger, Channel<string> channel)
        {
            this._messageRepository = messageRepository;
            this._logger = logger;
            this._channel = channel;
            this._reader = this._channel.Reader;
            this._writer = this._channel.Writer;

            this._retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(10, retryAttempt =>
            {
                var timetoWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                Console.WriteLine($"Waiting {timetoWait.TotalSeconds} seconds");

                return timetoWait;
            });

            this._circuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1), (ex, t) =>
            {
                Console.WriteLine($"Cicuit broken. Exception:{ex?.Message}");

            }, () =>
            {
                Console.WriteLine("Circuit Reset!");
            });

        }

        public async Task<string> GetHelloMessage() => await this._retryPolicy.ExecuteAsync<string>(async () => await this._messageRepository.GetHelloMessage());


        public async Task<string> GetGoodbyMessage()
        {
            try
            {
                Console.WriteLine($"Circuit State:  {this._circuitBreakerPolicy.CircuitState}");
                return await _circuitBreakerPolicy.ExecuteAsync<string>(async () => { return await this._messageRepository.GetGoodbyeMessage(); });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void WriteMessage(string message)
        {
            this._logger.LogInformation($">>>>>>{nameof(WriteMessage)}<<<<<<<");
            this._logger.LogInformation($"{nameof(WriteMessage)}| {nameof(message)}: {message}");
            var success = this._writer.TryWrite(message);
            //this._channel.Writer.Complete();

            this._logger.LogInformation($"{nameof(success)}=> {success}");

            /*if (this._channel.Reader.TryRead(out string data))
                this._logger.LogInformation($"onQueue: {data}");*/
        }

        public async Task<string> Read()
        {
            this._logger.LogInformation($"{nameof(Read)}");

            await foreach (var message in this._reader.ReadAllAsync())
            {
                this._logger.LogInformation($"{nameof(message)}:{message} => {DateTime.Now}");
            }

            /*var success = this._reader.TryRead(out string data);
            this._logger.LogInformation($"{nameof(success)}=> {success}");

            if (success)
                this._logger.LogInformation($"onQueue: {data}");

            var value = await this._reader.ReadAsync();

            this._logger.LogInformation($"{nameof(value)}: {value}");*/

            
            return "";
        }
    }
}