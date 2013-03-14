using System;
using System.Collections.Generic;
using System.Linq;
using KataBowling.data;

namespace KataBowling.operations
{
    public class Scorer
    {
        public int Calc_total_score(IEnumerable<Frame> frames)
        {
            return frames.Where(f => f.Score.HasValue)
                         .Select(f => f.Score.Value)
                         .Sum();
        }


        public IEnumerable<int> Calc_frame_scores(int[] rolls)
        {
            var frameScores = new List<int>();
            var i = 0;
            while (i < rolls.Length)
            {
                var score = rolls[i];
                score = Always_add_next_roll(rolls, i, score);
                score = Add_roll_after_next_for_strike_or_spare(rolls, i, score);

                frameScores.Add(score);

                i += Jump_to_first_roll_of_next_frame(rolls, i);
            }

            Adjust_score_for_frame_11(frameScores);

            return frameScores;
        }


        private static int Always_add_next_roll(int[] rolls, int i, int score)
        {
            if ((i + 1) < rolls.Length)
                score += rolls[i + 1];
            return score;
        }

        private static int Add_roll_after_next_for_strike_or_spare(int[] rolls, int i, int score)
        {
            if ((score == 10 || rolls[i] == 10) && (i + 2 < rolls.Length))
                score += rolls[i + 2];
            return score;
        }

        private static int Jump_to_first_roll_of_next_frame(int[] rolls, int i)
        {
            return rolls[i] == 10 ? 1 : 2;
        }

        private static void Adjust_score_for_frame_11(List<int> frameScores)
        {
            if (frameScores.Count <= 10) return;

            frameScores.RemoveRange(10, frameScores.Count() - 10);
            frameScores.Add(0);
        }
    }
}