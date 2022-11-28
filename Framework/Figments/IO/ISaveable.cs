using System;
using Godot;

namespace Figments.IO
{
    public interface ISaveable
    {
        public SavedObject Save();
    }
}