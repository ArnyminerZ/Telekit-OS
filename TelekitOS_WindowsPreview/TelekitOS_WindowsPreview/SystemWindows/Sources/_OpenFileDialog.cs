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

namespace TelekitOS_WindowsPreview.Resources
{
    public partial class _OpenFileDialog : Form
    {
        public string InitialDirectory = "C:\\\\";

        public _OpenFileDialog()
        {
            InitializeComponent();
        }

        private void _OpenFileDialog_Load(object sender, EventArgs e)
        {
            panel1.Width = Width;

            pictureBox1.Width = panel1.Height;
            pictureBox1.Height = pictureBox1.Width;
            pictureBox1.Location = new Point(Width - pictureBox1.Width, 0);

            pictureBox2.Width = pictureBox1.Width;
            pictureBox2.Height = pictureBox1.Width;
            pictureBox2.Location = new Point(Width - (pictureBox1.Width * 2), 0);

            textBox1.Width = Width;
            textBox1.Text = InitialDirectory;
            textBox1.Location = new Point(0, panel1.Height);

            string[] files = Directory.GetFiles(InitialDirectory);
            foreach (string file in files)
            {
                listBox1.Items.Add(file);
            }
            listBox1.Location = new Point(0, panel1.Height + textBox1.Height);
            listBox1.Width = Width;
            listBox1.Height = Height - (panel1.Height + textBox1.Height);

            TopMost = true;
        }
    }
}
