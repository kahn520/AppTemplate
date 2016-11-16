using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;

namespace AppLogic
{
    public class OfficeHelper
    {
        public async Task OpenTemplate(string strUrl)
        {
            await Task.Run(async () =>
             {
                 string strFile = strUrl.Substring(strUrl.LastIndexOf('/') + 1);
                 HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                 HttpWebResponse response = null;
                 try
                 {
                     response = (HttpWebResponse)(await request.GetResponseAsync());
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }

                 Stream st = response.GetResponseStream();
                 MemoryStream stmMemory = new MemoryStream();
                 byte[] buffer = new byte[64 * 1024];
                 int i;
                 while ((i = st.Read(buffer, 0, buffer.Length)) > 0)
                 {
                     stmMemory.Write(buffer, 0, i);
                 }
                 byte[] by = stmMemory.ToArray();
                 stmMemory.Dispose();
                 StorageFile file =
                     await
                         ApplicationData.Current.LocalFolder.CreateFileAsync("PPTFiles\\" + strFile,
                             CreationCollisionOption.OpenIfExists);
                 await FileIO.WriteBytesAsync(file, by);
                 await Windows.System.Launcher.LaunchFileAsync(file);
             });
        }
    }
}
