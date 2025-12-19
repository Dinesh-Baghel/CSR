using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public static class CmdNames
    {
        public const string Get_Breadcrumbs = "Get_Breadcrumbs"; //Get Breadcrumbs
        public const string Get_All_Roles = "Get_All_Roles"; //Select All Roles
        public const string Get_Roles_List = "Get_Roles_List"; //Select Active Roles List
        public const string Get_Role_Details_On_Role_Id = "Get_Role_Details_On_Role_Id"; //Select Roles Details On Role Id
        public const string Get_Role_Details_On_User_Id = "Get_Role_Details_On_User_Id"; //Select Roles Details On User Id
        public const string Get_All_Users = "Get_All_Users"; //Select All Users
        public const string Get_User_Details_On_User_Code = "Get_User_Details_On_User_Code"; //Select User On Emp Code

        public const string Get_All_Menu = "Get_All_Menu"; //Select All Menu
        public const string Get_Menu_List = "Get_Menu_List"; //Select All Menu
        public const string Get_Menu_List_Public = "Get_Menu_List_Public"; //Select All Menu for Public

        public const string Get_All_Content = "Get_All_Content"; //Select All Content
        public const string Get_All_ContentDetails = "Get_All_ContentDetails"; //Select All ContentDetails On Content Id
        public const string Get_Public_Content = "Get_Public_Content"; //Select All Content And ContentDetails On Page Id for public
        public const string Get_Content_Archive = "Get_Content_Archive"; //Select All Archive Content And ContentDetails On Page Id for Admin
        public const string Get_Public_Content_Archive = "Get_Public_Content_Archive"; //Select All Archive Content And ContentDetails On Page Id for public

        public const string Approve_ContentDetails = "Approve_ContentDetails"; //Approver Approve Content
        public const string Reject_ContentDetails = "Reject_ContentDetails"; //Approver Reject Content
        public const string Activate_Deactivate_ContentMaster = "Activate_Deactivate_ContentMaster"; //Activate Content
        public const string Activate_Deactivate_ContentDetails = "Activate_Deactivate_ContentDetails"; //Activate Content
        public const string Activate_Deactivate_ContentDetailsArchived = "Activate_Deactivate_ContentDetailsArchived"; //Activate Content
        public const string Activate_Deactivate_MenuMaster = "Activate_Deactivate_MenuMaster"; //Activate/Deactivate MenuMaster
        public const string Activate_Deactivate_RoleMaster = "Activate_Deactivate_RoleMaster"; //Activate/Deactivate RoleMaster

        public const string Get_BSR_Report = "Get_BSR_Report"; //Select All Menu
    }
}
