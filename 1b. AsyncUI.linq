<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

Thread _main;
bool IsMainThread => Thread.CurrentThread == _main;

async Task DoWork()
{
	DebugWrite();
	await Task.Delay(1);
	DebugWrite();
	Console.WriteLine("DoWork End");
}

public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}");
}








async Task Main()
{
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
	await DoWork();
	Console.WriteLine("Main End");
}