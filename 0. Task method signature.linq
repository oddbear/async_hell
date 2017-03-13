<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

void Main()
{
    var obj = typeof(Bla).GetMethod(nameof(IBla.T));
    obj.GetCustomAttributes().OfType<AsyncStateMachineAttribute>().SingleOrDefault().Dump(); //Has a secret attribute.
    obj.ReturnType.Dump();
    
    var iface = typeof(IBla).GetMethod(nameof(IBla.T));
    iface.GetCustomAttributes().OfType<AsyncStateMachineAttribute>().SingleOrDefault().Dump(); //Does not have the attribute.
    iface.ReturnType.Dump();
}

interface IBla
{
	void T(); //The async keyword is not a part of the signature.
}

class Bla : IBla
{
	public async void T() //The interface is happy with this.
	{
		await Task.CompletedTask;
	}
}