using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService;

namespace AppLogic
{
    public class TemplateListLogic
    {
        private int iPageSize = 20;
        private Dictionary<int, ObservableCollection<ListModel>> dicDatas;
        private DataHelper db;
        public TemplateListLogic()
        {
            dicDatas = new Dictionary<int, ObservableCollection<ListModel>>();
            db = new DataHelper();
        }

        public async Task<ObservableCollection<ListModel>> GetTemplateList(int page)
        {
            if (dicDatas.ContainsKey(page))
            {
                return dicDatas[page];
            }
            else
            {
                ObservableCollection<ListModel> data = await db.GetPatternList(page);
                FormatData(ref data);
                dicDatas.Add(page, data);
                return data;
            }
        }

        private void FormatData(ref ObservableCollection<ListModel> listData)
        {
            foreach (ListModel data in listData)
            {
                data.ThumbName = "http://www.misear.com/resource/template/" + data.ThumbName;
                data.FileName = "http://www.misear.com/resource/template/" + data.FileName;
            }
        }

        public async Task<int> GetPageCount()
        {
            int iTemplateCount = await db.GetPageCount();
            int iPageCount = iTemplateCount / iPageSize;
            if (iTemplateCount%iPageSize > 0)
            {
                iPageCount++;
            }
            return iPageCount;
        }

    }
}
