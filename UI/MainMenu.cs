using Godot;
using System;
using System.Threading.Tasks;

namespace Figments
{
	public class MainMenu : Control, Figments.Direction.IDirectable
	{
		public async Task SelfDirect()
		{
			GetNode<AnimationPlayer>("MenuAnimator").Play("ShowMenu");
			await ToSignal(GetNode<AnimationPlayer>("MenuAnimator"), "animation_finished");
			GetNode<AnimationPlayer>("LabelAnimator").Play("PressAnyKeyFade");
			GetNode<FileDialog>("PCKSelector").Popup_();
		}
	}
}