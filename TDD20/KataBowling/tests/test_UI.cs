using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace KataBowling.tests
{
    [TestFixture]
    public class test_UI
    {
        [Test, Explicit]
        public void Display_view_model()
        {
            var vm = new ViewModel
                {
                    GameScore = 12,
                    GameFinished = true
                };

            var frames = new List<ListViewItem>();
            for (var i = 12; i > 0; i--)
            {
                var lvm = new ListViewItem(i.ToString());
                lvm.SubItems.Add(i.ToString());
                lvm.SubItems.Add((i+1).ToString());
                lvm.SubItems.Add((i + i + 1).ToString());
                frames.Add(lvm);
            }
            vm.Frames = frames;

            var sut = new UI();

            sut.Display(vm);

            sut.ShowDialog();
        }


        [Test, Explicit]
        public void Receive_events()
        {
            var ui = new UI();
            ui.On_Clear += () => MessageBox.Show("clearing requested");
            ui.On_Pins += pins => MessageBox.Show(pins.ToString() + " pins submitted");

            ui.ShowDialog();
        }
    }
}
