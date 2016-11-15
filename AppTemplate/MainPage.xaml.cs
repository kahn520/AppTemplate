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
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            listLogic = new TemplateListLogic();
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitList();
            await GetDataPage(1);
            flipView.ItemsSource = listDataBind;
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

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {

        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btnApply_Click(object sender, RoutedEventArgs e)
        {
            OfficeHelper office = new OfficeHelper();
            ListModel model = (ListModel) ((Button) e.OriginalSource).DataContext;
            office.OpenTemplate(model.FileName);
        }
    }
}
