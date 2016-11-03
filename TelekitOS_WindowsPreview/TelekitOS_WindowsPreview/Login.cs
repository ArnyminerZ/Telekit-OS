using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelekitOS_WindowsPreview.Administers.Users;

namespace TelekitOS_WindowsPreview
{
    public partial class Login : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public Login()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, 
                                                              Primary.BlueGrey900, 
                                                              Primary.BlueGrey500, 
                                                              Accent.LightBlue200, 
                                                              TextShade.WHITE);

            materialTabSelector1.Width  = Width * 2;
            materialTabSelector1.Height = Height * 10;
        }

        public static User user;
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (userBox.TextLength < 3 ||
                passBox.TextLength < 5)
            {
                SystemSounds.Exclamation.Play();
                return;
            }
            user = new User(userBox.Text);
            Form1 form = new Form1(user);
        }
    }
}
