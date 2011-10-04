using System;
using System.Management.Automation;
using System.Reactive.Linq;

namespace rxps
{
    internal static class Extensions
    {
        public static IObservable<TOut> WithScriptBlock<TIn, TOut>(this IObservable<TIn> source, ScriptBlock scriptBlock, Func<IObservable<TIn>, Func<object, dynamic>, IObservable<TOut>> streamSelector)
        {
            var pipeline = scriptBlock.GetSteppablePipeline();

            return Observable.Defer(() =>
            {
                pipeline.Begin(false);
                return streamSelector(source, o => ((object[])pipeline.Process(o))[0]).Finally(() => pipeline.End());
            });
        }
    }
}
