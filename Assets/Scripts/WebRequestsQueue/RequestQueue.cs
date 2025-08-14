using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class RequestQueue<TRequest, TResponse> {
    private readonly ConcurrentDictionary<Guid, QueueItem> _queue = new ConcurrentDictionary<Guid, QueueItem>();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private readonly Func<TRequest, CancellationToken, Task<TResponse>> _processor;

    public RequestQueue(Func<TRequest, CancellationToken, Task<TResponse>> processor) {
        _processor = processor ?? throw new ArgumentNullException(nameof(processor));
    }

    public int PendingCount => _queue.Count;

    public async Task<QueueItem> EnqueueAsync(TRequest request, CancellationToken cancellationToken = default) {
        var item = new QueueItem(request, cancellationToken);
        _queue.TryRemove(item.Id, out _);
        _queue.TryAdd(item.Id, item);

        _ = ProcessNextAsync();
        return item;
    }

    public bool TryCancel(Guid requestId) {
        if (_queue.TryRemove(requestId, out var item)) {
            item.Cancel();
            return true;
        }
        return false;
    }

    private async Task ProcessNextAsync() {
        await _semaphore.WaitAsync();
        try {
            if (_queue.IsEmpty) return;

            var item = _queue.Values.FirstOrDefault();
            if (item == null) return;

            try {
                var response = await _processor(item.Request, item.CancellationToken);
                item.Complete(response);
            }
            catch (OperationCanceledException) {
                item.Cancel();
            }
            catch (Exception ex) {
                item.Fail(ex);
            }
            finally {
                _queue.TryRemove(item.Id, out _);
            }
        }
        finally {
            _semaphore.Release();
        }
    }

    public class QueueItem {
        public Guid Id { get; } = Guid.NewGuid();
        public TRequest Request { get; }
        public CancellationToken CancellationToken { get; }
        public Task<TResponse> Task => _tcs.Task;

        private readonly TaskCompletionSource<TResponse> _tcs;
        private readonly CancellationTokenSource _cts;

        public QueueItem(TRequest request, CancellationToken cancellationToken) {
            Request = request;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            CancellationToken = _cts.Token;
            _tcs = new TaskCompletionSource<TResponse>();
        }

        public void Complete(TResponse response) => _tcs.TrySetResult(response);
        public void Cancel() => _tcs.TrySetCanceled(_cts.Token);
        public void Fail(Exception ex) => _tcs.TrySetException(ex);
    }
}