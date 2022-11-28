using Godot;
using System;
using System.Threading.Tasks;

namespace Figments
{
	public class Splash : Control, Figments.Direction.IDirectable
	{
		public async Task SelfDirect()
		{
			await ToSignal(GetTree().CreateTimer(4), "timeout");
		}
	}
}