using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace json.uiproxy
{
    internal partial class JsonPortalDlg : Form
    {
        public JsonPortalDlg()
        {
            InitializeComponent();
        }


        private void cboJsonTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            Template_ausgewählt(cboJsonTemplates.SelectedIndex);
        }

        private void btnSenden_Click(object sender, EventArgs e)
        {
            JsonAnzeigen(txtJsonInput.Text, "Input:");
            JsonInput(txtJsonInput.Text);
        }


        public void Templates(string[] templateNames)
        {
            cboJsonTemplates.Items.Clear();
            cboJsonTemplates.Items.AddRange(templateNames);
        }


        public void Template_anzeigen(string jsonTemplate)
        {
            txtJsonInput.Text = jsonTemplate;
        }


        public void JsonOutput_anzeigen(string jsonOutput)
        {
            JsonAnzeigen(jsonOutput, "Output:");
        }

        private void JsonAnzeigen(string jsonOutput, string überschrift)
        {
            using (var sr = new StringReader(jsonOutput))
            {
                var i = 0;
                lvJsonIO.Items.Insert(i++, überschrift).BackColor = Color.LightGray;

                string line;
                while ((line = sr.ReadLine()) != null)
                    lvJsonIO.Items.Insert(i++, line);
            }
        }


        public Action<int> Template_ausgewählt;
        public Action<string> JsonInput;

    }
}
