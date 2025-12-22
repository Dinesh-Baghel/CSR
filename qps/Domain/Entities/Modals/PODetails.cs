using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class PODetails
    {
        public int Id { get; set; }
        public string LineItem { get; set; }
        public string Article { get; set; }
        public string ArticleDesc { get; set; }
        public decimal PrepackQty { get; set; }
        public decimal PurchaseTotQty { get; set; }
        public decimal PrepackQtyEaches { get; set; }
        public decimal Eaches { get; set; }
    }

    public class PO_DETAILS_MODEL
    {
        public int PO_DETAILS_ID { get; set; }
        public int POID { get; set; }
        public string LINE_ITEM { get; set; }
        public string ARTICLE { get; set; }
        public string ARTICLE_DESC { get; set; }
        public decimal PREPACK_QTY { get; set; }
        public decimal PURCHASE_TOT_QTY { get; set; }
        public decimal PREPACK_QTY_EACHES { get; set; }
        public decimal EACHES { get; set; }
        public DateTime CREATED_ON { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime LAST_UPDATED_ON { get; set; }
        public int UPDATED_BY { get; set; }
    }
}
