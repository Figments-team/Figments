using System;
using Godot;

namespace Figments.Direction
{
	public class Entry
	{
        delegate void Method();
		Method WhatToDo;
		Entry[] Dependencies;
		public Entry()
		{
			
		}
	}
}