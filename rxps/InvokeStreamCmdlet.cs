using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet("Invoke", "Stream")]
    public sealed class InvokeStreamCmdlet : Cmdlet
    {
        private readonly SingleAssignmentDisposable _disposable = new SingleAssignmentDisposable();

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            IObservable<object> stream = Convert((dynamic)Stream.BaseObject);

            var queue = new BlockingCollection<object>();

            _disposable.Disposable = stream.Finally(queue.CompleteAdding).Subscribe(queue.Add);

            object item;
            while (!Stopping && queue.TryTake(out item, -1))
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

        [ValidateNotNull, Parameter(ValueFromPipeline = true)]
        public PSObject Stream { get; set; }
    }
}
