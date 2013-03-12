using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using KataBowling.data;
using KataBowling.operations;
using NUnit.Framework;

namespace KataBowling.tests
{
    [TestFixture]
    public class test_Scorer
    {
        [Test]
        public void Calc_total_score()
        {
            var frames = new[]
                {
                    new Frame{Score=1},
                    new Frame{Score=2},
                    new Frame{}
                };

            var result = Scorer.Calc_total(frames);

            Assert.AreEqual(3, result);
        }


        [TestCase(null, null, null)]
        [TestCase(1, null, null)]
        [TestCase(1, 2, 3)]
        public void Regular_frames(int? roll1, int? roll2, int? expected)
        {
            var frames = new List<Frame> {new Frame {Roll1 = roll1, Roll2 = roll2}};

            var frame = frames.First();
            int? score = frame.Roll1.HasValue && frame.Roll2.HasValue ? frame.Roll1 + frame.Roll2 : null;

            Assert.AreEqual(expected, score);
        }

        [TestCase(5, 5, 4, 14)]
        [TestCase(5, 5, null, null)]
        public void Spare_frame(int roll11, int roll12, int? roll21, int? expected)
        {
            var frames = new List<Frame> { 
                                            new Frame { Roll1 = roll11, Roll2 = roll12 },
                                            new Frame{ Roll1 = roll21 }
                                         };

            int? score = frames[0].Roll1.HasValue && frames[0].Roll2.HasValue ? frames[0].Roll1 + frames[0].Roll2 : null;
            if (score.HasValue && score == 10 && frames.Count > 1)
                score = frames[1].Roll1.HasValue ? score + frames[1].Roll1 : null;

            Assert.AreEqual(expected, score);
        }

        [TestCase(null, null, 5, 3, null, 18)]
        [TestCase(10, null, 10, null, 3, 23)]
        [TestCase(10, null, null, null, null, null)]
        public void Strike_frame(int roll11, int roll12, int? roll21, int? roll22, int? roll31, int? expected)
        {
            var frames = new List<Frame> { 
                                            new Frame { Roll1 = roll11, Roll2 = roll12 },
                                            new Frame { Roll1 = roll21, Roll2 = roll22 },
                                            new Frame { Roll1 = roll31 }
                                            };

            int? score = 0;
            if (((int)frames[0].Roll1) == 10)
            {
                score = 10;
                score = frames[1].Roll1.HasValue ? score + frames[1].Roll1;

            }

            Assert.AreEqual(expected, score);
        }
    }
}
