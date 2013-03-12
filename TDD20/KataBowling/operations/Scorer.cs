using System;
using System.Collections.Generic;
using System.Linq;
using KataBowling.data;

namespace KataBowling.operations
{
    public class Scorer
    {
        public int Calc_total(IEnumerable<Frame> frames)
        {
            return frames.Where(f => f.Score.HasValue)
                         .Select(f => f.Score.Value)
                         .Sum();
        }


        public IEnumerable<int> Score_rolls(int[] rolls)
        {
            var result = new List<int>();
            var i = 0;
            while (i < rolls.Length)
            {
                var score = rolls[i];

                if ((i + 1) < rolls.Length)
                    score += rolls[i + 1];

                if ((score == 10 || rolls[i] == 10) && (i + 2 < rolls.Length))
                    score += rolls[i + 2];

                result.Add(score);

                i += rolls[i] == 10 ? 1 : 2;
            }
            return result.ToArray();
        } 
    }
}