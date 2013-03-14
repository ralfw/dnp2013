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
        private readonly Scorer _scorer;

        public Integration(Frames frames, Scorer scorer)
        {
            _frames = frames;
            _scorer = scorer;
        }


        public void Start()
        {
            New_game();
        }


        public void New_game()
        {
            var game = _frames.Clear();
            Result(game);
        }


        public void Register_roll(int pinsRolled)
        {
            var rolls = _frames.Insert_roll(pinsRolled);
            Result(Build_game(rolls));
        }

        private Game Build_game(IEnumerable<int> rolls)
        {
            var frames = Build_frames(rolls);

            var game = new Game
            {
                Frames = frames,
                Score = _scorer.Calc_total_score(frames)
            };

            game.Finished = _frames.Check_for_end_of_game(game);

            return game;
        }

        private IEnumerable<Frame> Build_frames(IEnumerable<int> rolls)
        {
            var frames = _frames.Frame_rolls(rolls);
            var scores = _scorer.Calc_frame_scores(rolls.ToArray());
            return _frames.Mixin_scores(frames, scores);
        }


        public event Action<Game> Result;
    }
    }
