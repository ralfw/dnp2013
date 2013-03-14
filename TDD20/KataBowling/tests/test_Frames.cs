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
        public void Mixin_scores()
        {
            var frames = new[] { new Frame {Roll1 = 1, Roll2 = 2}, 
                                 new Frame {Roll1 = 11, Roll2 = 12}};
            var scores = new int[] {100, 200};

            var sut = new Frames();
            var result = sut.Mixin_scores(frames, scores);

            Equalidator.AreEqual(result, 
                                    new[] { new Frame { Roll1 = 1, Roll2 = 2, Score=100 }, 
                                            new Frame { Roll1 = 11, Roll2 = 12, Score=200 } },
                                    true);
        }


        [Test]
        public void No_end_of_game_with_less_than_10_frames()
        {
            var frames = new List<Frame>();
            for(var i=1;i<10;i++)
                frames.Add(new Frame());

            var result = new Frames().Check_for_end_of_game(new Game{Frames=frames});

            Assert.IsFalse(result);
        }

        [Test]
        public void End_of_game_if_10th_frame_is_regular()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1=3, Roll2=6, Score=9 });

            var result = new Frames().Check_for_end_of_game(new Game { Frames = frames });

            Assert.IsTrue(result);   
        }

        [Test]
        public void No_end_of_game_if_10th_frame_is_incomplete()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1 = 3, Score = 3 });

            var result = new Frames().Check_for_end_of_game(new Game { Frames = frames });

            Assert.IsFalse(result);   
        }


        [Test]
        public void End_of_game_if_spare_followed_by_one_more_roll()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1 = 4, Roll2 = 6, Score = 13});
            frames.Add(new Frame { Roll1 = 3 });

            var result = new Frames().Check_for_end_of_game(new Game { Frames = frames });

            Assert.IsTrue(result); 
        }

        [Test]
        public void End_of_game_if_strike_followed_by_two_more_rolls()
        {
            var frames = new List<Frame>();
            for (var i = 1; i < 10; i++)
                frames.Add(new Frame());
            frames.Add(new Frame { Roll1 = 10, Score = 18 });
            frames.Add(new Frame { Roll1 = 3, Roll2 = 5});

            var result = new Frames().Check_for_end_of_game(new Game { Frames = frames });

            Assert.IsTrue(result);
        }


        [Test]
        public void Single_roll_in_frame()
        {
            var rolls = new[] {1};

            var result = new Frames().Frame_rolls(rolls);

            Equalidator.AreEqual(result, new[]{new Frame{Roll1=1}}, true);
        }


        [Test]
        public void Two_rolls_in_frame()
        {
            var rolls = new[] { 1, 2 };

            var result = new Frames().Frame_rolls(rolls);

            Equalidator.AreEqual(result, new[] { new Frame { Roll1 = 1, Roll2 = 2 } }, true);
        }

        [Test]
        public void Two_frames()
        {
            var rolls = new[] { 1, 2, 3 };

            var result = new Frames().Frame_rolls(rolls);

            Equalidator.AreEqual(result, new[] { new Frame { Roll1 = 1, Roll2 = 2 }, 
                                                    new Frame{Roll1=3} }, 
                                    true);
        }

        [Test]
        public void Strike_frame()
        {
            var rolls = new[] { 10, 2, 3 };

            var result = new Frames().Frame_rolls(rolls);

            Equalidator.AreEqual(result, new[] { new Frame { Roll1 = 10 }, 
                                            new Frame{ Roll1 = 2, Roll2 = 3} },
                                    true);
        }


        [Test]
        public void No_new_frame_even_if_strike_if_11th_frame()
        {
            var rolls = new[] { 10,10,10,10,10, 10,10,10,10,10, 10,10 };

            var result = new Frames().Frame_rolls(rolls);

            Assert.AreEqual(11, result.Count());
        }
    }
}
