using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelekitOS_WindowsPreview.SystemWindows
{
    public partial class Settings : Form
    {
        private Form1 desktopForm;

        public Settings(Form1 desktopForm)
        {
            this.desktopForm = desktopForm;
            InitializeComponent();

            Cursor = new Cursor(Form1.mainCursorSource);
            UseWaitCursor = true;
            Cursor.Current = new Cursor(Form1.loadingCursorSource);
            UseWaitCursor = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
