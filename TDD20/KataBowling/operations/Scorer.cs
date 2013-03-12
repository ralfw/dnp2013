using System.Collections.Generic;
using System.Linq;
using KataBowling.data;

namespace KataBowling.operations
{
    public class Scorer
    {
        public static int Calc_total(IEnumerable<Frame> frames)
        {
            return frames.Where(f => f.Score.HasValue)
                         .Select(f => f.Score.Value)
                         .Sum();
        }
    }
}