using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet(Verbs.Start, Nouns.Observable)]
    public sealed class StartObservableCommand : StreamCmdlet, IDisposable
    {
        private readonly SingleAssignmentDisposable _disposable = new SingleAssignmentDisposable();

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var queue = new BlockingCollection<object>();

            _disposable.Disposable = Stream.Finally(queue.CompleteAdding).Subscribe(queue.Add);

            foreach (var item in queue.GetConsumingEnumerable())
                WriteObject(item);
        }

        private IObservable<object> Convert<T>(IObservable<T> source)
        {
            return source.Select<T, object>(x => x);
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();

            _disposable.Dispose();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
