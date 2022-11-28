using Godot;
using System;

namespace Figments
{
	public class MusicPlayer : AudioStreamPlayer
	{
		public void PlayMusic(string path) //Play music
		{
			Stream = (AudioStream)GD.Load(path);
			Play();
		}
	}
}