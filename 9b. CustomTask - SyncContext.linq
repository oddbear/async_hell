<Query Kind="Program">
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	Thread.CurrentThread.ManagedThreadId.Dump();
	SynchronizationContext.SetSynchronizationContext(new MySyncContext());
	var t = new MyTask();
	await t;
	Thread.CurrentThread.ManagedThreadId.Dump();
}

public class MyTask
{
	public MyTaskAwaiter GetAwaiter()
	{
		Debug.WriteLine(nameof(GetAwaiter) + " " + Thread.CurrentThread.ManagedThreadId.Dump());
		return new MyTaskAwaiter(this);
	}
}

public struct MyTaskAwaiter : INotifyCompletion
{
	MyTask _task;
	
	public MyTaskAwaiter(MyTask task)
	{
		_task = task;
	}
	
	public bool IsCompleted => false;

	public void OnCompleted(Action continuation)
	{
		Debug.WriteLine(nameof(OnCompleted) + " " + Thread.CurrentThread.ManagedThreadId.Dump());
		//if ConfigureAwait false: no context.
		
		var context = SynchronizationContext.Current;
		if(context != null)
			context.Post(obj => continuation(), null);
		else
			continuation(); //Normalt bruker TaskScheduler her...
	}
	
	private static void InvokeAction(object state) { ((Action)state)(); }
	
	public void GetResult()
	{
		Debug.WriteLine(nameof(GetResult) + " " + Thread.CurrentThread.ManagedThreadId.Dump());
	}
}

public class MySyncContext : SynchronizationContext
{
	public override void Post(SendOrPostCallback d, object state)
	{
		Debug.WriteLine(nameof(Post) + " " + Thread.CurrentThread.ManagedThreadId.Dump());
		d(state);
	}
}