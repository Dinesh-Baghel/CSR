using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class GenericArticleResponse
    {
        [JsonPropertyName("Generic Articles")]
        public List<GenericArticleItem> GenericArticles { get; set; }
    }

    public class GenericArticleItem
    {
        public int Request_Id { get; set; }
        public string? Generic_Article { get; set; }
        [JsonPropertyName("Article_desc")]
        public string? Article_Description { get; set; }
        public string? Color { get; set; }
        public string? Mc { get; set; }
        [JsonPropertyName("MC_DESC")]
        public string? Mc_Description { get; set; }
        [JsonPropertyName("DIVI")]
        public string? Division { get; set; }
        [JsonPropertyName("DIVI_DESC")]
        public string? Division_Description { get; set; }
        public string? Vendor_Code { get; set; }
        public string? Vendor_Name { get; set; }
        public string? Mrp { get; set; }

        public int ERRORCODE { get; set; }
        public string? ERRORMESSAGE { get; set; }

        public string PP_Type { get; set; }
    }


    public class PpsRequest : GenericArticleItem
    {
        //public int Request_Id { get; set; }
        public string Buyer_Id { get; set; }
        public string Title
        {
            get
            {
                var srchtext = base.Generic_Article
                    + "" + base.Article_Description
                    + "" + base.Mc
                    + "" + base.Mc_Description
                    + "" + base.Division
                    + "" + base.Division_Description
                    + "" + base.Vendor_Code
                    + "" + base.Vendor_Name
                    + "" + base.Color
                    + "" + PP_Type
                    + " " + Qty
                    + " " + Status
                    + "" + Created_At
                    + " " + CREATED_BY
                    + " " + VERIFIED_BY
                    + "" + VERIFIED_AT;
                return srchtext;
            }
        }
        public string Status { get; set; }
        public int STATUS_ID { get; set; }
        public string Description { get; set; } = "";
        public decimal Pps_Mrp
        {
            get
            {
                var mrp = (Mrp != "" && Mrp != null) ? Convert.ToDecimal(Mrp.ToString()) : 0;
                return mrp;
            }
        }

        public int Qty { get; set; }
        public string ATTACHMENTS { get; set; } = "";
        public string ATTACHMENTS_STEP2 { get; set; } = "";
        public string ATTACHMENTS_STEP3 { get; set; } = "";
        public DateTime Created_At { get; set; }
        public string VERIFIED_AT { get; set; } = "";
        public string CREATED_BY { get; set; } = "";
        public string VERIFIED_BY { get; set; } = "";
    }

    public class UpdateStausPpsRequest
    {
        public int Request_Id { get; set; }
        public string User_Id { get; set; }
        public int StatusId { get; set; }
        public string Comments { get; set; } = "";
        public string ATTACHMENTS { get; set; } = "";
    }

    public class PpsLogHistory
    {
        public int REQUEST_ID { get; set; }
        public int STATUS_ID { get; set; }
        public string LAST_LOG { get; set; }
        public int ATTEMPT_COUNT { get; set; }
        public int VERIFIED_BY { get; set; }
        public DateTime VERIFIED_AT { get; set; }
        public string COMMENTS { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_DEPT_NAME { get; set; }
        public string STATUS { get; set; }



    }

}



