<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/*
Output:
1
2
4
System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Int32]
3
*/

async Task<int> DoWork()
{
	Console.WriteLine("2");
	await Task.Delay(100);
	Console.WriteLine("3");
	
	return -1;
}

Console.WriteLine("1");
var result = Task.Run(() => DoWork().ConfigureAwait(false)) //no Task.Run(async () => await ...)
	.ConfigureAwait(false)
	.GetAwaiter()
	.GetResult(); //We await and lock the ConfiguredTaskAwaitable, and not the task.

Console.WriteLine("4");
Console.WriteLine(result.ToString());