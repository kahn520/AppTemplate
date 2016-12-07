using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public static class DataConst
    {
        private static readonly string m_strBaseAddress = "http://www.misear.com:8080/api";

        public static readonly string m_strTemplateListAddress = m_strBaseAddress + "/Template/TemplateList";

        public static readonly string m_strTemplateListCountAddress = m_strBaseAddress + "/Template/templatecount";
    }
}
