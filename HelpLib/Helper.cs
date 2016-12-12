using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpLib
{
    public class Helper
    {
        public static bool IsDesktop()
        {
            string strDevice = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
            if (strDevice == "Windows.Desktop")
            {
                return true;
            }
            return false;
        }
    }
}
