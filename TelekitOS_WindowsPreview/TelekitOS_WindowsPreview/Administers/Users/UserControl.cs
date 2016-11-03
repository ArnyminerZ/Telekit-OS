using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelekitOS_WindowsPreview.Administers.Users
{
    class UserControl
    {
        public static List<User> users;

        public static void setupUsersFolder()
        {
            if (!Directory.Exists(Form1.telekitBaseDir + "\\" + "Users"))
            {
                Directory.CreateDirectory(Form1.telekitBaseDir + "\\" + "Users");
            }

            foreach (User user in users)
            {
                string currentUserDir = Form1.telekitBaseDir + "\\Users\\" + user.userName;
                if (!Directory.Exists(currentUserDir))
                {
                    Directory.CreateDirectory(currentUserDir);
                }
                if (!Directory.Exists(currentUserDir + "\\Desktop"))
                {
                    Directory.CreateDirectory(currentUserDir + "\\Desktop");
                }
            }
        }
    }
}
