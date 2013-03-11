using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBowling
{
    class Frames
    {
        private List<Frame> _frames; 

        public Game Clear()
        {
            return new Game {Frames = new Frame[0], Score = 0};
        }


        public Game Extend_game(Game game)
        {
            _frames = new List<Frame>(game.Frames) {new Frame()};

            return new Game {Frames = _frames, Score = game.Score};
        } 
    }
}
