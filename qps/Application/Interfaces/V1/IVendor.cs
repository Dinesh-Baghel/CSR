using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IVendor
    {
        public Task<GenRes> SetVendor(Vendor req);
        public Task<GetAllVendors> GetAllVendors();
        public Task<GetAddressByVendorCode> GetAddressOnVendorCode(SelectListReq req);
        public Task<GetVendorByCode> GetVendorByID(SelectListReq req);

        public Task<PODashboardResponse> GetAllVendorWisePOs(SelectListReq req);
        public Task<POResponse> GetPODetails(SelectListReq req);

        public Task<GenRes> SaveInspectionRequest(POHeader req);
        public Task<InspectionDetailResponse> GetInspectionRequestByID(SelectListReq req);
        public Task<MasterDataList> GetMasterData(SelectListReq req);
        public Task<GenRes> AddAddress(Vendor req);
        public Task<GenRes> UpdateDOI(UpdateDOIRequest req);
        public Task<GenRes> AssignInspector(AssignInspectorRequest req);
        public Task<GenRes> SaveInspection(InspectionModel req);

    }
}
