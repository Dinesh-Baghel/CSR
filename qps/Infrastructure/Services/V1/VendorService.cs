using Application.Interfaces.V1;
using Dapper;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Utilitys;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class VendorService : IVendor
    {
        private readonly DapperHelper _dapperHelper;

        public VendorService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }


        public async Task<GenRes> SetVendor(Vendor req)
        {
            var Res = new GenRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.VendorId, DbType.Int32);
            parameters.Add("@Vendor_Code", req.VendorCode, DbType.String);
            parameters.Add("@Vendor_Name", req.VendorName, DbType.String);
            parameters.Add("@Password", req.PassWord, DbType.String);
            parameters.Add("@DEFAULT_PASSWORD_CHANGED", req.Default_Password_Changed, DbType.String);
            parameters.Add("@GSTNo", req.GSTNo, DbType.String);
            parameters.Add("@IsActive", req.IsActive, DbType.Boolean);
            parameters.Add("@IsEmailSent", req.IsSentEmail, DbType.Boolean);
            parameters.Add("@Inserted_Updated_By", req.CreatedBy, DbType.Int32);


            // Table-valued parameter
            var addressDetailsTable = new DataTable();
            addressDetailsTable.Columns.Add("ADDRESS_ID", typeof(int));
            addressDetailsTable.Columns.Add("Address", typeof(string));
            addressDetailsTable.Columns.Add("City", typeof(string));
            addressDetailsTable.Columns.Add("PinCode", typeof(string));
            addressDetailsTable.Columns.Add("Region", typeof(string));
            addressDetailsTable.Columns.Add("Region_Description", typeof(string));
            addressDetailsTable.Columns.Add("Moble_Numbers", typeof(string));
            addressDetailsTable.Columns.Add("Email_Ids", typeof(string));
            addressDetailsTable.Columns.Add("Is_Default", typeof(bool));
            foreach (var address in req.VendorAddresses)
            {
                addressDetailsTable.Rows.Add(address.AddressId, address.AddressLine1, address.City, address.PinCode, address.Region, address.RegionDescription, address.Mobile_Numbers, address.Email_Ids, address.IsDefault);
            }
            parameters.Add("@ADDRESSES", addressDetailsTable.AsTableValuedParameter("AddressDetails"));

            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("PRO_INSERT_UPDATE_VENDOR_MASTER", parameters);
            if (!parameters.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            return Res;
        }

        public async Task<GetAllVendors> GetAllVendors()
        {
            var Res = new GetAllVendors();

            var parameters = new
            {
                Id = 0,
                StrField = "",
                Cmd = "Get_All_VENDORS"
            };
            var Out = await _dapperHelper.ExecuteStoredProcedureListAsync<VendorModel>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
            Res.VendorsList = (List<VendorModel>)Out;
            return Res;
        }

        public async Task<GetAddressByVendorCode> GetAddressOnVendorCode(SelectListReq req)
        {
            var Res = new GetAddressByVendorCode();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);//Get_VENDORS_ADDRESS
            var OutSet = await _dapperHelper.ExecuteStoredProcedureListAsync<VendorAddressModel>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
            Res.AddressList = OutSet.Count() > 0 ? (List<VendorAddressModel>)OutSet : new List<VendorAddressModel>();

            return Res;
        }

        public async Task<GetVendorByCode> GetVendorByID(SelectListReq req)
        {
            var Res = new GetVendorByCode();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", req.Id, DbType.Int32);
                parameters.Add("@StrField", req.StrField, DbType.String);
                parameters.Add("@Cmd", req.Cmd, DbType.String);//GET_VENDOR_BY_CODE
                var (vendors, addresses) = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<VendorModel, VendorAddressModel>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
                Res.VendorModel = vendors?.FirstOrDefault() ?? new VendorModel();
                Res.VendorAddressList = addresses?.ToList() ?? new List<VendorAddressModel>();
            }
            catch (Exception ex)
            {
                Res.responseCode = 1;
                Res.responseMessage = ex.Message;
            }
            return Res;

        }

        public async Task<PODashboardResponse> GetAllVendorWisePOs(SelectListReq req)
        {
            var Res = new PODashboardResponse();

            var parameters = new
            {
                Id = req.Id,
                StrField = req.StrField!,
                Cmd = req.Cmd!
            };
            var Out = await _dapperHelper.ExecuteStoredProcedureListAsync<PO_HEADER_MODEL>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
            Res.POList = (List<PO_HEADER_MODEL>)Out;
            return Res;
        }

        public async Task<POResponse> GetPODetails(SelectListReq req)
        {
            var Res = new POResponse();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", req.Id, DbType.Int32);
                parameters.Add("@StrField", req.StrField, DbType.String);
                parameters.Add("@Cmd", req.Cmd, DbType.String);//GET_VENDOR_BY_CODE
                var (PO, Details) = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<PO_HEADER_MODEL, PO_DETAILS_MODEL>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
                Res.POHeader = PO?.FirstOrDefault() ?? new PO_HEADER_MODEL();
                Res.PODetails = Details?.ToList() ?? new List<PO_DETAILS_MODEL>();
            }
            catch (Exception ex)
            {
                Res.responseCode = 1;
                Res.responseMessage = ex.Message;
            }
            return Res;
        }

        public async Task<GenRes> SaveInspectionRequest(POHeader req)
        {
            var Res = new GenRes();


            var headerParams = new DynamicParameters();

            headerParams.Add("@POID", req.Id, DbType.Int32, ParameterDirection.InputOutput);
            headerParams.Add("@PO_NUMBER", req.PoNumber);
            headerParams.Add("@VENDOR_CODE", req.VendorCode);
            headerParams.Add("@VENDOR_NAME", req.VendorName);
            headerParams.Add("@FABRIC", req.Fabric);
            headerParams.Add("@COLOR", req.Color);
            headerParams.Add("@TOTAL_QTY", req.POTotalQty);
            headerParams.Add("@USER_ID", req.CreatedBy);

            // inspection
            headerParams.Add("@INSP_ID", req.InspId);
            headerParams.Add("@INSPECTION_DATE", req.DateOfInspection);
            headerParams.Add("@OFFERED_QTY", req.OfferedQty);
            headerParams.Add("@ADDRESS_ID", req.AddressId);
            headerParams.Add("@REGION_ID", req.RegionId);

            // table valued parameter
            var poDetailsTable = new DataTable();
            poDetailsTable.Columns.Add("PO_DETAILS_ID", typeof(int));
            poDetailsTable.Columns.Add("LINE_ITEM", typeof(string));
            poDetailsTable.Columns.Add("ARTICLE", typeof(string));
            poDetailsTable.Columns.Add("ARTICLE_DESC", typeof(string));
            poDetailsTable.Columns.Add("PREPACK_QTY", typeof(decimal));
            poDetailsTable.Columns.Add("PURCHASE_TOT_QTY", typeof(decimal));
            poDetailsTable.Columns.Add("PREPACK_QTY_EACHES", typeof(decimal));
            poDetailsTable.Columns.Add("EACHES", typeof(decimal));
            poDetailsTable.Columns.Add("CREATED_BY", typeof(int));
            poDetailsTable.Columns.Add("UPDATED_BY", typeof(int));
            foreach (var dtl in req.Details)
            {
                poDetailsTable.Rows.Add(dtl.Id, dtl.LineItem, dtl.Article, dtl.ArticleDesc, dtl.PrepackQty, dtl.PurchaseTotQty, dtl.PrepackQtyEaches, dtl.Eaches, req.CreatedBy, req.CreatedBy);
            }

            headerParams.Add("@DETAILS", poDetailsTable.AsTableValuedParameter("TVP_PODETAIL"), dbType: DbType.Object, direction: ParameterDirection.Input);

            //Output parameters
            headerParams.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            headerParams.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            headerParams.Add("@RECORD_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("SP_SAVE_PO", headerParams);
            int newPOID = headerParams.Get<int>("@POID");
            int INSPID = headerParams.Get<int>("@RECORD_ID");
            if (!headerParams.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = headerParams.Get<int>("Error_Code");
                Res.responseMessage = headerParams.Get<string>("Error_Message");
            }
            Res.recordId = INSPID;
            return Res;
        }

        public async Task<InspectionDetailResponse> GetInspectionRequestByID(SelectListReq req)
        {
            var Res = new InspectionDetailResponse();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", req.Id, DbType.Int32);
                parameters.Add("@StrField", req.StrField, DbType.String);
                parameters.Add("@Cmd", req.Cmd, DbType.String);
                var (PO, Details, AddressList) = await _dapperHelper.ExecuteStoredProcedureMultiple3ListAsync<PO_HEADER_MODEL, PO_DETAILS_MODEL, VendorAddressModel>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
                Res.POHeader = PO?.FirstOrDefault() ?? new PO_HEADER_MODEL();
                Res.PODetails = Details?.ToList() ?? new List<PO_DETAILS_MODEL>();
                Res.AddressList = AddressList?.ToList() ?? new List<VendorAddressModel>();
            }
            catch (Exception ex)
            {
                Res.responseCode = 1;
                Res.responseMessage = ex.Message;
            }
            return Res;

        }

        public async Task<MasterDataList> GetMasterData(SelectListReq req)
        {
            var Res = new MasterDataList();
            try
            {
                var parameters = new
                {
                    Id = 0,
                    StrField = req.StrField!,
                    Cmd = req.Cmd!
                };
                var Out = await _dapperHelper.ExecuteStoredProcedureListAsync<MasterData>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
                Res.masterDataList = (List<MasterData>)Out;

            }
            catch (Exception ex)
            {
                Res.responseCode = 1;
                Res.responseMessage = ex.Message;
            }
            return Res;
        }

        public async Task<GenRes> AddAddress(Vendor req)
        {
            var Res = new GenRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@VENDOR_CODE", req.VendorCode, DbType.String);
            parameters.Add("@Inserted_Updated_By", req.CreatedBy, DbType.Int32);


            // Table-valued parameter
            var addressDetailsTable = new DataTable();
            addressDetailsTable.Columns.Add("ADDRESS_ID", typeof(int));
            addressDetailsTable.Columns.Add("Address", typeof(string));
            addressDetailsTable.Columns.Add("City", typeof(string));
            addressDetailsTable.Columns.Add("PinCode", typeof(string));
            addressDetailsTable.Columns.Add("Region", typeof(string));
            addressDetailsTable.Columns.Add("Region_Description", typeof(string));
            addressDetailsTable.Columns.Add("Moble_Numbers", typeof(string));
            addressDetailsTable.Columns.Add("Email_Ids", typeof(string));
            addressDetailsTable.Columns.Add("Is_Default", typeof(bool));
            foreach (var address in req.VendorAddresses)
            {
                addressDetailsTable.Rows.Add(address.AddressId, address.AddressLine1, address.City, address.PinCode, address.Region, address.RegionDescription, address.Mobile_Numbers, address.Email_Ids, address.IsDefault);
            }
            parameters.Add("@ADDRESSES", addressDetailsTable.AsTableValuedParameter("AddressDetails"));

            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            parameters.Add("@RECORD_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("PRO_INSERT_NEW_VENDOR_ADDRESS", parameters);
            if (!parameters.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            if (!parameters.Get<int>("RECORD_ID").Equals(0))
            {
                Res.recordId = parameters.Get<int>("RECORD_ID");
            }
            return Res;
        }

        public async Task<GenRes> UpdateDOI(UpdateDOIRequest req)
        {
            var Res = new GenRes();

            var headerParams = new DynamicParameters();
            headerParams.Add("@USER_ID", req.CreatedBy);
            headerParams.Add("@INSP_ID", req.InspId);
            headerParams.Add("@QA_ASSIGNED_DOI", req.QA_ASSIGNED_DOI);

            //Output parameters
            headerParams.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            headerParams.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("SP_DOI_ASSIGNED_BY_QA", headerParams);
            if (!headerParams.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = headerParams.Get<int>("Error_Code");
                Res.responseMessage = headerParams.Get<string>("Error_Message");
            }
            return Res;
        }

        public async Task<GenRes> AssignInspector(AssignInspectorRequest req)
        {
            var Res = new GenRes();

            var headerParams = new DynamicParameters();
            headerParams.Add("@USER_ID", req.CreatedBy);
            headerParams.Add("@INSP_ID", req.InspId);
            headerParams.Add("@INSPECTOR_EMP_CODE", req.INSPECTOR_EMP_CODE);

            //Output parameters
            headerParams.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            headerParams.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("SP_ASSIGNED_INSPECTOR", headerParams);
            if (!headerParams.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = headerParams.Get<int>("Error_Code");
                Res.responseMessage = headerParams.Get<string>("Error_Message");
            }
            return Res;
        }

        public async Task<GenRes> SaveInspection(InspectionModel request)
        {
            var Res = new GenRes();

            var defectTable = new DataTable();
            defectTable.Columns.Add("DEFECTS_ID", typeof(int));
            defectTable.Columns.Add("INSPECTION_ID", typeof(int));
            defectTable.Columns.Add("DEFECTS", typeof(string));
            defectTable.Columns.Add("DIMENSION", typeof(string));
            defectTable.Columns.Add("MAJOR", typeof(int));
            defectTable.Columns.Add("MINOR", typeof(int));

            foreach (var d in request.Defects)
            {
                defectTable.Rows.Add(d.Defects_Id, request.InspectionId, d.Defects, d.Dimension, d.Major, d.Minor);
            }

            var parameters = new DynamicParameters();

            parameters.Add("@INSPECT_ID", request.InspectId);
            parameters.Add("@INSPECTION_ID", request.InspectionId);
            parameters.Add("@INSPECTOR_CODE", request.InspectorCode);
            parameters.Add("@GPT_NO", request.GptNo);
            parameters.Add("@GPT_STATUS", request.GptStatus);
            parameters.Add("@GPT_APPROVED_BY", request.GptApprovedBy);
            parameters.Add("@GPT_APPROVED_DATE", request.GptApprovedDate);

            parameters.Add("@NO_OF_BAGS_CARTONS_QTY", request.NoOfBagsCartonsQty);
            parameters.Add("@NO_OF_BAGS_CARTONS_UNIT", request.NoOfBagsCartonsUnit);
            parameters.Add("@PACKING_STATUS", request.PackingStatus);
            parameters.Add("@INLINE_DONE", request.InlineDone);
            parameters.Add("@PACKING_LIST", request.PackingList);

            parameters.Add("@PPS_RELEASE_DATE", request.PpsReleaseDate);
            parameters.Add("@PPS_EXPIRY_DATE", request.PpsExpiryDate);

            parameters.Add("@FIBER_CONTENT_ON_PO", request.FiberContentOnPo);
            parameters.Add("@FIBER_CONTENT_ON_TEST_REPORT", request.FiberContentOnTestReport);
            parameters.Add("@FIBER_CONTENT_ON_WC", request.FiberContentOnWc);

            parameters.Add("@GSM_ON_PO", request.GsmOnPo);
            parameters.Add("@GSM_ON_TEST_REPORT", request.GsmOnTestReport);
            parameters.Add("@GSM_ON_BULK", request.GsmOnBulk);

            parameters.Add("@MEASUREMENT_SAMPLE_SIZE", request.MeasurementSampleSize);
            parameters.Add("@MEASUREMENT_ATTACHMENT", request.MeasurementAttachment);

            parameters.Add("@NO_OF_CHECK", request.NoOfCheck);
            parameters.Add("@NO_OF_CHECK_UNIT", request.NoOfCheckUnit);
            parameters.Add("@NO_OF_PREPACK_CHECK", request.NoOfPrepackCheck);

            parameters.Add("@WORKMANSHIP", request.Workmanship);
            parameters.Add("@AQL", request.Aql);
            parameters.Add("@AQL_SAMPLE_SIZE", request.AqlSampleSize);

            parameters.Add("@DEFECTS_ALLOWED", request.DefectsAllowed);
            parameters.Add("@DEFECTS_STATUS", request.DefectsStatus);
            parameters.Add("@INSPECTION_RESULT", request.InspectionResult);

            parameters.Add("@REMARK_COMMENT", request.RemarkComment);
            parameters.Add("@NOTE", request.Note);
            parameters.Add("@NAME_OF_FACTORY_REPRESENTATIVE", request.NameOfFactoryRepresentative);

            parameters.Add("@TEST_REPORT_ATTACHMENT", request.TestReportAttachment);
            parameters.Add("@GEN_ATTACHMENT_FIRST", request.GenAttachmentFirst);
            parameters.Add("@GEN_ATTACHMENT_SECOND", request.GenAttachmentSecond);

            parameters.Add("@IS_COMPLETE", request.IsComplete);
            parameters.Add("@USER_ID", request.UserId);

            parameters.Add("@DEFECTS", defectTable.AsTableValuedParameter("dbo.TVP_INSPECTION_DEFECTS"), dbType: DbType.Object, direction: ParameterDirection.Input);

            parameters.Add("@OUT_INSPECT_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ERROR_CODE", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ERROR_MESSAGE", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("dbo.SP_SAVE_INSPECTION", parameters);


            Res.recordId = parameters.Get<int>("@OUT_INSPECT_ID");
            Res.responseCode = parameters.Get<int>("@ERROR_CODE");
            Res.responseMessage = parameters.Get<string>("@ERROR_MESSAGE");


            return Res;
        }



    }
}
