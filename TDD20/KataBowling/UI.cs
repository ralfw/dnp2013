using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KataBowling
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            On_Pins(int.Parse(txtPinsRolled.Text));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            On_Clear();
        }
        
        
        public void Display(ViewModel vm)
        {
            lvFrames.Items.Clear();
            lvFrames.Items.AddRange(vm.Frames.ToArray());

            lblGameScore.Text = vm.GameScore.ToString();

            btnSubmit.Enabled = !vm.GameFinished;

            txtPinsRolled.Text = "0";
            txtPinsRolled.Focus();
            txtPinsRolled.SelectAll();
        }

        public event Action On_Clear;
        public event Action<int> On_Pins;
    }
}
