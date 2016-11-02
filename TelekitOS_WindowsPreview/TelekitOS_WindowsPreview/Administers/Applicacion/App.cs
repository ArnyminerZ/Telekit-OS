using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelekitOS_WindowsPreview.Administers.Applicacion
{
    class App
    {
        public string package = null;
        public string dispName = null;
        public Image icon = null;
        public Form mainForm = null;
        public string executionName = null;
        public Cursor cursor = null;

        public App(){ }
        public App(string displayName) { dispName = displayName; }

        public App getCompleteApp()
        {
            if (package != null &&
                dispName != null &&
                icon != null &&
                mainForm != null &&
                executionName != null &&
                cursor != null)
            {
                return this;
            }
            else
            {
                throw new SomeValuesNotSet("You must set all the values specified");
            }
        }

        public void execute()
        {
            mainForm.Show();
            Cursor.Current = cursor;
            mainForm.Cursor = cursor;
        }
    }

    public class SomeValuesNotSet : Exception
    {
        public SomeValuesNotSet()
        {
        }

        public SomeValuesNotSet(string message)
            : base(message)
        {
        }

        public SomeValuesNotSet(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
