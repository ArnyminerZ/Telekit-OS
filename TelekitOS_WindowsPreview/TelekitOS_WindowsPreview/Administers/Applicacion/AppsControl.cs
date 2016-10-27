using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelekitOS_WindowsPreview.Administers.Applicacion
{
    class AppsControl
    {
        public static List<App> installedApps = new List<App>();

        public static void Register(App application)
        {
            installedApps.Add(application);
        }
    }
}
