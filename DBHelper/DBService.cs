using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web;
using System.Web.Script.Serialization;

namespace DBHelper
{
    public class DBService
    {
        private string strConString = ConfigurationManager.ConnectionStrings["AppConString"].ToString();
        public int GetTemplateCount()
        {

            int count = 0;
            MySqlConnection con = new MySqlConnection(strConString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("select count(1) from viewdata", con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                con.Close();
                count = Convert.ToInt32(obj);
                return count;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return count;
        }

        public string GetTemplateList()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(strConString);
                MySqlDataAdapter da = new MySqlDataAdapter("Select * From ViewData", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string json = ToJson(dt);
                return json;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetTemplateList(int page)
        {
            try
            {
                int size = 20;

                MySqlConnection con = new MySqlConnection(strConString);
                string strSql = $"Select * From ViewData limit {(page - 1)*size + 1},{size}";
                MySqlDataAdapter da = new MySqlDataAdapter(strSql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string json = ToJson(dt);
                return json;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ToJson(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, new DataTableConverter());

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }
    }
}
