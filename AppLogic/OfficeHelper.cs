using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using Windows.UI.Xaml.Controls;

namespace AppLogic
{
    public class OfficeHelper
    {
        public async Task OpenTemplate(string strUrl, TextBlock txtPercent)
        {
            await Task.Run(async () =>
            {
                string strFile = strUrl.Substring(strUrl.LastIndexOf('/') + 1);
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(strUrl);
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse) (await request.GetResponseAsync());
                }
                catch (Exception ex)
                {
                    MessageDialog msg = new MessageDialog("Downlaod Error,Please try again.");
                    msg.ShowAsync();
                    return;
                }
                Stream st = response.GetResponseStream();
                MemoryStream stmMemory = new MemoryStream();
                byte[] buffer = new byte[response.ContentLength];

                int iDownloadedSize = 0;
                int i;
                while ((i = st.Read(buffer, 0, buffer.Length/100)) > 0)
                {
                    stmMemory.Write(buffer, 0, i);
                    iDownloadedSize += i;
                    float fPercent = iDownloadedSize*1.0f/buffer.Length;
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            txtPercent.Text = String.Format("{0:0%}", fPercent);
                        });
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
