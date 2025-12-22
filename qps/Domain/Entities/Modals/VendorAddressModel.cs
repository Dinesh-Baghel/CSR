using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class VendorAddressModel
    {
        public int ADDRESS_ID { get; set; }
        public int VENDOR_ID { get; set; }
        public string ADDRESS_LINE1 { get; set; } = "";
        public string ADDRESS_LINE2 { get; set; } = "";
        public string CITY { get; set; } = "";
        public string PINCODE { get; set; } = "";
        public string EMAIL_IDS { get; set; } = "";
        public string MOBILE_NUMBERS { get; set; } = "";
        public string REGION_CODE { get; set; } = "";
        public string REGION_DESCRIPTION { get; set; } = "";
        public bool IS_DEFAULT { get; set; }
        public int CREATED_BY { get; set; }
        public string CREATED_DATE { get; set; } = "";
        public string MODIFIED_DATE { get; set; } = "";
        public int MODIFIED_BY { get; set; }
    }
}
