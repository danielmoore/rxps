using System;
using System.Linq;
using System.Management.Automation;
using System.Reactive.Linq;

namespace rxps
{
    [Cmdlet(Verbs.Select, Nouns.ObservableObject)]
    public sealed class SelectObservableObjectCommand : StreamCmdlet
    {
        private const string PropertyParameterSet = "PropertyView";
        private const string ProcessScriptParameterSet = "ProcessScriptView";
        private const string FirstParameterSet = "FirstView";
        private const string LastParameterSet = "LastView";
        private const string DistinctUntilChangedParameterSet = "DistinctUntilChangedView";

        [Parameter(ParameterSetName = PropertyParameterSet, Mandatory = true)]
        public string[] Property { get; set; }

        [Parameter(ParameterSetName = ProcessScriptParameterSet, Mandatory = true)]
        public ScriptBlock ProcessScript { get; set; }

        [Parameter, ValidateRange(1, int.MaxValue)]
        public int DelaySeconds { get; set; }

        [Parameter(ParameterSetName = PropertyParameterSet)]
        [Parameter(ParameterSetName = ProcessScriptParameterSet)]
        public bool Unique { get; set; }

        [Parameter(ParameterSetName = DistinctUntilChangedParameterSet, Mandatory = true)]
        public bool UniqueUntilChanged { get; set; }

        [Parameter, AllowNull]
        public object Default { get; set; }

        [Parameter(ParameterSetName = FirstParameterSet, Mandatory = true)]
        public SwitchParameter First { get; set; }

        [Parameter(ParameterSetName = LastParameterSet, Mandatory = true)]
        public SwitchParameter Last { get; set; }

        [ValidateRange(1, int.MaxValue)]
        [Parameter(ParameterSetName = FirstParameterSet)]
        [Parameter(ParameterSetName = LastParameterSet)]
        [Parameter(ParameterSetName = PropertyParameterSet)]
        [Parameter(ParameterSetName = ProcessScriptParameterSet)]
        public int Count { get; set; }

        [Parameter(ParameterSetName = PropertyParameterSet)]
        [Parameter(ParameterSetName = ProcessScriptParameterSet)]
        public ScriptBlock WhileScript { get; set; }

        [Parameter(ParameterSetName = PropertyParameterSet)]
        [Parameter(ParameterSetName = ProcessScriptParameterSet)]
        public IObservable<object> Until { get; set; }

        [Parameter(ParameterSetName = PropertyParameterSet)]
        [Parameter(ParameterSetName = ProcessScriptParameterSet)]
        public ScriptBlock FilterScript { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var stream = Stream;

            if (DelaySeconds > 0)
                stream = stream.Delay(TimeSpan.FromSeconds(DelaySeconds));

            if (Last)
                stream = stream.TakeLast(Math.Max(1, Count));
            else if (First || Count > 0)
                stream = stream.Take(Math.Max(1, Count));

            if (Unique)
                stream = stream.Distinct();

            if (UniqueUntilChanged)
                stream = stream.DistinctUntilChanged();

            if (ProcessScript != null)
                stream = stream.WithScriptBlock(ProcessScript, (o, p) => o.Select(x => p(x)));

            if (Property != null)
                stream = stream.Select(GetFilteredObject);

            if (Until != null)
                stream = stream.TakeUntil(Until);

            if (WhileScript != null)
                stream = stream.WithScriptBlock(WhileScript, (o, p) => o.TakeWhile(x => p(x)));

            WriteObject(stream);
        }

        private object GetFilteredObject(object input)
        {
            var current = input as PSObject ?? (input != null ? new PSObject(input) : new PSObject());
            var next = new PSObject();

            var lookup = current.Properties.ToLookup(p => p.Name);

            foreach (var prop in Property)
                next.Properties.Add(lookup.Contains(prop) ? current.Properties[prop] : new PSNoteProperty(prop, null));

            return next;
        }
    }
}
