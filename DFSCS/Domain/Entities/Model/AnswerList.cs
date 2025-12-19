using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Model
{
    public class Answerdata
    {
        [Column("RN")]
        public int rowNumber { get; set; }
        [Column("REFERENCE_NO")]
        public string? referenceNo { get; set; }
        [Column("SUBMITED_DATE")]
        public DateTime? submitedDate { get; set; }
        [Column("USERNAME")]
        public string? userName { get; set; }
        [Column("LATITUDE")]
        public string? latitude { get; set; }
        [Column("LONGITUTDE")]
        public string? longitutde { get; set; }
        [Column("STORE_CODE")]
        public string? storeCode { get; set; }
        [Column("SUBMIT_BY")]
        public string? submitBy { get; set; }
    }

    public class AnswerList : ApiBaseResponse
    {
        public List<Answerdata> answerlistData { get; set; } = new();
    }
}
