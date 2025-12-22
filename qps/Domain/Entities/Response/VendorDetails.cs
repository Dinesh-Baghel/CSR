using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities.Response
{
    public class VendorResponse
    {
        [JsonPropertyName("VENDOR_DETAILS")]
        public List<VendorDetail> VendorDetails { get; set; }
    }

    public class VendorDetail
    {
        [JsonPropertyName("VENDOR_CODE")]
        public string VendorCode { get; set; }

        [JsonPropertyName("VENDOR_NAME")]
        public string VendorName { get; set; }

        [JsonPropertyName("CITY")]
        public string City { get; set; }

        [JsonPropertyName("POST_CODE")]
        public string PostCode { get; set; }

        [JsonPropertyName("ADDRESS")]
        public string Address { get; set; }

        [JsonPropertyName("REGION")]
        public string Region { get; set; } = "";

        [JsonPropertyName("REGION_DESC")]
        public string RegionDescription { get; set; }

        [JsonPropertyName("GST")]
        public string GSTNo { get; set; }

        [JsonPropertyName("TELEPHONE_NUMBER")]
        public string TelephoneNumber { get; set; }

        [JsonPropertyName("TELEPHONE_NUMBER2")]
        public string TelephoneNumber2 { get; set; }

        [JsonPropertyName("TELEPHONE_NUMBER3")]
        public string TelephoneNumber3 { get; set; }

        public string ContactNo { get; set; }
        public string EmailId { get; set; }

        public bool IsDefault { get; set; }

        public Guid VendorUId { get; set; }




    }

    public class VendorAddressDetail
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; } = "";
        public string AddressLine2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PinCode { get; set; } = "";
        public string Email_Ids { get; set; } = "";
        public string Mobile_Numbers { get; set; } = "";
        public bool IsDefault { get; set; }
        public string Region { get; set; } = "";
        public string RegionDescription { get; set; } = "";
        public int Created_By { get; set; }
        public string Created_Date { get; set; } = "";
        public string Updated_Date { get; set; } = "";
        public Guid AddressGUId { get; set; }
    }

    public class Vendor
    {
        public int VendorId { get; set; } = 0;
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string GSTNo { get; set; } = "";
        public string PassWord { get; set; } = "";
        public bool Default_Password_Changed { get; set; }
        public bool IsActive { get; set; }
        public bool IsSentEmail { get; set; }
        public string LastLoginDate { get; set; } = "";
        public string CurrentLoginDate { get; set; } = "";
        public string IPAddress { get; set; } = "";
        public string CreatedDate { get; set; } = "";
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; } = "";
        public string ModifiedDate { get; set; } = "";
        public int ModifiedBy { get; set; }
        public List<VendorAddressDetail> VendorAddresses { get; set; } = new List<VendorAddressDetail>();

        public Guid VendorGUId { get; set; }
    }


}
