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


        public void Throw(int pinsRolled)
        {
            var rolls = _frames.Register_roll(pinsRolled);

            var frames = _frames.Frame_rolls(rolls);
            var scores = _scorer.Score_rolls(rolls.ToArray());

            frames = _frames.Mixin_scores(frames, scores);

            var game = new Game
                {
                    Frames = frames,
                    Score = _scorer.Calc_total(frames)
                };

            game.Finished = _frames.Check_for_end_of_game(game);

            Result(game);
        }


        public event Action<Game> Result;
    }
}
