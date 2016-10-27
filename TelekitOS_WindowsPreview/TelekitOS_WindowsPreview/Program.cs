using System;
using System.Windows.Forms;

namespace TelekitOS_WindowsPreview
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TelekitOS_WindowsPreview.Administers.Users.User emulationUser = new TelekitOS_WindowsPreview.Administers.Users.User("Invitado");
            //Application.Run(new Form1(emulationUser));
            Application.Run(new Booting_Screen());
        }
    }
}
