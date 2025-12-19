using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Model
{
    public class AnswerDetailsitem
    {

      
        [Column("REFERENCE_NO")]
        public string? referenceNo { get; set; }
     
        [Column("STORE_CODE")]
        public string? storeCode { get; set; }
        [Column("QUESTION")]
        public string? quetion { get; set; }
        [Column("ANSWER")]
        public string? answer { get; set; }

        [Column("IMG_FILE")]
        public string? imgfile { get; set; }
        public string[] imgBase { get; set; }
    }

    public class AnswerDetails:ApiBaseResponse
    {

        public List<AnswerDetailsitem> answerDetails { get; set; } 

     
    }
}
