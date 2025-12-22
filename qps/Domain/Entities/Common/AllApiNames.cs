using Domain.Entities.Response;
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
        public static string GET_VENDOR_LOGIN_API { get; set; } = string.Empty;
        public static string HRP_GET_EMP_DETAIL_CODE { get; set; } = string.Empty;
        public static string GET_EMP_COP_LOGIN_API { get; set; } = string.Empty;
        public static string INSERT_UPDATE_ROLE { get; set; } = string.Empty;
        public static string CREATE_PPS_REQUEST { get; set; } = string.Empty;
        public static string UPDATE_STATUS_PPS_REQUEST { get; set; } = string.Empty;
        public static string SELECT_ALL_ROLES { get; set; } = string.Empty;
        public static string SELECT_ALL_PPSREQUEST { get; set; } = string.Empty;
        public static string? SELECT_ROLES_LIST { get; set; } = string.Empty;
        public static string SELECT_ROLEDETAILS_ON_ID { get; set; } = string.Empty;
        public static string GET_ALL_USERS { get; set; } = string.Empty;
        public static string GET_ALL_VENDORS { get; set; } = string.Empty;
        public static string GET_VENDOR_WISE_POS { get; set; } = string.Empty;
        public static string GET_PO_DETAILS { get; set; } = string.Empty;
        public static string GET_MASTER_DATA { get; set; } = string.Empty;
        public static string GET_VENDOR_BY_CODE { get; set; } = string.Empty;
        public static string GET_INSPECTION_REQUEST_BY_ID { get; set; } = string.Empty;
        public static string GET_VENDOR_ADDRESS { get; set; } = string.Empty;
        public static string GET_SAP_API { get; set; } = string.Empty;
        public static string GET_SAP_VENDOR_API { get; set; } = string.Empty;
        public static string? SET_USER { get; set; } = string.Empty;
        public static string? SET_VENDOR { get; set; } = string.Empty;
        public static string? ADD_ADDRESS { get; set; } = string.Empty;
        public static string? SAVE_INSPECTION { get; set; } = string.Empty;
        public static string? SAVE_INSPECT_DATA { get; set; } = string.Empty;
        public static string? UPDATE_DOI { get; set; } = string.Empty;
        public static string? ASSIGN_INSPECTOR { get; set; } = string.Empty;
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
        public static string? BSR_REPORT { get; set; } = string.Empty;
        public static string? GET_LIST_BASED_ON_CMD { get; set; } = string.Empty;

        public static void Initialize(ApiNames apiNames)
        {
            MasterApi = apiNames.MasterApi!;
            HRP_GET_EMP_LOGIN_API = apiNames.HRP_GET_EMP_LOGIN_API!;
            GET_VENDOR_LOGIN_API = apiNames.GET_VENDOR_LOGIN_API!;
            HRP_GET_EMP_DETAIL_CODE = apiNames.HRP_GET_EMP_DETAIL_CODE!;
            GET_EMP_COP_LOGIN_API = apiNames.GET_EMP_COP_LOGIN_API!;
            INSERT_UPDATE_ROLE = apiNames.INSERT_UPDATE_ROLE!;
            CREATE_PPS_REQUEST = apiNames.CREATE_PPS_REQUEST!;
            UPDATE_STATUS_PPS_REQUEST = apiNames.UPDATE_STATUS_PPS_REQUEST!;
            SELECT_ALL_ROLES = apiNames.SELECT_ALL_ROLES!;
            SELECT_ROLES_LIST = apiNames.SELECT_ROLES_LIST!;
            SELECT_ALL_PPSREQUEST = apiNames.SELECT_ALL_PPSREQUEST!;
            SELECT_ROLEDETAILS_ON_ID = apiNames.SELECT_ROLEDETAILS_ON_ID!;
            GET_ALL_USERS = apiNames.GET_ALL_USERS!;
            GET_ALL_VENDORS = apiNames.GET_ALL_VENDORS!;
            GET_VENDOR_WISE_POS = apiNames.GET_VENDOR_WISE_POS!;
            GET_PO_DETAILS = apiNames.GET_PO_DETAILS!;
            GET_MASTER_DATA = apiNames.GET_MASTER_DATA!;
            GET_VENDOR_BY_CODE = apiNames.GET_VENDOR_BY_CODE!;
            GET_INSPECTION_REQUEST_BY_ID = apiNames.GET_INSPECTION_REQUEST_BY_ID!;
            GET_VENDOR_ADDRESS = apiNames.GET_VENDOR_ADDRESS!;
            GET_SAP_API = apiNames.GET_SAP_API!;
            GET_SAP_VENDOR_API = apiNames.GET_SAP_VENDOR_API!;
            SET_USER = apiNames.SET_USER!;
            SET_VENDOR = apiNames.SET_VENDOR!;
            ADD_ADDRESS = apiNames.ADD_ADDRESS!;
            SAVE_INSPECTION = apiNames.SAVE_INSPECTION!;
            SAVE_INSPECT_DATA = apiNames.SAVE_INSPECT_DATA!;
            UPDATE_DOI = apiNames.UPDATE_DOI!;
            ASSIGN_INSPECTOR = apiNames.ASSIGN_INSPECTOR!;
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
            BSR_REPORT = apiNames.BSR_REPORT!;
            GET_LIST_BASED_ON_CMD = apiNames.GET_LIST_BASED_ON_CMD!;
        }
    }
}
