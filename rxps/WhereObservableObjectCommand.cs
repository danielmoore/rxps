using System;
using System.Management.Automation;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet(Verbs.Where, Nouns.ObservableObject, DefaultParameterSetName = FilterParameterSet)]
    public sealed class WhereObservableObjectCommand : StreamCmdlet
    {
        private const string FilterParameterSet = "FilterView";
        private const string OfTypeParameterSet = "OfTypeView";

        [Parameter(ParameterSetName = FilterParameterSet, Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = OfTypeParameterSet, Position = 1)]
        public ScriptBlock FilterScript { get; set; }

        [Parameter(ParameterSetName = OfTypeParameterSet, Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = FilterParameterSet)]
        public Type OfType { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            WriteObject(GetModifiedObservable());
        }

        private IObservable<object> GetModifiedObservable()
        {
            var stream = Stream;

            if (OfType != null)
                stream = LateBoundTypeCalls.Observable.OfType(OfType, stream);

            if (FilterScript != null)
                stream = stream.WithScriptBlock(FilterScript, (o, p) => o.Where(x => p(x)));

            return stream;
        }
    }
}
