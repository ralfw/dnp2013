using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using equalidator;

namespace KataBowling.tests
{
    [TestFixture]
    public class test_Mappings
    {
        [Test]
        public void Pass_through_score()
        {
            var game = new Game{Score = 42};

            var sut = new Mappings();
            var result = sut.Map(game);

            Assert.AreEqual(42, result.GameScore);
        }


        [Test]
        public void Fill_frames()
        {
            var game = new Game
            {
                Frames = new List<Frame>
                {
                    new Frame{Roll1 = 6, Roll2 = 0},
                    new Frame{Roll1 = 0, Roll2 = 0, Score = 0},
                    new Frame{Roll1 = 10, Roll2 = 0, Score = 99},
                    new Frame{Roll1 = 5, Roll2 = 5, Score = 88},
                    new Frame{Roll1 = 3, Roll2 = 6, Score = 77}
                }
            };

            var sut = new Mappings();
            var result = sut.Map(game);

            var lvframes = new List<ListViewItem>();
            var lvi = new ListViewItem("1");
            lvi.SubItems.Add("3");
            lvi.SubItems.Add("6");
            lvi.SubItems.Add("77");
            lvframes.Add(lvi);
            lvi = new ListViewItem("2");
            lvi.SubItems.Add("5");
            lvi.SubItems.Add("/");
            lvi.SubItems.Add("88");
            lvframes.Add(lvi);
            lvi = new ListViewItem("3");
            lvi.SubItems.Add("X");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("99");
            lvframes.Add(lvi);
            lvi = new ListViewItem("4");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvframes.Add(lvi);
            lvi = new ListViewItem("5");
            lvi.SubItems.Add("6");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvframes.Add(lvi);

            Equalidator.AreEqual(result.Frames, lvframes, true);
        }


        [Test]
        public void Check_game_finished()
        {
            var game = new Game
            {
                Frames = new List<Frame>
                {
                    new Frame{Score = 77}
                }
            };

            var sut = new Mappings();
            var result = sut.Map(game);

            Assert.IsTrue(result.GameFinished);
        }
    }
}
