using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class VendorModel
    {
        public int VENDOR_ID { get; set; } = 0;
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string GST_NO { get; set; } = "";
        public string PASSWORD { get; set; } = "";
        public bool DEFAULT_PASSWORD_CHANGED { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_EMAIL_SENT { get; set; }
        public string LAST_LOGIN_DATE { get; set; } = "";
        public string CURRENT_LOGIN_DATE { get; set; } = "";
        public string IP_ADDRESS { get; set; } = "";
        public string CREATED_DATE { get; set; } = "";
        public string CREATED_BY_NAME { get; set; } = "";
        public int CREATED_BY { get; set; }
        public string MODIFIED_DATE { get; set; } = "";
        public int MODIFIED_BY { get; set; }
        public string MODIFIED_BY_NAME { get; set; } = "";
    }






}
