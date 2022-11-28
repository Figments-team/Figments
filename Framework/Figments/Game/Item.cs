using System;
using Godot;

namespace Figments.Game
{
    public class Item : INameable
    {
        private int _ID;
        public int ID => _ID;
        private string _Name;
        public string Name => _Name;
    }
}