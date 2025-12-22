using Application.Interfaces.V1;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VendorManager : IVendorManager
    {
        private readonly IAPICall aPICall;
        private readonly IMapper mapper;

        public VendorManager(IAPICall aPICall, IMapper mapper)
        {
            this.aPICall = aPICall;
            this.mapper = mapper;
        }
        public async Task<Vendor> GetVendorDetailsFromLocal(string VendorCode)
        {
            Vendor _vendor = new();
            var selectRequest = new SelectListReq
            {
                Id = 0,
                Cmd = CmdNames.Get_Vendor_By_Code,
                ResponseType = "",
                StrField = VendorCode
            };

            var existingVendor = await aPICall.CallingAPI<GetVendorByCode, SelectListReq>(AllApiNames.GET_VENDOR_BY_CODE, selectRequest);
            if (existingVendor.responseCode == 0 && existingVendor.responseMessage!.ToUpper() == "SUCCESS")
            {
                if (!string.IsNullOrEmpty(existingVendor.VendorModel.VENDOR_CODE))
                {
                    selectRequest.Id = existingVendor.VendorModel.VENDOR_ID;
                    selectRequest.Cmd = CmdNames.Get_Vendor_Address;
                    var res = await aPICall.CallingAPI<GetAddressByVendorCode, SelectListReq>(AllApiNames.GET_VENDOR_ADDRESS, selectRequest);
                    if (res.responseCode == 0 && res.responseMessage!.ToUpper() == "SUCCESS")
                    {
                        _vendor = mapper.Map<Vendor>(existingVendor.VendorModel);
                        _vendor.VendorGUId = Guid.NewGuid();
                        _vendor.VendorAddresses.Clear();
                        foreach (var addrss in res.AddressList)
                        {
                            _vendor.VendorAddresses.Add(mapper.Map<VendorAddressDetail>(addrss));
                        }

                    }
                    return _vendor;
                }
            }

            return _vendor;

        }
    }
}
