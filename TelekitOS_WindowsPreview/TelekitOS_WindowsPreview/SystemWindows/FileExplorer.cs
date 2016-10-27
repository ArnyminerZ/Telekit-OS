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
    public partial class FileExplorer : Form
    {
        Form1 desktopForm;

        public FileExplorer(Form1 desktopForm)
        {
            InitializeComponent();
            this.desktopForm = desktopForm;
        }

        private void FileExplorer_Load(object sender, EventArgs e)
        {
            
        }
    }
}