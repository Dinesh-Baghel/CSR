using Domain.Entities.Base;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class POSAPResponse : BaseResponse
    {
        public List<SAP_PO_DetailDto> APPS_FOOTWEAR_DETAILS { get; set; }
    }

    public class SAP_PO_DetailDto
    {
        public string PO_NUMBER { get; set; }
        public string PO_LINE_ITEM { get; set; }
        public string ARTICLE { get; set; }
        public string ARTICLE_DESC { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string PREPACK_QTY { get; set; }
        public string PURCHASE_TOT_QTY { get; set; }
        public string PREPACK_QTY_EACHES { get; set; }
        public string EACHES { get; set; }
        public string COLOR { get; set; }
        public string FABRIC { get; set; }


    }

    public class POResponse : BaseResponse
    {
        public PO_HEADER_MODEL POHeader { get; set; } = new();
        public List<PO_DETAILS_MODEL> PODetails { get; set; } = new();
    }

    public class PODashboardResponse : BaseResponse
    {
        public List<PO_HEADER_MODEL> POList { get; set; } = new();
    }

    public class InspectionDetailResponse : BaseResponse
    {
        public PO_HEADER_MODEL POHeader { get; set; } = new();
        public List<PO_DETAILS_MODEL> PODetails { get; set; } = new();
        public List<VendorAddressModel> AddressList { get; set; } = new();

    }

    public class MasterData
    {
        public int Master_ID { get; set; }
        public string Master_Name { get; set; } = "";


    }
    public class MasterDataList : BaseResponse
    {
        public List<MasterData> masterDataList { get; set; } = new();
    }
}
