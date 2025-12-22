using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class POHeader
    {
        public int Id { get; set; }
        public int InspId { get; set; }
        public string PoNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string Fabric { get; set; }
        public string Color { get; set; }
        public decimal OfferedQty { get; set; }
        public decimal POTotalQty { get; set; }
        //public DateTime? DateOfInspection { get; set; }

        public DateTime? SelectedDateDOI;
        public TimeSpan? SelectedTimeDOI;

        private DateTime? _Doi;
        public DateTime? DateOfInspection
        {
            get => SelectedDateDOI.HasValue && SelectedTimeDOI.HasValue ? SelectedDateDOI.Value.Date + SelectedTimeDOI.Value : _Doi;

            set
            {
                _Doi = value;

                if (value.HasValue)
                {
                    SelectedDateDOI = value.Value.Date;
                    SelectedTimeDOI = value.Value.TimeOfDay;
                }
                else
                {
                    SelectedDateDOI = null;
                    SelectedTimeDOI = null;
                }
            }
        }

        public string InspectionLocation { get; set; } = "";
        public int CreatedBy { get; set; }
        public string Created_By { get; set; } = "";
        public DateTime? CreatedOn { get; set; }
        public List<PODetails> Details { get; set; } = new();
        public int AddressId { get; set; }
        public int RegionId { get; set; }
        public int InspectorId { get; set; }
        public ICollection<VendorAddressDetail> AddressList { get; set; } = new List<VendorAddressDetail>();
        public ICollection<MasterData> RegionList { get; set; } = new List<MasterData>();
        public ICollection<MasterData> InspectorList { get; set; } = new List<MasterData>();
        public bool Is_Date_Assigned_By_QA { get; set; }
        public bool Is_Inspector_Assigned { get; set; }
        public string Insp_Req_Status { get; set; } = "Draft";
        //public DateTime? QA_ASSIGNED_DOI { get; set; }

        public DateTime? SelectedDate;
        public TimeSpan? SelectedTime;

        private DateTime? _qaAssignedDoi;
        public DateTime? QA_ASSIGNED_DOI
        {
            get => SelectedDate.HasValue && SelectedTime.HasValue ? SelectedDate.Value.Date + SelectedTime.Value : _qaAssignedDoi;

            set
            {
                _qaAssignedDoi = value;

                if (value.HasValue)
                {
                    SelectedDate = value.Value.Date;
                    SelectedTime = value.Value.TimeOfDay;
                }
                else
                {
                    SelectedDate = null;
                    SelectedTime = null;
                }
            }
        }


        public POHeader MapToHeader(POSAPResponse root)
        {

            var first = root.APPS_FOOTWEAR_DETAILS.First();

            var header = new POHeader
            {
                PoNumber = first.PO_NUMBER,
                VendorCode = first.VENDOR_CODE,
                VendorName = first.VENDOR_NAME,
                POTotalQty = Convert.ToDecimal(first.PURCHASE_TOT_QTY),
                Fabric = string.IsNullOrWhiteSpace(first.FABRIC!) ? root.APPS_FOOTWEAR_DETAILS.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.FABRIC!))?.FABRIC! : first.FABRIC,
                Color = string.IsNullOrWhiteSpace(first.COLOR!) ? root.APPS_FOOTWEAR_DETAILS.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.COLOR!))?.COLOR! : first.COLOR
            };

            header.Details = root.APPS_FOOTWEAR_DETAILS.Select(x => new PODetails
            {
                LineItem = x.PO_LINE_ITEM,
                Article = x.ARTICLE,
                ArticleDesc = x.ARTICLE_DESC,
                PrepackQty = Convert.ToDecimal(string.IsNullOrWhiteSpace(x.PREPACK_QTY) ? "0" : x.PREPACK_QTY),
                PurchaseTotQty = Convert.ToDecimal(x.PURCHASE_TOT_QTY),
                PrepackQtyEaches = Convert.ToDecimal(string.IsNullOrWhiteSpace(x.PREPACK_QTY_EACHES) ? "0" : x.PREPACK_QTY_EACHES),
                Eaches = Convert.ToDecimal(string.IsNullOrWhiteSpace(x.EACHES) ? "0" : x.EACHES)
            }).ToList();

            return header;
        }


    }

    public class PO_HEADER_MODEL
    {
        public int POID { get; set; }
        public int INSP_ID { get; set; }
        public string PO_NUMBER { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string FABRIC { get; set; }
        public string COLOR { get; set; }
        public decimal TOTAL_QTY { get; set; }
        public decimal OFFERED_QTY { get; set; }
        public int ADDRESS_ID { get; set; }
        public int REGION_ID { get; set; }
        public bool IS_DATE_ASSIGNED_BY_QA { get; set; }
        public bool IS_INSPECTOR_ASSIGNED { get; set; }
        public string? INSP_REQ_STATUS { get; set; }
        public int INSPECTOR_EMP_CODE { get; set; }
        public string LOCATION { get; set; } = "";
        public DateTime DATE_OF_INSPECTION { get; set; }
        public DateTime? QA_ASSIGNED_DOI { get; set; }
        public DateTime CREATED_ON { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime LAST_UPDATED_ON { get; set; }
        public string UPDATED_BY { get; set; }
        public string CREATED_BY_NAME { get; set; }
        public string CREATED_DATE { get; set; }

    }

    public class INSPECTION_REQUEST_MODEL
    {
        public int POID { get; set; }
        public string PO_NUMBER { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string FABRIC { get; set; }
        public string COLOR { get; set; }
        public decimal OFFERED_QTY { get; set; }
        public DateTime DATE_OF_INSPECTION { get; set; }
        public int ADDRESS_ID { get; set; }
        public DateTime CREATED_ON { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime LAST_UPDATED_ON { get; set; }
        public int UPDATED_BY { get; set; }

        public string CREATED_BY_NAME { get; set; }

        public string CREATED_DATE { get; set; }



    }



}

