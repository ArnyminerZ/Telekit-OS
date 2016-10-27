using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelekitOS_WindowsPreview.Administers.Applicacion;

namespace TelekitOS_WindowsPreview.Administers.Desktop
{
    public partial class DesktopItem : UserControl
    {
        public DesktopItem()
        {
            InitializeComponent();
        }

        private void DesktopItem_Load(object sender, EventArgs e)
        {

        }
        private void DesktopItem_Resize(object sender, EventArgs e)
        {
            pictureBox1.Height = Height - 36;
            Width = pictureBox1.Height;
        }

        private new void Update()
        {
            pictureBox1.Image = BackgroundImage;
            BackgroundImage = null;

            pictureBox1.Width = pictureBox1.Height;
        }
        
        [Description("The desktop icon icon"), Category("Data")]
        public Image Icon
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }
        [Description("The desktop icon text"), Category("Data")]
        public override string Text
        {
            get { return materialFlatButton1.Text; }
            set { materialFlatButton1.Text = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
