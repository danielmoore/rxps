using System;
using System.Management.Automation;

namespace rxps
{
    internal static class Extensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source, Cmdlet target)
        {
            return source.Subscribe(x => target.WriteObject(x), e => target.WriteError(new ErrorRecord(e, string.Empty, ErrorCategory.NotSpecified, null)));
        }
    }
}
