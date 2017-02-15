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

void DoWorkCallback(Action callback)
{
	Console.WriteLine("1");
	Thread.Sleep(500);
	Console.WriteLine("2");
	callback();
}

Task DoWorkAsync()
{
	var x = new TaskCompletionSource<object>();
	try
	{
		DoWorkCallback(() =>
		{
			//throw new Exception("Bah!");
			Console.WriteLine("3");
			x.SetResult(new object());
		});
	}
    catch (Exception ex)
	{
		x.SetException(ex);
	}

	return x.Task;
}

async Task Main()
{
	await DoWorkAsync();
	Console.WriteLine("Main End");
}