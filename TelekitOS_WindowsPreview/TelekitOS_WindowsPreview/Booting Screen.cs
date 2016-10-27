using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelekitOS_WindowsPreview.SystemWindows.Sources;

namespace TelekitOS_WindowsPreview
{
    public partial class Booting_Screen : Form
    {
        public Booting_Screen()
        {
            InitializeComponent();
        }

        private void Booting_Screen_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point((Width - pictureBox1.Width) / 2, ((Height - pictureBox1.Height) / 2) + 50);

            InitializeAll();

            Form1 form = new Form1(new Administers.Users.User("Emulador"));
            form.ShowDialog();
        }

        private void InitializeAll()
        {
            materialLabel2.Text = "Version: " + DownloadedVersion();
            if(DownloadedVersion() != WebVersion())
            {
                MaterialMessageBox mmb = new MaterialMessageBox("Una nueva versión está disponible, haz click en actualizar para iniciar la actualización", new bool[] { false, false });
                mmb.Show();
            }
        }

        private string WebVersion()
        {
            string urlAddress = "http://pastebin.com/raw/AU1ryFKm";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return data;
            }else
            {
                return null;
            }
        }
        private string DownloadedVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return fileVersionInfo.ProductVersion;
        }
    }
}
