using System;
using System.Management.Automation;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet("Get", "Interval")]
    public sealed class GetIntervalCmdlet : Cmdlet
    {
        private readonly SingleAssignmentDisposable _dispsosable = new SingleAssignmentDisposable();

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var timespan = TimeSpan.FromSeconds(Seconds);
            WriteObject(Scheduler == null ? Observable.Interval(timespan) : Observable.Interval(timespan, Scheduler));
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();

            _dispsosable.Dispose();
        }

        [Parameter(Mandatory = true, Position = 0)]
        public int Seconds { get; private set; }

        [Parameter(Position = 1)]
        public IScheduler Scheduler { get; private set; }
    }
}
