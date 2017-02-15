<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	
}

interface IBla
{
	void T();
}

class Bla : IBla
{
	public async void T()
	{
		await Task.CompletedTask;
	}
}