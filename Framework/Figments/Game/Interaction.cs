using System;

namespace Figments.Game
{
    public class Interaction
    {
        private Tuple<INameable, string>[] _InteractionText;
        public Tuple<INameable, string>[] Content { get => _InteractionText; }
        public Interaction(int length)
        {
            _InteractionText = new Tuple<INameable, string>[length];
        }
    }
}