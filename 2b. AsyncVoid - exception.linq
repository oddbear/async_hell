<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
</Query>

async Task DoWork()
{
	await Task.Delay(10);
	throw new Exception();
}

void Main()
{
	try
	{
		DoWork();
	}
	catch (Exception)
	{
		Console.WriteLine("Exception caught. puh... crisis awerted :)");
	}
	Thread.Sleep(5000);
	Console.WriteLine("Main End");
}