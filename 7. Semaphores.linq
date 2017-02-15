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

SemaphoreSlim _serviceSemaphore;

async Task DoWork()
{
	Console.WriteLine("1");
	await _serviceSemaphore.WaitAsync();
	try
	{
		//Threadsafe:
		Console.WriteLine("2");
		await Task.Delay(500);
		Console.WriteLine("3");
	}
	finally
	{
		_serviceSemaphore.Release(); //Viktig :P
	}
}


async Task Main()
{
	_serviceSemaphore = new SemaphoreSlim(1, 1);

	//WhenAny, WaitAll etc.
//	await Task.WhenAll(
//		Task.Run(DoWork),
//		Task.Run(DoWork)
//	);
	
	await Task.WhenAll(
		Task.Run(() => DoWorkIgnore()),
		Task.Run(() => DoWorkIgnore())
	);

	Console.WriteLine("Main End");
}

int _lock = 0;
private void DoWorkIgnore()
{
	//ingen lock / Monitor.Enter:
	var result = Interlocked.Exchange(ref _lock, 1); //Alternative løsning.
	if (result == 0)
	{
		try
		{
			Console.WriteLine("First :D");
			Thread.Sleep(500);
		}
		finally
		{
			_lock = 0; //Threadsafe... (ikke dersom Increment/Decrement)
		}
	}
	else
		Console.WriteLine("Ignored");
}