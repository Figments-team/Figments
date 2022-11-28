using Godot;
using System;
using System.Threading.Tasks;

namespace Figments
{
	public class Overlay : CanvasLayer
	{
		public async Task ShowLoader() //Shows loader in the top right corner
		{
			AnimationPlayer player = GetNode<AnimationPlayer>("LoadingAnimator");
			GetNode<CanvasItem>("Loading").Visible = true;
			player.Play("ShowOrHideLoader", -1, 1, false);
			await ToSignal(player, "animation_finished");
		}

		public async Task HideLoader() //Hides loader in the top right corner
		{
			AnimationPlayer player = GetNode<AnimationPlayer>("LoadingAnimator");
			player.Play("ShowOrHideLoader", -1, -1, true);
			await ToSignal(player, "animation_finished");
			GetNode<CanvasItem>("Loading").Visible = false;
		}

		public void CutInBlack() //Instantly cuts to black
		{
			GetNode<CanvasItem>("BlackPanel").Modulate = new Godot.Color(1, 1, 1, 1);
		}

		public void CutOutBlack() //Instantly cuts from black
		{
			GetNode<CanvasItem>("BlackPanel").Modulate = new Godot.Color(1, 1, 1, 0);
		}

		public async Task FadeInBlack(int duration = 1) //Fade to black
		{
			double speed = 1.0 / duration;
			GetNode<CanvasItem>("BlackPanel").Visible = true;
			GetNode<AnimationPlayer>("PanelAnimator").Play("FadeInBlack", -1, (float)speed);
			await ToSignal(GetNode<AnimationPlayer>("PanelAnimator"), "animation_finished");
		}

		public async Task FadeOutBlack(int duration = 1) //Fade from black
		{
			double speed = 1.0 / duration;
			GetNode<AnimationPlayer>("PanelAnimator").Play("FadeOutBlack", -1, (float)speed);
			await ToSignal(GetNode<AnimationPlayer>("PanelAnimator"), "animation_finished");
			GetNode<CanvasItem>("BlackPanel").Visible = false;
		}
	}
}