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
using TelekitOS_WindowsPreview.Administers.Applicacion;

namespace TelekitOS_WindowsPreview.DefaultApps
{
    public partial class Ejecutar : MaterialForm
    {
        public Ejecutar()
        {
            InitializeComponent();
        }

        private void Ejecutar_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string command = materialSingleLineTextField1.Text;

            if (command.Contains("app "))
            {
                string toExecute = command.Replace("app ", "");

                List<App> installedApps = AppsControl.installedApps;
                foreach(App app in installedApps)
                {
                    if (app.executionName.Equals(toExecute, StringComparison.CurrentCultureIgnoreCase))
                    {
                        app.execute();
                    }
                }
            }
        }
    }
}
