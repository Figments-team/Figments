using Godot;
using System;

namespace Figments
{
    public class SceneSelector : FileDialog
    {
        public override void _Ready()
        {
            Connect("file_selected", this, "OnFileSelected");
        }

        public void OnFileSelected(string path)
        {
            Globals.Root.RemoveAll();
            Globals.Root.LoadScene(path);
        }
    }
}