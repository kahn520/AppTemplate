using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using DataModel;

namespace DataService
{
    public class DataHelper
    {
        public async Task<ObservableCollection<ListModel>> GetPatternList(int page)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(DataConst.m_strTemplateListAddress + "/" + page);
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim('\"');
            DataConverter converter = new DataConverter();
            return await converter.ConvertToTemplateModel(responseBody);
        }

        public async Task<int> GetPageCount()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(DataConst.m_strTemplateListCountAddress);
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return Convert.ToInt32(responseBody);
        }

    }
}
