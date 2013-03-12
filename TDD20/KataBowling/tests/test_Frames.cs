using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataBowling.data;
using KataBowling.operations;
using NUnit.Framework;
using equalidator;

namespace KataBowling.tests
{
    [TestFixture]
    public class test_Frames
    {
        [Test]
        public void Register_first_roll_in_frame()
        {
            var sut = new Frames(new List<Frame>{new Frame()});

            var result = sut.RegisterRoll(4);

            Equalidator.AreEqual(result.First(), new Frame{Roll1=4});
        }


        [Test]
        public void Register_second_roll_in_frame()
        {
            var sut = new Frames(new List<Frame> {new Frame {Roll1 = 4}});

            var result = sut.RegisterRoll(3);

            Equalidator.AreEqual(result.First(), new Frame{Roll1=4, Roll2=3});
        }


        [Test]
        public void Extend_game_while_less_than_10_frames()
        {
            var frames = new List<Frame>();
            for(var i=1; i<10; i++)
                frames.Add(new Frame());

            var result = Frames.Extend_frames(frames);

            Assert.AreEqual(frames.Count+1, result.Count());
        }

        [Test]
        public void Extend_game_for_spare_in_10th_frame()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame{Roll1=6, Roll2=4, Score=null});

            var result = Frames.Extend_frames(frames);

            Assert.AreEqual(11, result.Count());
        }

        [Test]
        public void Extend_game_for_strike_in_10th_frame()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1 =10, Roll2 = null, Score = null });

            var result = Frames.Extend_frames(frames);

            Assert.AreEqual(11, result.Count());
        }


        [Test]
        public void No_extension_if_10th_frame_scores_less_than_10()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1 = 3, Roll2 = 6, Score = 9 });

            var result = Frames.Extend_frames(frames);

            Assert.AreEqual(10, result.Count());
        }
    }
}
