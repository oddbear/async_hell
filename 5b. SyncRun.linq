<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
</Query>

Thread _main;
bool IsMainThread => Thread.CurrentThread == _main;

async Task DoWork()
{
	DebugWrite();
	
	//await Task.Yield();
	//await Task.FromResult(0);
	//await Task.Delay(0);
	//await Task.Delay(1);
	
	DebugWrite();
}

void Main()
{
	_main = Thread.CurrentThread;
	
	var t = DoWork();

	Console.WriteLine("Main End");
}

public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}, {Thread.CurrentThread.CurrentCulture}");
}