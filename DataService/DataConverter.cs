using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Newtonsoft.Json;

namespace DataService
{
    public class DataConverter
    {
        public async Task<ObservableCollection<ListModel>> ConvertToTemplateModel(string strJson)
        {
            ObservableCollection<ListModel> listTemplate = JsonConvert.DeserializeObject<ObservableCollection<ListModel>>(strJson);
            return listTemplate;
        }
    }
}
