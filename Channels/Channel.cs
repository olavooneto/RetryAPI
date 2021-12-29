using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using  System.Threading.Channels;
namespace Channels
{
    public sealed class Channel<T>
    {
        //ChannelWriter<T> t = System.Threading.Channels.Channel.CreateBounded<T>(10);
         
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private readonly SemaphoreSlim _semphore = new SemaphoreSlim(0);

        public void Write(T value)
        {
            this._queue.Enqueue(value); // Store the data
            this._semphore.Release(); // notify any consumer that more data is avaiable
        }
        public async ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            await this._semphore.WaitAsync(cancellationToken).ConfigureAwait(false); // wait
            bool gotOne = this._queue.TryDequeue(out T item); // retrieve data
            Debug.Assert(gotOne);

            return item;
        }

    }
}