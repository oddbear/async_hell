<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

Thread _main;
bool IsMainThread => Thread.CurrentThread == _main;

async Task<int> DoWork()
{
	DebugWrite();
	await Task.Delay(1000);
	DebugWrite();
	Console.WriteLine("DoWork End");
	
	return 1;
}


public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}, {Thread.CurrentThread.CurrentCulture}");
}






Task<int> _task;

async Task Main()
{
	Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
	
	_task = DoWork();

	var t1 = Task.Run(async () =>
	{
		await _task; //Waits same task as t2
		Console.WriteLine("t1 end");
	});
	
	var t2 = Task.Run(async () =>
	{
		await _task; //Waits same task as t1
		Console.WriteLine("t2 end");
	});

	await Task.WhenAll(t2, t1); //Waits 2 tasks.
	
	Console.WriteLine("Main End");
}