using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KataBowling
{
    class Mappings
    {
        public ViewModel Map(Game game)
        {
            var vm = new ViewModel {
                                        GameScore = game.Score, 
                                        GameFinished = game.Frames.Last().Score.HasValue
                                   };

            var vmFrames = new List<ListViewItem>();
            var i = 1;
            foreach (var f in game.Frames.Reverse())
            {
                var vmFrame = new ListViewItem(i.ToString());
                vmFrame.SubItems.Add(f.Roll1 == 0 ? "" : f.Roll1 == 10 ? "X" : f.Roll1.ToString());
                vmFrame.SubItems.Add(f.Roll2 == 0 ? "" : (f.Roll1 + f.Roll2) == 10 ? "/" : f.Roll2.ToString());
                vmFrame.SubItems.Add(f.Score == null ? "" : f.Score == 0 ? "" : f.Score.ToString());

                vmFrames.Add(vmFrame);
                i++;
            }
            vm.Frames = vmFrames;

            return vm;
        }
    }
}
