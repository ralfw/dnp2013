using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataBowling.data;
using KataBowling.operations;
using NUnit.Framework;

namespace KataBowling.tests
{
    [TestFixture]
    public class test_Integration
    {
        [TestCase(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 11, 300)]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, 11, 150)]
        [TestCase(new[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, }, 10, 90)]
        public void Register_roll(int[] rolls, int nFrames, int totalScore)
        {
            var sut = new Integration(new Frames(), new Scorer());

            var results = new List<Game>();
            sut.Result += results.Add;

            rolls.ToList()
                    .ForEach(sut.Register_roll);

            Assert.AreEqual(nFrames, results.Last().Frames.Count());
            Assert.AreEqual(totalScore, results.Last().Score);

            Assert.IsTrue(results[rolls.Length-1].Finished);
            Assert.IsFalse(results[rolls.Length-2].Finished);
        }
    }
}
