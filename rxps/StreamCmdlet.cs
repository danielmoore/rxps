using System;
using System.Management.Automation;
using System.Reactive.Linq;

namespace rxps
{
    public abstract class StreamCmdlet : PSCmdlet
    {
        protected StreamCmdlet() { }

        private IObservable<object> _stream;
        protected IObservable<object> Stream { get { return _stream ?? (_stream = Convert((dynamic)InputObservable.BaseObject)); } }

        private IObservable<object> Convert<T>(IObservable<T> source)
        {
            return source.Select<T, object>(x => x);
        }

        [ValidateNotNull, Parameter(Mandatory = true, ValueFromPipeline = true)]
        public PSObject InputObservable { get; set; }
    }
}
