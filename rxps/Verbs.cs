using System.Management.Automation;

namespace rxps
{
    internal static class Verbs
    {
        public const string Invoke = VerbsLifecycle.Invoke;
        public const string Where = "Where";
        public const string Select = VerbsCommon.Select;
        public const string Skip = VerbsCommon.Skip;
        public const string New = VerbsCommon.New;
        public const string Start = VerbsLifecycle.Start;
        public const string Stop = VerbsLifecycle.Stop;
        public const string ConvertTo = VerbsData.ConvertTo;
        public const string Set = VerbsCommon.Set;
        public const string Add = VerbsCommon.Add;
        public const string ForEach = "ForEach";
        public const string Convert = VerbsData.Convert;
        public const string Merge = VerbsData.Merge;
        public const string Group = VerbsData.Group;
        public const string Measure = VerbsDiagnostic.Measure;
        public const string Test = VerbsDiagnostic.Test;
        public const string Initialize = VerbsData.Initialize;
        public const string Reset = VerbsCommon.Reset;
        public const string Publish = VerbsData.Publish;
    }
}
