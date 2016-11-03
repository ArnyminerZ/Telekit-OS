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

            Administers.Users.User emulationUser = new Administers.Users.User("Invitado");
            //Application.Run(new Form1(emulationUser));
            //Application.Run(new Booting_Screen())
            Application.Run(new Login());
        }
    }
}
