using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class ApiNames
    {
        public string? MasterApi { get; set; }
        public string? HRP_GET_EMP_LOGIN_API { get; set; }
        public string? GET_VENDOR_LOGIN_API { get; set; }
        public string? HRP_GET_EMP_DETAIL_CODE { get; set; }
        public string? GET_EMP_COP_LOGIN_API { get; set; }
        public string? INSERT_UPDATE_ROLE { get; set; }
        public string? CREATE_PPS_REQUEST { get; set; }
        public string? UPDATE_STATUS_PPS_REQUEST { get; set; }
        public string? SELECT_ALL_ROLES { get; set; }
        public string? SELECT_ROLES_LIST { get; set; }
        public string? SELECT_ALL_PPSREQUEST { get; set; }
        public string? SELECT_ROLEDETAILS_ON_ID { get; set; }
        public string? SELECT_LOGGEDIN_USER_ON_EMP_CODE { get; set; }
        public string? GET_ALL_USERS { get; set; }
        public string? GET_ALL_VENDORS { get; set; }
        public string? GET_VENDOR_WISE_POS { get; set; }
        public string? GET_PO_DETAILS { get; set; }
        public string? GET_MASTER_DATA { get; set; }
        public string? GET_VENDOR_BY_CODE { get; set; }
        public string? GET_INSPECTION_REQUEST_BY_ID { get; set; }
        public string? GET_VENDOR_ADDRESS { get; set; }
        public string? GET_SAP_API { get; set; }
        public string? GET_SAP_VENDOR_API { get; set; }
        public string? SET_USER { get; set; }
        public string? SET_VENDOR { get; set; }
        public string? ADD_ADDRESS { get; set; }
        public string? SAVE_INSPECTION { get; set; }
        public string? SAVE_INSPECT_DATA { get; set; }
        public string? UPDATE_DOI { get; set; }
        public string? ASSIGN_INSPECTOR { get; set; }
        public string? SELECT_USER_ON_EMP_CODE { get; set; }

        public string? GET_ALL_MENUS { get; set; }
        public string? GET_MENUS_LIST { get; set; }
        public string? SET_MENU { get; set; }

        public string? SET_FLAGS { get; set; }

        public string? GET_BSR_LIST { get; set; }
        public string? BSR_LIST_REPORT_PDF { get; set; }
        public string? BSR_LIST_REPORT_PDF_WITH_IMAGE { get; set; }
        public string? BSR_LIST_REPORT_EXCEL { get; set; }
        public string? BSR_LIST_REPORT_EXCEL_WITH_IMAGE { get; set; }
        public string? BSR_GET_DISPLAY_FILTERS { get; set; }

        public string? BSR_REPORT { get; set; }
        public string? GET_LIST_BASED_ON_CMD { get; set; }
    }
}
