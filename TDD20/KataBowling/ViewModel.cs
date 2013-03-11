using System.Collections.Generic;
using System.Windows.Forms;

namespace KataBowling
{
    public struct ViewModel
    {
        public IEnumerable<ListViewItem> Frames;
        public int GameScore;
        public bool GameFinished;
    }
}