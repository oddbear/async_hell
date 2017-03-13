<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

bool _waitForSyncContext = true;

void Main()
{
    var syncContext = new SyncContext();
    SynchronizationContext.SetSynchronizationContext(syncContext);
    
    Test();
    
    if (_waitForSyncContext)
        syncContext.WaitOne();
    
    Console.WriteLine("Main");
}

public async void Test()
{
    Console.WriteLine("1");
    await Task.Delay(100).ConfigureAwait(false);
    SynchronizationContext.SetSynchronizationContext(null);
    Console.WriteLine("2");
    await Task.Delay(100).ConfigureAwait(false);
    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
    Console.WriteLine("3");
    await Task.Delay(100).ConfigureAwait(false);
    Console.WriteLine("4");
}

public class SyncContext : SynchronizationContext
{
    private ManualResetEvent _resetEvent = new ManualResetEvent(false);
    
    public override void OperationCompleted()
    {
        base.OperationCompleted();
        _resetEvent.Set();
        Console.WriteLine("OperationCompleted");
    }

    public override void OperationStarted()
    {
        base.OperationStarted();
        Console.WriteLine("OperationStarted");
    }
    
    public void WaitOne()
    {
        _resetEvent.WaitOne();
    }
}