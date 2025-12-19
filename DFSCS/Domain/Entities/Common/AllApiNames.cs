using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public static class AllApiNames
    {
        public static string MasterApi { get; set; } = string.Empty;
        public static string HRP_GET_EMP_LOGIN_API { get; set; } = string.Empty;
        public static string HRP_GET_EMP_DETAIL_CODE { get; set; } = string.Empty;
        public static string GET_EMP_COP_LOGIN_API { get; set; } = string.Empty;
        public static string INSERT_UPDATE_ROLE { get; set; } = string.Empty;
        public static string SELECT_ALL_ROLES { get; set; } = string.Empty;
        public static string? SELECT_ROLES_LIST { get; set; } = string.Empty;
        public static string SELECT_ROLEDETAILS_ON_ID { get; set; } = string.Empty;
        public static string GET_ALL_USERS { get; set; } = string.Empty;
        public static string? SET_USER { get; set; } = string.Empty;
        public static string SELECT_USER_ON_EMP_CODE { get; set; } = string.Empty;
        public static string SELECT_LOGGEDIN_USER_ON_EMP_CODE { get; set; } = string.Empty;
        public static string? GET_ALL_MENUS { get; set; } = string.Empty;
        public static string? GET_MENUS_LIST { get; set; } = string.Empty;
        public static string? SET_MENU { get; set; } = string.Empty;
       
        public static string? SET_FLAGS { get; set; } = string.Empty;

        public static string? GET_BSR_LIST { get; set; } = string.Empty;
        public static string? BSR_LIST_REPORT_PDF { get; set; } = string.Empty;
        public static string? BSR_LIST_REPORT_PDF_WITH_IMAGE { get; set; } = string.Empty;
        public static string? BSR_LIST_REPORT_EXCEL { get; set; } = string.Empty;
        public static string? BSR_LIST_REPORT_EXCEL_WITH_IMAGE { get; set; } = string.Empty;
        public static string? BSR_GET_DISPLAY_FILTERS { get; set; } = string.Empty;

        public static void Initialize(ApiNames apiNames)
        {
            MasterApi = apiNames.MasterApi!;
            HRP_GET_EMP_LOGIN_API = apiNames.HRP_GET_EMP_LOGIN_API!;
            HRP_GET_EMP_DETAIL_CODE = apiNames.HRP_GET_EMP_DETAIL_CODE!;
            GET_EMP_COP_LOGIN_API = apiNames.GET_EMP_COP_LOGIN_API!;
            INSERT_UPDATE_ROLE = apiNames.INSERT_UPDATE_ROLE!;
            SELECT_ALL_ROLES = apiNames.SELECT_ALL_ROLES!;
            SELECT_ROLES_LIST = apiNames.SELECT_ROLES_LIST!;
            SELECT_ROLEDETAILS_ON_ID = apiNames.SELECT_ROLEDETAILS_ON_ID!;
            GET_ALL_USERS = apiNames.GET_ALL_USERS!;
            SET_USER = apiNames.SET_USER!;
            SELECT_USER_ON_EMP_CODE = apiNames.SELECT_USER_ON_EMP_CODE!;
            SELECT_LOGGEDIN_USER_ON_EMP_CODE = apiNames.SELECT_LOGGEDIN_USER_ON_EMP_CODE!;
            GET_ALL_MENUS = apiNames.GET_ALL_MENUS!;
            GET_MENUS_LIST = apiNames.GET_MENUS_LIST!;
            SET_MENU = apiNames.SET_MENU!;

            SET_FLAGS = apiNames.SET_FLAGS!;

            GET_BSR_LIST = apiNames.GET_BSR_LIST!;

            BSR_LIST_REPORT_PDF = apiNames.BSR_LIST_REPORT_PDF!;
            BSR_LIST_REPORT_PDF_WITH_IMAGE = apiNames.BSR_LIST_REPORT_PDF_WITH_IMAGE!;
            BSR_LIST_REPORT_EXCEL = apiNames.BSR_LIST_REPORT_EXCEL!;
            BSR_LIST_REPORT_EXCEL_WITH_IMAGE = apiNames.BSR_LIST_REPORT_EXCEL_WITH_IMAGE!;
            BSR_GET_DISPLAY_FILTERS = apiNames.BSR_GET_DISPLAY_FILTERS!;
        }
    }
}
