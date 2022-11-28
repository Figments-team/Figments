using System;
using Godot;

namespace Figments.Game
{
    public interface IPickable<ItemType> where ItemType : Figments.Game.Item
    {
        public ItemType PickUp();
    }
}