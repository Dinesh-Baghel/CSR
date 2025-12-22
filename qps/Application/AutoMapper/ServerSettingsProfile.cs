using AutoMapper;
using Domain.Entities.Modals;
using Domain.Entities.Response;
using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{

    public class ServerSettingsProfile : Profile
    {
        public ServerSettingsProfile()
        {
            CreateMap<EmailServerSettings, ServerSettings>()
                .ForMember(dest => dest.SmtpServer, opt => opt.MapFrom(src => src.SmtpServer))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.SmtpUser))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.SmtpPwd))
                .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.SmtpPort))
                .ForMember(dest => dest.UseDefaultCredentials, opt => opt.MapFrom(src => src.SmtpDefaultCredential))
                .ForMember(dest => dest.EnableSsl, opt => opt.MapFrom(src => src.EnableSsl));

        }

    }

    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing(d => d.ToString("yyyy-MM-dd"));
            CreateMap<string, DateTime>().ConvertUsing(s => DateTime.Parse(s));

            CreateMap<Vendor, VendorModel>()
                   .ForMember(dest => dest.VENDOR_CODE, cfg => cfg.MapFrom(src => src.VendorCode))
                   .ForMember(dest => dest.VENDOR_NAME, cfg => cfg.MapFrom(src => src.VendorName))
                   .ForMember(dest => dest.VENDOR_ID, cfg => cfg.MapFrom(src => src.VendorId))
                   .ForMember(dest => dest.PASSWORD, cfg => cfg.MapFrom(src => src.PassWord))
                   .ForMember(dest => dest.DEFAULT_PASSWORD_CHANGED, cfg => cfg.MapFrom(src => src.Default_Password_Changed))
                   .ForMember(dest => dest.IP_ADDRESS, cfg => cfg.MapFrom(src => src.IPAddress))
                   .ForMember(dest => dest.CURRENT_LOGIN_DATE, cfg => cfg.MapFrom(src => src.CurrentLoginDate))
                   .ForMember(dest => dest.LAST_LOGIN_DATE, cfg => cfg.MapFrom(src => src.LastLoginDate))
                   .ForMember(dest => dest.IS_ACTIVE, cfg => cfg.MapFrom(src => src.IsActive))
                   .ForMember(dest => dest.IS_EMAIL_SENT, cfg => cfg.MapFrom(src => src.IsSentEmail))
                   .ForMember(dest => dest.GST_NO, cfg => cfg.MapFrom(src => src.GSTNo))
                   .ForMember(dest => dest.CREATED_BY, cfg => cfg.MapFrom(src => src.CreatedBy))
                   .ForMember(dest => dest.CREATED_BY_NAME, cfg => cfg.MapFrom(src => src.CreatedByName))
                   .ForMember(dest => dest.CREATED_DATE, cfg => cfg.MapFrom(src => src.CreatedDate))
                   .ForMember(dest => dest.MODIFIED_BY, cfg => cfg.MapFrom(src => src.ModifiedBy))
                   .ForMember(dest => dest.MODIFIED_DATE, cfg => cfg.MapFrom(src => src.ModifiedDate)).ReverseMap();

            CreateMap<Vendor, VendorDetail>().ReverseMap();

            CreateMap<VendorDetail, VendorAddressDetail>()
               .ForMember(dest => dest.PinCode, cft => cft.MapFrom(src => src.PostCode))
               .ForMember(dest => dest.AddressLine1, cft => cft.MapFrom(src => src.Address))
               .ForMember(dest => dest.Email_Ids, cft => cft.MapFrom(src => src.EmailId))
               .ForMember(dest => dest.Mobile_Numbers, cft => cft.MapFrom(src => src.ContactNo));

            CreateMap<VendorAddressDetail, VendorAddressModel>()
                .ForMember(dest => dest.ADDRESS_ID, cft => cft.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.ADDRESS_LINE1, cft => cft.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.ADDRESS_LINE2, cft => cft.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.CITY, cft => cft.MapFrom(src => src.City))
                .ForMember(dest => dest.PINCODE, cft => cft.MapFrom(src => src.PinCode))
                .ForMember(dest => dest.EMAIL_IDS, cft => cft.MapFrom(src => src.Email_Ids))
                .ForMember(dest => dest.MOBILE_NUMBERS, cft => cft.MapFrom(src => src.Mobile_Numbers))
                .ForMember(dest => dest.REGION_CODE, cft => cft.MapFrom(src => src.Region))
                .ForMember(dest => dest.REGION_DESCRIPTION, cft => cft.MapFrom(src => src.RegionDescription))
                .ForMember(dest => dest.IS_DEFAULT, cft => cft.MapFrom(src => src.IsDefault))
                .ForMember(dest => dest.CREATED_BY, cft => cft.MapFrom(src => src.Created_By))
                .ForMember(dest => dest.CREATED_DATE, cft => cft.MapFrom(src => src.Created_Date))
                .ReverseMap();

            CreateMap<POHeader, PO_HEADER_MODEL>()
               .ForMember(dest => dest.POID, cfg => cfg.MapFrom(src => src.Id))
               .ForMember(dest => dest.INSP_ID, cfg => cfg.MapFrom(src => src.InspId))
               .ForMember(dest => dest.PO_NUMBER, cfg => cfg.MapFrom(src => src.PoNumber))
               .ForMember(dest => dest.VENDOR_CODE, cfg => cfg.MapFrom(src => src.VendorCode))
               .ForMember(dest => dest.VENDOR_NAME, cfg => cfg.MapFrom(src => src.VendorName))
               .ForMember(dest => dest.COLOR, cfg => cfg.MapFrom(src => src.Color))
               .ForMember(dest => dest.FABRIC, cfg => cfg.MapFrom(src => src.Fabric))
               .ForMember(dest => dest.DATE_OF_INSPECTION, cfg => cfg.MapFrom(src => src.DateOfInspection))
               .ForMember(dest => dest.OFFERED_QTY, cfg => cfg.MapFrom(src => src.OfferedQty))
               .ForMember(dest => dest.TOTAL_QTY, cfg => cfg.MapFrom(src => src.POTotalQty))
               .ForMember(dest => dest.ADDRESS_ID, cfg => cfg.MapFrom(src => src.AddressId))
               .ForMember(dest => dest.REGION_ID, cfg => cfg.MapFrom(src => src.RegionId))
               .ForMember(dest => dest.LOCATION, cft => cft.MapFrom(src => src.InspectionLocation))
               .ForMember(dest => dest.CREATED_BY, cft => cft.MapFrom(src => src.Created_By))
               .ForMember(dest => dest.CREATED_ON, cft => cft.MapFrom(src => src.CreatedOn))

               .ForMember(dest => dest.IS_DATE_ASSIGNED_BY_QA, cft => cft.MapFrom(src => src.Is_Date_Assigned_By_QA))
               .ForMember(dest => dest.IS_INSPECTOR_ASSIGNED, cft => cft.MapFrom(src => src.Is_Inspector_Assigned))
               .ForMember(dest => dest.INSP_REQ_STATUS, cft => cft.MapFrom(src => src.Insp_Req_Status))
               .ForMember(dest => dest.INSPECTOR_EMP_CODE, cft => cft.MapFrom(src => src.InspectorId))
               .ForMember(dest => dest.QA_ASSIGNED_DOI, cfg => cfg.MapFrom(src => src.QA_ASSIGNED_DOI))

               .ReverseMap();



            CreateMap<PODetails, PO_DETAILS_MODEL>()
              .ForMember(dest => dest.PO_DETAILS_ID, cfg => cfg.MapFrom(src => src.Id))
              .ForMember(dest => dest.LINE_ITEM, cfg => cfg.MapFrom(src => src.LineItem))
              .ForMember(dest => dest.ARTICLE, cfg => cfg.MapFrom(src => src.Article))
              .ForMember(dest => dest.ARTICLE_DESC, cfg => cfg.MapFrom(src => src.ArticleDesc))
              .ForMember(dest => dest.PREPACK_QTY, cfg => cfg.MapFrom(src => src.PrepackQty))
              .ForMember(dest => dest.PURCHASE_TOT_QTY, cfg => cfg.MapFrom(src => src.PurchaseTotQty))
              .ForMember(dest => dest.PREPACK_QTY_EACHES, cfg => cfg.MapFrom(src => src.PrepackQtyEaches))
              .ForMember(dest => dest.EACHES, cfg => cfg.MapFrom(src => src.Eaches))
              .ReverseMap();


        }
    }
}
