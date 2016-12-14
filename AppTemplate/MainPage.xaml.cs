using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AppLogic;
using DataModel;
using HelpLib;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace AppTemplate
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TemplateListLogic listLogic;
        private List<ListDataModel> listDataBind;
        private OfficeHelper officeHelper;
        private int iClickOpen = 0;
        private string strDonateUrl = "http://www.misear.com/donate.html";
        public MainPage()
        {
            this.InitializeComponent();
            listLogic = new TemplateListLogic();
            officeHelper = new OfficeHelper();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Helper.IsDesktop())
            {
                var dialog = new MessageDialog("To ensure that the App runs properly,it should runing in desktop device.", "Infomation");
                await dialog.ShowAsync();
            }
            await InitList();
            await GetDataPage(1);
            flipView.ItemsSource = listDataBind;
            progressInit.IsActive = false;
        }

        private async Task InitList()
        {
            listDataBind = new List<ListDataModel>();
            int iPgaeCount = await listLogic.GetPageCount();
            for (int i = 0; i < iPgaeCount; i++)
            {
                ListDataModel data = new ListDataModel();
                listDataBind.Add(data);
            }
        }

        private async Task GetDataPage(int iPage)
        {
            ObservableCollection<ListModel> list = await listLogic.GetTemplateList(iPage);
            listDataBind[iPage - 1].ListDatas = list;
        }

        private async void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = flipView.SelectedIndex;
            if (index > 0 && listDataBind[index].ListDatas == null)
            {
                await GetDataPage(flipView.SelectedIndex + 1);
                flipView.ItemsSource = listDataBind;
            }
            
        }

        private async void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            TextBlock txtPercent = (TextBlock)((RelativePanel)((Button)sender).Parent).FindName("txtPercent");
            ShowLoadingRing(sender, true);
            txtPercent.Visibility = Visibility.Visible;
            ListModel model = (ListModel)((Button)e.OriginalSource).DataContext;
            await officeHelper.OpenTemplate(model.FileName, txtPercent);
            ShowLoadingRing(sender, false);
            txtPercent.Visibility = Visibility.Collapsed;
            iClickOpen++;
            if (iClickOpen == 5)
            {
                await Launcher.LaunchUriAsync(new Uri(strDonateUrl));
            }
        }

        private async void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(strDonateUrl));
        }

        private void ShowLoadingRing(object sender, bool bShow)
        {
            Button btnSender = (Button) sender;
            if (btnSender != null)
            {
                RelativePanel rpane = (RelativePanel) btnSender.Parent;
                if (rpane != null)
                {
                    ProgressRing ring = (ProgressRing)rpane.FindName("ProgressRing");
                    if (ring != null)
                    {
                        ring.IsActive = bShow;
                    }
                }
            }
        }
    }
}
