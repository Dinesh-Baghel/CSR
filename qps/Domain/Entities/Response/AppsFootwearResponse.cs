using System.Text.Json.Serialization;

namespace Domain.Entities.Response
{
    public class AppsFootwearResponse
    {
        [JsonPropertyName("APPS_FOOTWEAR_DETAILS")]
        public List<AppsFootwearDetail> AppsFootwearDetails { get; set; } = new List<AppsFootwearDetail>();
    }

    public class AppsFootwearDetail
    {
        [JsonPropertyName("PO_NUMBER")]
        public string PoNumber { get; set; }

        [JsonPropertyName("PO_LINE_ITEM")]
        public string PoLineItem { get; set; }

        [JsonPropertyName("ARTICLE")]
        public string Article { get; set; }

        [JsonPropertyName("ARTICLE_DESC")]
        public string ArticleDesc { get; set; }

        [JsonPropertyName("VENDOR_CODE")]
        public string VendorCode { get; set; }

        [JsonPropertyName("VENDOR_NAME")]
        public string VendorName { get; set; }

        [JsonPropertyName("PREPACK_QTY")]
        public string PrepackQty { get; set; }

        [JsonPropertyName("PURCHASE_TOT_QTY")]
        public string PurchaseTotQty { get; set; }

        [JsonPropertyName("PREPACK_QTY_EACHES")]
        public string PrepackQtyEaches { get; set; }

        [JsonPropertyName("EACHES")]
        public string Eaches { get; set; }

        [JsonPropertyName("COLOR")]
        public string Color { get; set; }

        [JsonPropertyName("FABRIC")]
        public string Fabric { get; set; }
    }
}




