using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBHelper;

namespace AppWebApi.Controllers
{
    public class TemplateController : ApiController
    {
        DBService db = new DBService();

        [ActionName("TemplateCount")]
        public int GetTemplateCount()
        {
            int count = db.GetTemplateCount();
            return count;
        }

        [ActionName("TemplateList")]
        public string GetTemplateList()
        {
            string json = db.GetTemplateList();
            return json;
        }
    }
}
