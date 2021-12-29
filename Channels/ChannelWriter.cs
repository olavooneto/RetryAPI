using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace Channels
{
    public abstract class ChannelWriter<T>
    {
        /*public abstract bool TryWrite(T item);

        public async virtual ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            while (await WaitToWriteAsync(cancellationToken).ConfigureAwait(false))
            {
                if (TryWrite(item))
                    return;
            }

            throw new ChannelClosedException();
        }

        public abstract ValueTask<bool> WaitToWriteAsync(CancellationToken cancellation = default);

        public void Complete(Exception error);

        public virtual bool TryComplete(Exception error);*/
    }
}