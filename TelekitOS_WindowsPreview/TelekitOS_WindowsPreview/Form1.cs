using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Windows.Forms;

using TelekitOS_WindowsPreview.Administers.Applicacion;
using TelekitOS_WindowsPreview.Administers.Desktop;
using TelekitOS_WindowsPreview.Administers.IconGet.IconHelper;
using TelekitOS_WindowsPreview.Administers.Users;
using TelekitOS_WindowsPreview.SystemWindows;

namespace TelekitOS_WindowsPreview
{
    public partial class Form1 : Form
    {
        Install install;
        Settings settings;
        FileExplorer fileExplorer;
        AltF4 alt;

        private ImageList _smallImageList = new ImageList();
        private ImageList _largeImageList = new ImageList();
        private IconListManager _iconListManager;

        public string mainCursorSource = "C:\\\\Telekit\\System\\Cursors\\cursor.cur";
        public string handCursorSource = "C:\\\\Telekit\\System\\Cursors\\text.cur";
        public string loadingCursorSource = "C:\\\\Telekit\\System\\Cursors\\loading.cur";
        public string textCursorSource = "C:\\\\Telekit\\System\\Cursors\\text.cur";

        public static string telekitBaseDir = "C:\\\\Telekit";
        public static string usersBaseDir = telekitBaseDir + "\\Users";
        public string currentUserDir;
        public string currentUserDesktopDir;

        public List<DesktopItem> desktopIcons = new List<DesktopItem>();

        public User user;

        #region Taskbar sources
        public List<Form> oppenedApps = new List<Form>();
        #endregion

        public Form1(User loginUser)
        {
            user = loginUser;

            install = new Install(this);
            settings = new Settings(this);
            fileExplorer = new FileExplorer(this);
            alt = new AltF4(this);

            _smallImageList.ColorDepth = ColorDepth.Depth32Bit;
            _largeImageList.ColorDepth = ColorDepth.Depth32Bit;
            _smallImageList.ImageSize = new System.Drawing.Size(16, 16);
            _largeImageList.ImageSize = new System.Drawing.Size(32, 32);
            _iconListManager = new IconListManager(_smallImageList, _largeImageList);

            InitializeComponent();
            setupUserDirsAndDesktop();

            taskbarInit();
            confirmDirectrories();
            checkAndDownloadSystemFiles();

            Cursor = new Cursor(mainCursorSource);
            UseWaitCursor = true;
            Cursor.Current = new Cursor(loadingCursorSource);
            UseWaitCursor = false;
            foreach(Component component in this.components.Components)
            {
                if (component.GetType().Equals(ContextMenuStrip))
                {
                    ContextMenuStrip cms = (ContextMenuStrip)component;
                    cms.Cursor = Cursor;
                }else if (component.GetType().Equals(ContextMenuStrip))
                {
                    LinkLabel lnk = (LinkLabel)component;
                    lnk.Cursor = Cursor;
                }
            }
        }

        #region Form Events
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, Height - panel1.Height);
            panel1.Width = Width;

            pictureBox1.Width = panel1.Height;
            pictureBox1.Height = pictureBox1.Width;
            pictureBox1.Location = panel1.Location;

            panel2.Location = new Point(0, Height - (pictureBox1.Height + panel2.Height));

            panel3.Location = new Point(panel2.Location.X + panel2.Width, Height - panel1.Height - panel3.Height);

            materialLabel1.Text = user.userName;

            registerDefaultApps();
            foreach (App app in AppsControl.installedApps)
            {
                //materialListView1.Items.Add(new ListViewItem(app.dispName));
                materialListView1.Items.Add(app.dispName, _iconListManager.AddFileIcon("C:\\\\Users\\Arnyminer Z\\Source\\Repos\\Telekit-OS\\TelekitOS_WindowsPreview\\TelekitOS_WindowsPreview\\Administers\\Users\\User.cs"));
            }
            MessageBox.Show(materialListView1.Items.Count + " total Installed apps." + Environment.NewLine +
                            "::" + string.Join(" ", materialListView1.Items.Cast<ListViewItem>()));

            Update.Start();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
        }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            alt.Show();
            e.Cancel = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;

            panel2.Visible = !panel2.Visible;
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.menu_hover;
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.menu;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FormClosing -= new FormClosingEventHandler(Form1_FormClosing);
            Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            install.Show();
            try
            {
                oppenedApps.Add(install);
            }catch(Exception ex)
            {
                SystemSounds.Exclamation.Play();
                Console.Out.WriteLine("Error " + ex.Message + " in " + ex.Source + Environment.NewLine + "More: " + ex.ToString());
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            taskbarUpdate();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            fileExplorer.Show();
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Taskbar functions
        private int numberOfItems = 0;
        private Panel taskbarItems;

        private void taskbarInit()
        {
            taskbarItems = new Panel();
            Controls.Add(taskbarItems);
            taskbarItems.Location = new Point(panel1.Location.X + panel1.Width, panel1.Location.Y);
            taskbarItems.Width = Width;
            taskbarItems.Height = panel1.Height;
            taskbarItems.BackColor = Color.Green;
            taskbarItems.BringToFront();
        }

        private void taskbarUpdate()
        {
            if (numberOfItems != oppenedApps.Count)
            {
                numberOfItems = oppenedApps.Count;

                foreach (PictureBox pb in taskbarItems.Controls)
                {
                    taskbarItems.Controls.Remove(pb);
                }

                foreach (Form form in oppenedApps)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImageLayout = ImageLayout.Stretch;
                    pb.BackgroundImage = form.Icon.ToBitmap();
                    taskbarItems.Controls.Add(pb);
                }

                Controls.Remove(taskbarItems);
            }
        }
        #endregion

        private void confirmDirectrories()
        {
            if (!Directory.Exists("C:\\\\Telekit"))
            {
                Directory.CreateDirectory("C:\\\\Telekit");
            }
            if (!Directory.Exists("C:\\\\Telekit\\Home"))
            {
                Directory.CreateDirectory("C:\\\\Telekit\\Home");
            }
            if (!Directory.Exists("C:\\\\Telekit\\Home\\Desktop"))
            {
                Directory.CreateDirectory("C:\\\\Telekit\\Home\\Desktop");
            }
            if (!Directory.Exists("C:\\\\Telekit\\System"))
            {
                Directory.CreateDirectory("C:\\\\Telekit\\System");
            }
            if (!Directory.Exists("C:\\\\Telekit\\System\\Cursors"))
            {
                Directory.CreateDirectory("C:\\\\Telekit\\System\\Cursors");
            }
            if (!Directory.Exists("C:\\\\Telekit\\Software"))
            {
                Directory.CreateDirectory("C:\\\\Telekit\\Software");
            }
        }
        public void checkAndDownloadSystemFiles()
        {
            #region Cursors
            string normalCursor =  "http://www.rw-designer.com/cursor-extern.php?id=93346";
            string handCursor = "http://www.rw-designer.com/cursor-extern.php?id=93362";
            string loadingCursor = "http://www.rw-designer.com/cursor-extern.php?id=93349";
            string textCursor =    "http://www.rw-designer.com/cursor-extern.php?id=93353";

            if (!File.Exists(mainCursorSource))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(normalCursor, mainCursorSource);
                }
            }
            if (!File.Exists(handCursor))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(handCursor, handCursorSource);
                }
            }
            if (!File.Exists(loadingCursorSource))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(loadingCursor, loadingCursorSource);
                }
            }
            if (!File.Exists(textCursorSource))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(textCursor, textCursorSource);
                }
            }
            #endregion
        }

        public void registerDefaultApps()
        {
            App settings = new App("Settings");
            settings.package = "com.telekit.settings";
            settings.icon = new Bitmap(Properties.Resources.wrench);

            AppsControl.Register(settings.getCompleteApp());
        }

        public void setupUserDirsAndDesktop()
        {
            if (!Directory.Exists(usersBaseDir))
            {
                Directory.CreateDirectory(usersBaseDir);
            }
            currentUserDir = usersBaseDir + "\\" + user.userName;
            if (!Directory.Exists(currentUserDir))
            {
                Directory.CreateDirectory(currentUserDir);
            }
            currentUserDesktopDir = currentUserDir + "\\Desktop";
            if (!Directory.Exists(currentUserDesktopDir))
            {
                Directory.CreateDirectory(currentUserDesktopDir);
            }
            foreach(string file in Directory.EnumerateFiles(currentUserDesktopDir))
            {
                DesktopItem button = new DesktopItem();
                button.Text = file.Replace(currentUserDesktopDir, "").Replace("\\", "");
                desktopIcons.Add(button);
            }
            int counter = 0;
            foreach (DesktopItem btn in desktopIcons)
            {
                Controls.Add(btn);
                btn.Width = 100;
                int x = 10 + ((counter * btn.Width) + 3);
                int y = 10 + (counter * +(counter / 5));
                btn.Location = new Point(x, y);

                counter++;
            }
        }
    }
}
