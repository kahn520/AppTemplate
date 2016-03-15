using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
                string json = JsonConvert.SerializeObject(dt, new DataTableConverter());
                return json;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
