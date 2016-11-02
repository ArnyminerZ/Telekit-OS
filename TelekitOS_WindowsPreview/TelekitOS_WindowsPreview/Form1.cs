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
using TelekitOS_WindowsPreview.DefaultApps;
using TelekitOS_WindowsPreview.SystemWindows;

namespace TelekitOS_WindowsPreview
{
    public partial class Form1 : Form
    {
        #region windows
        Install install;
        Settings settings;
        FileExplorer fileExplorer;
        AltF4 alt;
        Ejecutar ejecutar;
        #endregion

        #region Cursors
        public static readonly string mainCursorSource = "C:\\\\Telekit\\System\\Cursors\\cursor.cur";
        public static readonly string handCursorSource = "C:\\\\Telekit\\System\\Cursors\\text.cur";
        public static readonly string loadingCursorSource = "C:\\\\Telekit\\System\\Cursors\\loading.cur";
        public static readonly string textCursorSource = "C:\\\\Telekit\\System\\Cursors\\text.cur";

        public static readonly Cursor mainCursor = new Cursor(mainCursorSource);
        public static readonly Cursor handCursor = new Cursor(handCursorSource);
        public static readonly Cursor loadingCursor = new Cursor(loadingCursorSource);
        public static readonly Cursor textCursor = new Cursor(textCursorSource);
        #endregion

        #region System dirs
        public static readonly string sep = "\\";
        public static string telekitBaseDir = "C:\\\\Telekit";
        public static string usersBaseDir = telekitBaseDir + "\\Users";
        public string softwareBaseDir;
        public string currentUserDir;
        public string currentUserDesktopDir;
        #endregion

        public List<DesktopItem> desktopIcons = new List<DesktopItem>();

        #region current user
        public User user;
        #endregion

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
            ejecutar = new Ejecutar();
            
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
            installApps();
            foreach (App app in AppsControl.installedApps)
            {
                //materialListView1.Items.Add(new ListViewItem(app.dispName));
                materialListView1.Items.Add(app.dispName);
            }
            /*MessageBox.Show(materialListView1.Items.Count + " total Installed apps." + Environment.NewLine +
                            "::" + string.Join(" ", materialListView1.Items.Cast<ListViewItem>()));*/

            update.Start();
        }

        #region Form Events
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
        private void materialListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string appName = materialListView1.SelectedItems[0].Text;
            List<App> apps = AppsControl.installedApps;

            //MessageBox.Show("Trying to show " + appName);

            foreach(App app in apps)
            {
                if (app.dispName.Equals(appName, StringComparison.CurrentCultureIgnoreCase))
                {
                    //MessageBox.Show("Executing " + appName);
                    app.execute();
                    app.mainForm.Cursor = mainCursor;
                }/*else
                    MessageBox.Show("Passing " + appName);*/
            }

            // DESELECT ALL 
            foreach(ListViewItem item in materialListView1.SelectedItems)
            {
                item.Selected = false;
            }
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
            App execute = new App("Execute");
            execute.package = "com.telekit.exec";
            execute.executionName = "exec";
            execute.icon = new Bitmap(Properties.Resources.wrench);
            execute.cursor = mainCursor;
            execute.mainForm = this.ejecutar;

            App settings = new App("Settings");
            settings.executionName = "sett";
            settings.package = "com.telekit.settings";
            settings.icon = new Bitmap(Properties.Resources.wrench);
            settings.cursor = mainCursor;
            settings.mainForm = this.settings;

            App dictionary = new App("Dictionary");
            dictionary.package = "com.telekit.dict";
            dictionary.executionName = "dict";
            dictionary.icon = new Bitmap(Properties.Resources.wrench);
            dictionary.cursor = mainCursor;
            dictionary.mainForm = new Diccionario();

            AppsControl.Register(execute.getCompleteApp());
            AppsControl.Register(settings.getCompleteApp());
            AppsControl.Register(dictionary.getCompleteApp());
        }
        public void installApps()
        {
            // fileNames value is all files in softwareBaseDir variable
            string[] fileNames = Directory.GetDirectories(softwareBaseDir);
            foreach (string _appDir in fileNames)
            {
                string appDir = _appDir.Replace(softwareBaseDir + sep, "");
                MessageBox.Show("Loading app " + appDir);
                App currentRegisteringApp = new App();
                string currentAppDir = softwareBaseDir + sep + appDir;
                if (File.Exists(currentAppDir + sep + "info.nf"))
                {
                    string[] infoFileLines = File.ReadAllLines(currentAppDir + sep + "info.nf");
                    foreach(string line in infoFileLines)
                    {
                        // This detects if is commented
                        if (line.StartsWith("-->"))
                        {
                            continue;
                        }

                        // If line defines app name
                        if (line.StartsWith("NM--"))
                        {
                            currentRegisteringApp.dispName = line.Replace("NM--", "");
                        }
                        // If line defines app package
                        if (line.StartsWith("PK--"))
                        {
                            currentRegisteringApp.package = line.Replace("PK--", "");
                        }
                        // If line defines app mainForm
                        if (line.StartsWith("MF--"))
                        {
                            currentRegisteringApp.mainForm = new Form();
                        }
                        // If line defines app mainForm
                        if (line.StartsWith("EN--"))
                        {
                            currentRegisteringApp.executionName = line.Replace("EN--", "");
                        }
                        // If line defines app mainForm
                        if (line.StartsWith("IC--"))
                        {
                            currentRegisteringApp.icon = Image.FromFile(currentAppDir + sep + "res" + sep + line.Replace("IC--", ""));
                        }
                    }

                    AppsControl.Register(currentRegisteringApp);
                }
                else
                {
                    continue;
                }
            }
        }

        public void setupUserDirsAndDesktop()
        {
            #region Create users base dir
            if (!Directory.Exists(usersBaseDir))
            {
                Directory.CreateDirectory(usersBaseDir);
            }
            #endregion
            #region Create current user dir
            currentUserDir = usersBaseDir + "\\" + user.userName;
            if (!Directory.Exists(currentUserDir))
            {
                Directory.CreateDirectory(currentUserDir);
            }
            #endregion
            #region Create current user desktop dir
            currentUserDesktopDir = currentUserDir + "\\Desktop";
            if (!Directory.Exists(currentUserDesktopDir))
            {
                Directory.CreateDirectory(currentUserDesktopDir);
            }
            #endregion

            #region Create software directory
            softwareBaseDir = telekitBaseDir + "\\Software";
            if (!Directory.Exists(softwareBaseDir))
            {
                Directory.CreateDirectory(softwareBaseDir);
            }
            #endregion

            #region AddDesktopItems
            foreach (string file in Directory.EnumerateFiles(currentUserDesktopDir))
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
                btn.Icon = Properties.Resources.gear_dwn;

                counter++;
            }
            #endregion
        }
    }
}
