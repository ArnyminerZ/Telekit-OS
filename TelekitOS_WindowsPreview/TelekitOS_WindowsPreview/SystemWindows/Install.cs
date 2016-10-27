using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TelekitOS_WindowsPreview.Resources;

namespace TelekitOS_WindowsPreview
{
    public partial class Install : Form
    {
        private Form1 desktopForm;

        public Install(Form1 desktopForm)
        {
            this.desktopForm = desktopForm;
            InitializeComponent();

            desktopForm.checkAndDownloadSystemFiles();

            Cursor = new Cursor(desktopForm.mainCursorSource);
            UseWaitCursor = true;
            Cursor.Current = new Cursor(desktopForm.loadingCursorSource);
            UseWaitCursor = false;
            Cursor.Position = new Point(textBox1.Location.X + 3, textBox1.Location.Y + 3);
            Cursor.Current = new Cursor(desktopForm.textCursorSource);
        }

        private void Install_Load(object sender, EventArgs e)
        {
            WindowToolbar toolbar = new WindowToolbar(Width, Height);
            toolbar.get().Location = new Point(0, 0);
            Controls.Add(toolbar.get());

            TopMost = true;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            desktopForm.oppenedApps.Remove(this);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\\\Telekit\\Home\\Desktop";
            openFileDialog.ShowDialog(this);
            if (openFileDialog.FileName.Length <= 0)
            {
                textBox1.Text = openFileDialog.FileName;
            }*/
            _OpenFileDialog fileDialog = new _OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\\\Telekit\\Home\\Desktop";
            fileDialog.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class WindowToolbar
    {
        private Panel panel;

        private PictureBox closeButton;
        private PictureBox minimizeButton;

        public WindowToolbar(int width, int height)
        {
            panel = new Panel();

            panel.BackColor = Color.LightBlue;
            panel.Width  = width;
            panel.Height = height;

            closeButton    = new PictureBox();
            minimizeButton = new PictureBox();

            closeButton.Image       = Properties.Resources.close_dwn;
            minimizeButton.Image    = Properties.Resources.minimize_dwn;
            closeButton.SizeMode = PictureBoxSizeMode.StretchImage;
            minimizeButton.SizeMode = PictureBoxSizeMode.StretchImage;

            closeButton.Width = panel.Height;
            closeButton.Height = closeButton.Width;
            closeButton.Location = new Point(panel.Width - closeButton.Width, 0);

            minimizeButton.Width = closeButton.Width;
            minimizeButton.Height = closeButton.Width;
            minimizeButton.Location = new Point(panel.Width - (closeButton.Width * 2), 0);
        }

        public Panel get()
        {
            return panel;
        }
    }
}
