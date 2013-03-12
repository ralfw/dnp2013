using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataBowling.data;
using KataBowling.operations;

namespace KataBowling
{
    class Integration
    {
        private readonly Frames _frames;

        public Integration(Frames frames)
        {
            _frames = frames;
        }


        public void Start(Action<Game> continueWith)
        {
            var game = _frames.Clear();
            continueWith(_frames.Extend_game(game));
        }
    }
}
