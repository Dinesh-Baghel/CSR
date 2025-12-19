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
        public string? HRP_GET_EMP_DETAIL_CODE { get; set; }
        public string? GET_EMP_COP_LOGIN_API { get; set; }
        public string? INSERT_UPDATE_ROLE { get; set; }
        public string? SELECT_ALL_ROLES { get; set; }
        public string? SELECT_ROLES_LIST { get; set; }
        public string? SELECT_ROLEDETAILS_ON_ID { get; set; }
        public string? SELECT_LOGGEDIN_USER_ON_EMP_CODE { get; set; }
        public string? GET_ALL_USERS { get; set; }
        public string? SET_USER { get; set; }
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
    }
}
