﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KataBowling.data;

namespace KataBowling.operations
{
    class Mappings
    {
        public void Map(Game game)
        {
            var vm = new ViewModel {
                                        GameScore = game.Score,
                                        GameFinished = game.Finished
                                   };

            var vmFrames = new List<ListViewItem>();
            var i = game.Frames.Count();
            foreach (var f in game.Frames.Reverse())
            {
                var vmFrame = new ListViewItem(i.ToString());
                vmFrame.SubItems.Add(f.Roll1 == null ? "" : f.Roll1 == 10 ? "X" : f.Roll1.ToString());
                vmFrame.SubItems.Add(f.Roll2 == null ? "" : f.Roll2 == 10 ? "X" : (f.Roll1 + f.Roll2) == 10 ? "/" : f.Roll2.ToString());
                vmFrame.SubItems.Add(f.Score == null ? "" : f.Score == 0 ? "" : f.Score.ToString());

                vmFrames.Add(vmFrame);
                i--;
            }
            vm.Frames = vmFrames;

            Result(vm);
        }

        public event Action<ViewModel> Result;
    }
}
