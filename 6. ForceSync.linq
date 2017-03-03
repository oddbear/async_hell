<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

Thread _main;
bool IsMainThread => Thread.CurrentThread == _main;

void Main() //ForceSyncIsh...
{
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());

	Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
	Console.WriteLine($"Start, {Thread.CurrentThread.CurrentCulture}");
	
	//var a = DoWork(3).GetAwaiter().GetResult(); //.Result; //DeadLock

	Console.WriteLine("--- start ---");
	var x = Task.Run(() => DoWork(1)).GetAwaiter().GetResult(); //.Result; or .Wait(); -> Wrappes Exception as a inner exception of AggregateException... use GetResult()
	Console.WriteLine("--- end ---");

	Console.WriteLine("--- start ---");
	var y = AsyncHelper.RunSync(() => DoWork(2));
	Console.WriteLine("--- end ---");
	
	Console.WriteLine($"Done, {Thread.CurrentThread.CurrentCulture}");
}

private async Task<int> DoWork(int n)
{
	DebugWrite();
	await Task.Delay(100);
	DebugWrite();
	return 0;
}

internal static class AsyncHelper //Microsoft.AspNet.Identity
{
	private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

	public static TResult RunSync<TResult>(Func<Task<TResult>> func)
	{
		CultureInfo cultureUi = CultureInfo.CurrentUICulture;
		CultureInfo culture = CultureInfo.CurrentCulture;
		return AsyncHelper._myTaskFactory.StartNew<Task<TResult>>((Func<Task<TResult>>)(() =>
		{
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = cultureUi;
			return func();
		})).Unwrap<TResult>().GetAwaiter().GetResult();
	}
}

public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}, {Thread.CurrentThread.CurrentCulture}");
}