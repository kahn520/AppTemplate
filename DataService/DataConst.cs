using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public static class DataConst
    {
        private static readonly string m_strBaseAddress = "http://localhost:53779/api";

        public static readonly string m_strTemplateListAddress = m_strBaseAddress + "/template/TemplateList";

        public static readonly string m_strTemplateListCountAddress = m_strBaseAddress + "/template/templatecount";
    }
}
