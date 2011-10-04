using System.Management.Automation;
using System.Reactive.Subjects;

namespace rxps
{
    [Cmdlet(Verbs.ConvertTo, Nouns.Observable)]
    public sealed class ConvertToObservableCommand : PSCmdlet
    {
        private ISubject<object> _subject;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            WriteObject(_subject = new Subject<object>());
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            _subject.OnNext(InputObject);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();

            _subject.OnCompleted();
        }

        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        public object InputObject { get; set; }
    }
}
