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

void DoWork()
{
	DebugWrite();
	var result = DoOtherWork().Result;
	Console.WriteLine(result);
	DebugWrite();
	Console.WriteLine("DoWork End");
}

async Task<int> DoOtherWork()
{
	DebugWrite();
	await Task.Delay(10);
	DebugWrite();
	return 123;
}


public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}");
}

void Main()
{
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
	DoWork();
	Console.WriteLine("Main End");
}