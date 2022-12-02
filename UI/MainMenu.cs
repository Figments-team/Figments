using Godot;
using System;
using System.Threading.Tasks;

namespace Figments
{
	public class MainMenu : Control, Figments.Direction.IDirectable
	{
		public async Task SelfDirect()
		{
			Globals.MusicPlayer.PlayMusic("res://Assets/Music/Figments.wav");
			await Globals.Overlay.FadeOutBlack();
			GetNode<AnimationPlayer>("MenuAnimator").Play("ShowMenu");
			await ToSignal(GetNode<AnimationPlayer>("MenuAnimator"), "animation_finished");
			GetNode<AnimationPlayer>("LabelAnimator").Play("PressAnyKeyFade");
			GetNode<FileDialog>("PCKSelector").Popup_();
		}
	}
}