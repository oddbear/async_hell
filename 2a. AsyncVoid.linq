<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
</Query>

Thread _main;
bool IsMainThread => Thread.CurrentThread == _main;

async void DoWork()
{
	IsMainThread.Dump();
	await Task.Delay(1);
	IsMainThread.Dump();
	Console.WriteLine("DoWork End");
}










void Main()
{
	_main = Thread.CurrentThread;
	SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
	DoWork();
	IsMainThread.Dump();
	Console.WriteLine("Main End");
}