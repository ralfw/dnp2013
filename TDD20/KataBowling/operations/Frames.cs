using System.Collections.Generic;
using System.Linq;
using KataBowling.data;

namespace KataBowling.operations
{
    class Frames
    {
        private List<Frame> _frames; 

        public Frames() : this(new List<Frame>()) {}
        internal Frames(List<Frame> frames)
        {
            _frames = frames;
        }


        public Game Clear()
        {
            return new Game {Frames = new Frame[0], Score = 0};
        }


        public Game Extend_game(Game game)
        {
            _frames = new List<Frame>(game.Frames) {new Frame()};
            return new Game {Frames = Extend_frames(game.Frames), Score = game.Score};
        }


        internal static IEnumerable<Frame> Extend_frames(IEnumerable<Frame> frames)
        {
            var result = new List<Frame>(frames);
            if (result.Count < 10)
                result.Add(new Frame());
            else if (result.Count() == 10)
                    if (Strike_in_10th_frame(result))
                        result.Add(new Frame());
                    else if (Spare_in_10th_frame(result))
                        result.Add(new Frame());
            return result;
        }

        private static bool Strike_in_10th_frame(List<Frame> result)
        {
            return result.Last().Roll1.HasValue &&
                   result.Last().Roll1 == 10;
        }

        private static bool Spare_in_10th_frame(List<Frame> result)
        {
            return result.Last().Roll1.HasValue && result.Last().Roll2.HasValue &&
                   result.Last().Roll1 + result.Last().Roll2 == 10;
        }


        public IEnumerable<Frame> RegisterRoll(int pins)
        {
            var frame = _frames.Last();

            if (frame.Roll1.HasValue)
                frame.Roll2 = pins;
            else
                frame.Roll1 = pins;

            return _frames;
        }
    }
}
