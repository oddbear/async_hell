<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

async Task Main()
{ 
	//Default culture:
	//CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
	Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
	
	Thread.CurrentThread.CurrentCulture.Dump();
	await Task.Delay(100).KeepCulture();
	Thread.CurrentThread.CurrentCulture.Dump();
	await Task.Delay(100).ConfigureAwait(false).KeepCulture();
	Thread.CurrentThread.CurrentCulture.Dump();
	await Task.Delay(100).ConfigureAwait(true).KeepCulture();
	Thread.CurrentThread.CurrentCulture.Dump();

	var task = Task.Delay(100);
	var t1 = task.ContinueWith(t => {
		Console.WriteLine("Test1");
	});
	var t2 = task.ContinueWith(t =>
	{
		Console.WriteLine("Test2");
	});
	var t3 = task.KeepCulture(); //Does not compute.
	await task;
	Thread.CurrentThread.CurrentCulture.Dump();
}

public static class MyExtension
{
	public static Task KeepCulture(this Task task)
	{
		var culture = Thread.CurrentThread.CurrentCulture;
		return task.ContinueWith(t =>
		{
			Thread.CurrentThread.CurrentCulture = culture;
		});
	}
	
	public static ConfiguredTaskAwaitable KeepCulture(this ConfiguredTaskAwaitable task)
	{
		var culture = Thread.CurrentThread.CurrentCulture;
		var awaiter = task.GetAwaiter();
		awaiter.OnCompleted(() => {
			Thread.CurrentThread.CurrentCulture = culture;
		});
		return task;
	}
}