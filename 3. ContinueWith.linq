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

void DoWork()
{
	DebugWrite();
	var task = DoOtherWork();
	task.ContinueWith(t => DebugWrite("Lambda"), TaskContinuationOptions.OnlyOnFaulted); //Not on main thread
	DebugWrite();
	Console.WriteLine("DoWork End");
}

async Task<int> DoOtherWork()
{
	DebugWrite();
	await Task.Delay(10).ConfigureAwait(false);
	DebugWrite();
	throw new Exception();
	return 123;
}

public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}, {Thread.CurrentThread.CurrentCulture}");
}








void Main()
{
	Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
	DoWork();
	Console.WriteLine("Main End");
}