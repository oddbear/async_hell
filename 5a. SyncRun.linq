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
	Thread.Sleep(500);
	DebugWrite();
	await Task.Delay(500);
	DebugWrite();
}

void Main()
{
	_main = Thread.CurrentThread;

	var t = DoWork(); //Obs, kan fungere annerledes. F.eks. i Xamarin.Android
	DebugWrite();
	Thread.Sleep(750);
	DebugWrite();

	Console.WriteLine("Main End");
}

public void DebugWrite([CallerMemberName]string memberName = "")
{
	Console.WriteLine($"{memberName}: {IsMainThread}, {Thread.CurrentThread.CurrentCulture}, {DateTime.Now:HH\\:mm\\:ss\\.fff}");
}