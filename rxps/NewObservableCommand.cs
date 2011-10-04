using System;
using System.Management.Automation;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet(VerbsCommon.New, "Observable")]
    public sealed class NewObservableCommand : PSCmdlet
    {
        private const string TimerParameterSet = "TimerView";
        private const string CreateParameterSet = "CreateView";
        private const string DeferParameterSet = "DeferView";
        private const string GenerateParameterSet = "GenerateView";
        private const string EmptyParameterSet = "EmptyView";
        private const string NeverParemeterSet = "NeverView";
        private const string RangeParameterSet = "RangeView";
        private const string ReturnParameterSet = "ReturnView";

        [Parameter(ParameterSetName = TimerParameterSet, Mandatory = true, Position = 0)]
        public int Seconds { get; private set; }

        [Parameter(ParameterSetName = RangeParameterSet, Position = 0, Mandatory = true)]
        public int Min { get; set; }

        [Parameter(ParameterSetName = RangeParameterSet, Position = 1, Mandatory = true)]
        public int Max { get; set; }

        [Parameter(ParameterSetName = EmptyParameterSet, Mandatory = true)]
        public SwitchParameter Empty { get; set; }

        [Parameter(ParameterSetName = NeverParemeterSet, Mandatory = true)]
        public SwitchParameter Never { get; set; }

        [Parameter(ParameterSetName = ReturnParameterSet, Mandatory = true), AllowNull]
        public object Value { get; set; }

        [Parameter(ParameterSetName = TimerParameterSet, Position = 1)]
        [Parameter(ParameterSetName = RangeParameterSet, Position = 2)]
        [Parameter(ParameterSetName = ReturnParameterSet)]
        public IScheduler Scheduler { get; private set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            WriteObject(CreateObservable());
        }

        private object CreateObservable()
        {
            switch (ParameterSetName)
            {
                case NeverParemeterSet:
                    return Observable.Never<object>();

                case EmptyParameterSet:
                    return Observable.Empty<object>();

                case RangeParameterSet:
                    return Observable.Range(Min, Max);

                case ReturnParameterSet:
                    if (Scheduler != null)
                        return Observable.Return(Value, Scheduler);
                    else
                        return Observable.Return(Value);

                case TimerParameterSet:
                    var timeSpan = TimeSpan.FromSeconds(Seconds);
                    if (Scheduler != null)
                        return Observable.Interval(timeSpan, Scheduler);
                    else
                        return Observable.Interval(timeSpan);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
