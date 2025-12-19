using Domain.Entities.Common;
using Domain.Entities.Modals;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Records
{
    public record LoginUser(string Id, string EmpCode, string Name, string MobilePhone, string Email, string Role, Modules Modules, CopLoginRes copLoginRes)
    {
        public Claim[] ToClaims()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, EmpCode),
                    new Claim(ClaimTypes.Name, Name),
                    new Claim(ClaimTypes.MobilePhone, MobilePhone),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(CustomClaimTypes.User_Id, Id),
                    new Claim(CustomClaimTypes.User_Management, Modules.User_Management.ToString()),
                    new Claim(CustomClaimTypes.Media, Modules.Media.ToString()),
                    new Claim(CustomClaimTypes.Menu, Modules.Menu.ToString()),
                    new Claim(CustomClaimTypes.CMS, Modules.CMS.ToString()),
                    //new Claim(CustomClaimTypes.LoginRes, JsonSerializer.Serialize(copLoginRes)), // Commented By Chandra Bhan Singh On 16 Apr 2025
                };

            //// Add claims for each menu
            //if (menuDetail != null)
            //{
            //    claims.AddRange(menuDetail.Select(menu =>
            //    {
            //        var permission = GetPermissionFromAccessType(menu.menuAccessType);
            //        var role = GetUserRoleNameFromMenuId(menu.menuID);
            //        return new Claim($"Menu:{role}", $"{permission}");
            //        //return new Claim($"Menu:{menu.menuID}", $"{permission},{role}");
            //    }));
            //}
            return claims.ToArray();
        }

        //private static UserRolePermissions GetPermissionFromAccessType(string? accessType)
        //{
        //    return accessType switch
        //    {
        //        "R" => UserRolePermissions.Read,
        //        "RW" => UserRolePermissions.Write,
        //        "W" => UserRolePermissions.Write,
        //        _ => UserRolePermissions.None,
        //    };
        //}
        //private static UserRoleName? GetUserRoleNameFromMenuId(int menuId)
        //{
        //    return menuId switch
        //    {
        //        1 => UserRoleName.Sale,
        //        2 => UserRoleName.CreateReturn,
        //        3 => UserRoleName.IssueCreditNote,
        //        4 => UserRoleName.Tender,
        //        5 => UserRoleName.Billing,
        //        6 => UserRoleName.SetupDevice,
        //        7 => UserRoleName.UserMaster,
        //        8 => UserRoleName.GstInvoice,
        //        10 => UserRoleName.CreateGiftVoucher,
        //        11 => UserRoleName.ApproveGiftVoucher,
        //        12 => UserRoleName.ApproveManualDiscount,
        //        14 => UserRoleName.Report,
        //        16 => UserRoleName.ApproveDuplicateBill,
        //        18 => UserRoleName.ApproveCancelBill,
        //        _ => null
        //    };
        //}
    }
}
