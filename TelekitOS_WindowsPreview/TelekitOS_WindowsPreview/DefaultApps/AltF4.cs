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

namespace TelekitOS_WindowsPreview
{
    public partial class AltF4 : MaterialForm
    {
        Form1 form1;

        public AltF4(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();

            Cursor = new Cursor(Form1.mainCursorSource);
            UseWaitCursor = true;
            Cursor.Current = new Cursor(Form1.loadingCursorSource);
            UseWaitCursor = false;
        }

        private void AltF4_Load(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            form1.FormClosing -= new FormClosingEventHandler(form1.Form1_FormClosing);
            form1.Close();
            Close();
        }
    }
}
