using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelekitOS_WindowsPreview.SystemWindows.Sources
{
    public partial class MaterialMessageBox : MaterialForm
    {
        private string title;
        private bool[] showBoxes;

        [Description ("Title = Message title \n" + 
                      "ShowBoxes[2] = { showMinimizeBox, showMaximizeBox }")]
        public MaterialMessageBox(string title, bool[] showBoxes)
        {
            InitializeComponent();
            this.title = title;
            this.showBoxes = showBoxes;

            Text = title;
            MinimizeBox = showBoxes[0];
            MaximizeBox = showBoxes[1];
        }

        private void MaterialMessageBox_Load(object sender, EventArgs e)
        {

        }
    }
}
