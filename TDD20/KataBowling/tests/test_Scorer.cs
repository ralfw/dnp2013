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

            var sut = new Scorer();
            var result = sut.Calc_total(frames);

            Assert.AreEqual(3, result);
        }


        [TestCase(new[] { 1 }, Result = new[] { 1 })]
        [TestCase(new[] { 1, 2 }, Result = new[] { 3 })]
        [TestCase(new[] { 4, 6, 10 }, Result = new[] { 20, 10 })]
        [TestCase(new[] { 4, 6 }, Result = new[] { 10 })]
        [TestCase(new[] { 10, 1, 2 }, Result = new[] { 13, 3 })]
        [TestCase(new[] { 10, 1 }, Result = new[] { 11, 1 })]
        [TestCase(new[] { 10 }, Result = new[] { 10 })]
        public int[] Score_regular_frame(int[] rolls)
        {
            return new Scorer().Score_rolls(rolls).ToArray();
        }
    }
}
