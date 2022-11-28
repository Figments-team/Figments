using Godot;
using Figments.Direction;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Figments
{
	public class Director : Node
	{
		[Signal] delegate void director_state_changed(State oldState, State newState); //Signal declaration, refer to Godot docs for Signal infos
		private State _Status = State.Unknown;
		public State Status
		{
			get
			{
				return _Status;
			}
			set
			{
				State oldState = _Status;
				_Status = value;
				EmitSignal("director_state_changed", oldState, _Status);
			}
		}
		private Task currentTask;
		private Queue<Task> tasks = new Queue<Task>();

		public override async void _Ready() //First thing executed in the game
		{
			Status = State.Starting;

			// Assign Globals, these are static variables accessible from everywhere
			Globals.Director = GetNode<Director>("/root/Director");
			Globals.Overlay = GetNode<Overlay>("/root/Overlay");
			Globals.MusicPlayer = GetNode<MusicPlayer>("/root/MusicPlayer");
			Globals.Root = GetNode<Root>("/root/Root");

			await ToSignal(Globals.Root, "ready"); // _Ready of Director is called before Root is ready, so we wait for it

			Status = State.Idle;

			Direct(Opening().Wait);
		}

		public override void _Process(float delta)
		{
			GD.Print(Status);
			switch(Status)
			{
				case State.Idle:
					if(tasks.Count > 0)
					{
						//if(tasks.Peek().Status
						currentTask = tasks.Dequeue();
						currentTask.Start();
						Status = State.Working;
					}
					break;
				case State.Waiting: case State.Working:
					if(currentTask.IsCompleted)
					{
						currentTask.Dispose();
						currentTask = null;
						Status = State.Idle;
					}
					break;
			}
		}

		public void Direct(Action action)
		{
			tasks.Enqueue(new Task(action));
		}

		public Task SelfDirect(string nodeName)
		{
			IDirectable scene = (IDirectable)Globals.Root.GetNode(nodeName);
			return scene.SelfDirect(); //Waits for SelfDirect 
		}

		public Task UseLoader(Task taskToLoad) //Displays the loader along with task
		{
			Globals.Overlay.ShowLoader();
			return taskToLoad.ContinueWith((task) => Globals.Overlay.HideLoader()); //Hide loader when task completes
		}

		private async Task Opening()
		{
			await Globals.Root.LoadScene("res://UI/Splash.tscn", true); //Splash scene gets loaded (in RAM, ready for use) and added
			await Globals.Overlay.FadeOutBlack(); //Splash is displayed
			Task loadMainMenu = UseLoader(Globals.Root.LoadScene("res://UI/MainMenu.tscn", true)); //Main menu starts loading and shows loader
			await SelfDirect("Splash"); //Do whatever Splash is supposed to do
			await Globals.Overlay.FadeInBlack(); //Splash is hidden
			Globals.Root.RemoveScene("Splash"); //Splash gets removed from the tree
			await Globals.Root.WaitTime(1); //Wait 1 second
			await loadMainMenu; //Wait for MainMenu loading if still isn't done
			Globals.Root.AddLoadedScene(); //MainMenu gets added to the tree
			Globals.MusicPlayer.PlayMusic("res://Assets/Music/Figments.wav"); //Menu music starts playing
			await Globals.Overlay.FadeOutBlack(); //MainMenu is displayed
			await SelfDirect("MainMenu"); //Do whatever MainMenu is supposed to do*/
		}
	}
}