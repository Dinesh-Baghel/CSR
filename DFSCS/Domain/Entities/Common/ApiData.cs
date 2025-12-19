using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class ApiData
    {
        public string? Api_Name { get; set; }
        public string? Task_Name { get; set; }
        public string? Api_Url { get; set; }
        public string? Api_Username { get; set; }
        public string? Api_Pwd { get; set; }
        public string? Api_Proxy { get; set; }
        public int Api_Port { get; set; }
        public string? Api_Http_Method { get; set; }
        public string? Api_Content_Type { get; set; }
        public string? Api_Authorization_Type { get; set; }
        public string? Api_Authorization { get; set; }
        public int Api_Timeout { get; set; }
        public string? Token_Api_Name { get; set; }
        public DateTime? Api_Token_Expiry_Time { get; set; }
        public string? Api_Token_Status { get; set; }
    }
}
