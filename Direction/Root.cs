using Godot;
using System;
using System.Threading.Tasks;

namespace Figments
{
	public class Root : Node
	{
		[Signal] delegate void load_finished(); //Signal declaration, refer to Godot docs for Signal infos
		private ResourceInteractiveLoader sceneLoader; //sceneLoader declaration, object that will load scenes

		public override void _Ready()
		{
			SetProcess(false); //_Process of Root gets disabled from the start
		}

		public override void _Process(float delta) //When enabled, it loads a chunk of data every frame
		{
			if(sceneLoader.Poll() == Error.FileEof) //When finished
			{
				SetProcess(false); //Disables _Process from running every frame
				EmitSignal("load_finished"); //Emits load_finished so whoever listens for it, will know
			}
		}

		public async Task LoadScene(string scenePath, bool alsoAdd = false) //Load a scene
		{
			sceneLoader = ResourceLoader.LoadInteractive(scenePath); //sceneLoader gets instanced, refer to Godot docs for LoadInteractive
			SetProcess(true); //_Process gets enabled
			await ToSignal(this, "load_finished"); //Waits for load_finished
			if(alsoAdd)
				AddLoadedScene();
		}

		public async Task WaitTime(int duration) //Wait some amount of seconds
		{
			await ToSignal(GetTree().CreateTimer(duration), "timeout"); //Refer to Godot docs for GetTree().CreateTimer()
		}

		public void AddLoadedScene() //Add loaded scene to the tree
		{
			if(sceneLoader.GetResource() != null)
			{
				PackedScene loadedScene = (PackedScene)sceneLoader.GetResource();
				this.AddChild(loadedScene.Instance());
			}
		}
		
		public int IndexOf(string sceneName) //Search a scene by name and returns its index
		{
			for(int n = 0; n < GetChildCount(); n++)
				if(GetChild(n).Name == sceneName)
					return n;
			return -1;
		}

		public void RemoveScene(string sceneName) //Remove a scene by name from the tree
		{
			if(sceneName != null)
				foreach(Node child in GetChildren())
					if(child.Name == sceneName)
						child.QueueFree(); //Refer to Godot docs for QueueFree
		}

		public void RemoveAll() //Remove all scenes from the tree
		{
			foreach(Node child in GetChildren())
				child.QueueFree(); //Refer to Godot docs for QueueFree
		}
	}
}