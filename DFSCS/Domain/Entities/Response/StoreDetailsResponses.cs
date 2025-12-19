using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class StoreDetails
    {
        [Column("STORE_CODE")]
        public string? storeCode { get; set; }
        [Column("STORE_NAME")]
        public string? storeName { get; set; }
        [Column("COMPANY_NAME")]
        public string? companyName { get; set; }
        [Column("ADDRESS")]
        public string? address { get; set; }
        [Column("PIN_CODE")]
        public string? pinCode { get; set; }
        [Column("MOBILE")]
        public string? mobile { get; set; }
        [Column("LATITUDE")]
        public string? lattitude { get; set; }
        [Column("LONGITUDE")]
        public string? longitude { get; set; }
        [Column("TOLLERENCE_METER")]
        public int tollerenceMeter { get; set; }
        [Column("IS_ACTIVE")]
        public bool isActive { get; set; }
        [Column("DISTANCE_METER")]
        public decimal distanceMeter { get; set; }
    }

    public class StoreDetailsResponses : ApiBaseResponse
    {
        public List<StoreDetails>? storeDetails { get; set; }
    }
}
