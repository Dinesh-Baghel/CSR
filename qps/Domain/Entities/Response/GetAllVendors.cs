using Domain.Entities.Base;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class GetAllVendors : BaseResponse
    {
        public List<VendorModel> VendorsList { get; set; } = new();
    }
    public class GetAddressByVendorCode : BaseResponse
    {
        public List<VendorAddressModel> AddressList { get; set; } = new();
    }

    public class GetVendorByCode : BaseResponse
    {
        public VendorModel VendorModel { get; set; } = new();
        public List<VendorAddressModel> VendorAddressList { get; set; }=new();
    }

}
