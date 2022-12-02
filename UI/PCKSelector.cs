using Godot;
using System;

namespace Figments
{
    public class PCKSelector : FileDialog
    {
        public override void _Ready()
        {
            Connect("file_selected", this, "OnFileSelected");
        }

        public void OnFileSelected(string path)
        {
            GD.Print(path);
            GD.Print(ProjectSettings.LoadResourcePack(path));
            this.Visible = false;
            GetNode<SceneSelector>("../SceneSelector").Visible = true;
        }
    }
}