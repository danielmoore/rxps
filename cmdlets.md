# Cmdlets in rxps

This is a roadmap for the implementation of cmdlets in rxps.

## Observable Subscription

    Start-Observable [-AsJob]
    (Subscribe)

    Stop-Observable -InputObservable <IObservable<object>>
    (Dispose)

## Observable Generation

    ConvertTo-Observable -InputObject <psobject>

    New-Observable [-DelaySeconds <int>] [-IntervalSeconds <int>] [-Scheduler <IScheduler>]
    (Interval, Timer)

    New-Observable -ProducerScript <ScriptBlock> [-Disposable]
    (Create)

    New-Observable -DeferredObservableScript <ScriptBlock>
    (Defer)

    New-Observable -InitialState <object> -Process <ScriptBlock> [-ConditionScript <ScriptBlock>] -Properties <string[]> 
                   -IntervalSeconds <int> [-Scheduler <IScheduler>]
    (Generate)

    New-Observable -Empty
    (Empty)

    New-Observable -Never
    (Never)

    New-Observable -Range -Min <int> -Max <int> [-Scheduler <IScheduler>]
    (Range)

    New-Observable -Value <object> [-Scheduler <IScheduler>]
    (Return)

## Concurrency Management

    Set-ObservableScheduler -InputObservable <IObservable<object>> [-Subscribing] -Scheduler <IScheduler>
    (ObserveOn, SubscribeOn)

    Set-ObservableScheduler -InputObservable <IObservable<object>> [-Subscribing] -SyncContext <SynchronizationContext>
    (ObserveOn, SubscribeOn)

## Object Selection

    Skip-ObservableObject [-Count <int>] [-WhileScript <ScriptBlock>] [-Until <IObservable>)] [-First] [-Last] [-All] 
                          -InputObservable <IObservable<object>>
    (Skip, SkipWhile, SkipUntil, Skiplast, IgnoreElments)

    Skip-ObservableObject -InputObservable <IObservable<object>> -IntervalSeconds <int> [-Scheduler <IScheduler>] [-Sample]
    (Throttle, Sample)

    Skip-ObservableObject -InputObservable <IObservable<object>> -SampleScript <ScriptBlock>
    (Sample)

    Add-RefToDisposable -Disposable <RefCountDisposable> -InputObservable <IObservable<object>>
    (AddRef)

    Where-ObservableObject -FilterScript <ScriptBlock> [-OfType <Type>] -InputObservable <IObservable<object>>
    Where-ObservableObject -OfType <Type> [-FilterScript <ScriptBlock>] -InputObservable <IObservable<object>>
    Alias: %?
    (Where, DefaultIfEmpty)

    Select-ObservableObject -Property <string[]> -InputObservable <IObservable<object>> [-DelaySeconds <int>] [-Unique] 
                            [-UniqueUntilChanged] [-Default [<object>]] [-Index <int>] [-First] [-Last] [-Count <int>] [-Single]
                            [-WhileScript <ScriptBlock>] [-Until <IObservable<object>>] [-FilterScript <ScriptBlock>]
    Alias: %select
    (Select, Delay, Distinct, DistinctUntilChanged, DefaultIfEmpty, ElementAt, ElementAtOrDefault, First, FirstOrDefault,
     Last, LastOrDefault, Single, SingleOrDefault Take, TakeLast, TakeWhile, TakeUntil)

    Convert-Observable -Type <Type> -InputObservable <IObservable<object>>
    (Cast)

    Foreach-ObservableObject -Process <ScriptBlock> [-Begin <ScriptBlock>] [-End <ScriptBlock>]
    Alias: %%
    (Do, Defer [side effect], Finally)

## Aggregation

    Group-Observable -Key <ScriptBlock> [-Value <ScriptBlock>] [-Until <ScriptBlock>]
    Alias: %group
    (GroupBy, GroupByUntil)

    Group-Observable -Aggregate <ScriptBlock> [-Seed <object>]
    Alias: %group
    (Aggregate)

    Group-Observable -Scan <ScriptBlock> [-Seed <object>]
    Alias: %group
    (Scan)

    Measure-Observable [-Average] [-Min] [-MinBy <ScriptBlock>] [-Max] [-MaxBy <ScriptBlock>] [-Sum] [-Count] [-LongCount]
    Alias: %measure
    (Average, Min, MinBy, Max, MaxBy, Sum, Count, LongCount)

## Conditionals

    Test-Observable -InputObservable <IObservable<object>> -Test <ScriptBlock> [-All]
    Alias: %test
    (Any, All)

    Test-Observable -InputObservable <IObservable<object>> -Contains <object>
    Alias: %test
    (Contains)

## Merging

    Select-ObservableObject -Merge -Property <string[]> 
    (SelectMany)

    Initialize-Observable -InputObservable <IObservable<object>> -ObservableObject object
    (StartWith)

    Merge-Observable -InputObservable <IObservable<object>> -Observables <IObservable<object>[]>
    (Merge)

    Merge-Observable -InputObservable <IObservable<object>> -Observables <IObservable<object>[]> -CombinatorScript <ScriptBlock> [-Zip]
    (CombineLatest, Zip)

    Add-Observable -InputObservable <IObservable<object>> [-NextOnError] -Observables <IObservable<object>[]>
    (Concat, OnErrorResumeNext)

    Add-Observable -OnError -Error <Type> -HandlingScript <ScriptBlock> -InputObservable <IObservable<object>>
    (Catch)

    Add-Observable -OnError -InputObservable <IObservable<object>> -ContinueWith <IObservable<object>[]>
    (Catch)

    Add-Observable -OnTimeout -Seconds <int> -InputObservable <IObservable<object>> -ContinueWith <IObservable<object>> [-Scheduler <IScheduler>]
    (Timeout)

## Windowing 
    
    Group-Observable -InputObservable <IObservable<object>> [-Buffer] [-Openings <IObservable<object>>] -Closings <ScriptBlock>
    Alias: %group
    (Window, Buffer)

    Group-Observable -InputObservable <IObservable<object>> [-Buffer] [-Seconds <int>] [-Count <int>] [-Skip <int>] [-Scheduler <IScheduler>]
    Alias: %group
    (Window, Buffer)

## Reiteration

    Reset-Observable -InputObservable <IObservable<object>> -OnError [-Count <int>]
    (Retry)

    Reset-Observable -InputObservable <IObservable<object>> [-Count <int>] [-Scheduler <IScheduler>]
    (Repeat)

## Switching

    Select-Observable -First -InputObservable <IObservable<object>> -Observables (IObservable<object>[])
    (Amb)

    Select-Observable -Latest -InputObservable <IObservable<object>> -Observables (IObservable<object>[])
    (Switch)

## Multicasting

    Publish-Observable -InputObservable <IObservable<object>> [-SubjectScript <ScriptBlock>] [-Process <ScriptBlock>] [-RefCount]
    Alias: %publish
    (Multicast, RefCount)

    Publish-Observable -InputObservable <IObservable<object>> [-Last] [-Process <ScriptBlock>] [-Scheduler <IScheduler>] [-RefCount]
    Alias: %publish
    (Replay, RefCount)

    Publish-Observable -InputObservable <IObservable<object>> -Replay [-Seconds <int>] [-Count <int>] [-Process <ScriptBlock>] 
                       [-Scheduler <IScheduler>] [-RefCount]
    Alias: %publish
    (Replay, RefCount)
