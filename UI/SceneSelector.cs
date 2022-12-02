using Godot;
using System.Threading.Tasks;

namespace Figments
{
    public class SceneSelector : FileDialog
    {
        public override void _Ready()
        {
            Connect("file_selected", this, "OnFileSelected");
        }

        string selectedPath;

        public void OnFileSelected(string path)
        {
            selectedPath = path;
            Globals.Director.Direct(SwitchToSelected());
        }

        public async Task SwitchToSelected()
        {
            Globals.Root.RemoveAll();
            await Globals.Root.LoadScene(selectedPath, true);
        }
    }
}