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

async Task Main()
{
	var task = new CustomTask();
	await task;
	await task;
	await task;
	
	Console.WriteLine($"Main End: {DateTime.Now:mm.ss.fff}");
}

public class CustomTask
{
	CustomAwaiter _awaiter = new CustomAwaiter();
	
//	public System.Runtime.CompilerServices.TaskAwaiter GetAwaiter()
//	{
//		return ((Task)Task.FromResult(0)).GetAwaiter();
//	}

	public CustomAwaiter GetAwaiter()
	{
		return _awaiter;
	}
}

public class CustomAwaiter : INotifyCompletion
{
	int _incrementor;
	
	public bool IsCompleted => _incrementor > 1;

	public void OnCompleted(Action continuation)
	{
		_incrementor++;
		Thread.Sleep(500); //Her gjøres noe helt sinnsykt IO krevende greier.
		Console.WriteLine($"OnCompleted before: {DateTime.Now:mm.ss.fff}");
		continuation(); //Må kalle denne for å fortsette.
		Console.WriteLine($"OnCompleted after: {DateTime.Now:mm.ss.fff}");
	}

	public void GetResult() //Can have return type for var result = await [CustomTask];
	{
		Console.WriteLine($"GetResult: {DateTime.Now:mm.ss.fff}");
	}
}