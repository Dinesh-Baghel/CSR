using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class AppsBsr
    {
        public string? MATERIAL { get; set; }
        public string? MATERIAL_DESC { get; set; }
        public string? CALENDAR_WEEK { get; set; }
        public string? SEASON { get; set; }
        public string? DIV { get; set; }
        public string? DIV_DESC { get; set; }
        public string? MC { get; set; }
        public string? MC_DESC { get; set; }
        public decimal MRP { get; set; }
        public string? GENERIC_MATERIAL { get; set; }
        public string? GENERIC_MATERIAL_DESC { get; set; }
        public string? PRICE_BAND_CATEGORY { get; set; }
        public string? COLOR { get; set; }
        public string? VENDOR { get; set; }
        public string? VENDOR_NAME { get; set; }
        public string? MOTHER_COMPANY { get; set; }
        public string? BS_ARTICLE_KEY { get; set; }
        public string? BS_ARTICLE_TEXT { get; set; }
        public string? BRAND_TYPE { get; set; }
        public string? ARTICLE_TYPE { get; set; }
        public string? REMARKS { get; set; }
        public string? BS_FLAG { get; set; }
        public string? LAST_WEEK_REMARKS { get; set; }
        public string? BS_REPEAT_REMARK { get; set; }
        public string? TOP_BS_WEEK_REMARKS { get; set; }
        public decimal GRC_QTY { get; set; }
        public decimal PENDING_PO_QTY { get; set; }
        public decimal WEEK_ON_FLOOR { get; set; }
        public decimal STR_PUR_WK1 { get; set; }
        public decimal STR_PUR_WK2 { get; set; }
        public decimal STR_PUR_WK3 { get; set; }
        public decimal SELL_THRU_WK1 { get; set; }
        public decimal SELL_THRU_WK2 { get; set; }
        public decimal SELL_THRU_WK3 { get; set; }
        public decimal SELL_THRU_CUMMULATIVE { get; set; }
        public decimal TOTAL_SELL_THRU { get; set; }
        public decimal AVG_PERDAY_SELL_THRU { get; set; }
        public decimal BS_SELL_THRU_CRITERIA { get; set; }
        public decimal BS_AVG_PERDAY_SELL_THRU { get; set; }
        public string? IMAGE_URL { get; set; }
        public int TotalCount { get; set; }
    }

}
