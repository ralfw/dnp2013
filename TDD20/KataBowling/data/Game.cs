using System.Collections.Generic;

namespace KataBowling.data
{
    struct Game
    {
        public IEnumerable<Frame> Frames;
        public int Score;
    }
}