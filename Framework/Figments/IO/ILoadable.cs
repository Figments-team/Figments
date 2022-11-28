using System;
using Godot;

namespace Figments.IO
{
    public interface ILoadable
    {
        public void Load(SavedObject savedObject);
    }
}