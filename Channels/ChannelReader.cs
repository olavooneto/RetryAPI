using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Channels
{
    public abstract class ChannelReader<T> 
    {
        /*public abstract bool TryRead(out T item);

        public async virtual ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                if (!await WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    throw new ChannelClosedException();
                }

                if (TryRead(out T item))
                    return item;
            }
        }

        public abstract ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default);

        public async virtual IAsyncEnumerable<T> ReadAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            while (true)
            {
                T item = await 
            }
        }

        public virtual Task Completion { get; }*/
    }
}