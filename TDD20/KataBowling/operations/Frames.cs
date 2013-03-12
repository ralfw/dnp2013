using System;
using System.Collections.Generic;
using System.Linq;
using KataBowling.data;

namespace KataBowling.operations
{
    class Frames
    {
        private List<int> _rolls; 


        public Frames()
        {
            Clear();
        }


        public Game Clear()
        {
            _rolls = new List<int>();
            return new Game { Frames = new[]{new Frame()}, Score = 0 };
        }


        public IEnumerable<int> Register_roll(int pins)
        {
            _rolls.Add(pins);
            return _rolls;
        } 


        public IEnumerable<Frame> Frame_rolls(IEnumerable<int> rolls)
        {
            var frames = new List<Frame>();

            var frame = new Frame();
            foreach (var roll in rolls)
            {
                if (frame.Roll1.HasValue)
                {
                    frame.Roll2 = roll;
                    frames.Add(frame);
                    frame = new Frame();
                }
                else
                {
                    frame.Roll1 = roll;
                    if (roll == 10)
                    {
                        frames.Add(frame);
                        frame = new Frame();
                    }
                }
            }
            if (frame.Roll1.HasValue) frames.Add(frame);

            return frames;
        }


        public IEnumerable<Frame> Mixin_scores(IEnumerable<Frame> frames, IEnumerable<int> scores)
        {
            return frames.Zip(scores, (f, s) => {
                                                    f.Score = s;
                                                    return f;
                                                });  
        } 


        public bool Check_for_end_of_game(Game game)
        {
            var frames = game.Frames.ToArray();

            if (frames.Count() == 10 &&
                frames[9].Roll2.HasValue &&
                frames[9].Score < 10) return true;

            if (frames.Count() == 11 &&
                frames[10].Roll1.HasValue) return true;

            if (frames.Count() == 11 &&
                frames[9].Roll1 == 10 &&
                frames[10].Roll1.HasValue && frames[10].Roll2.HasValue) return true;

            return false;
        }
    }
}
