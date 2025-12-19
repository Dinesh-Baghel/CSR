using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AnswerListDto
    {
        public int RN { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime? SubmitedDate { get; set; }
        public string? UserName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitutde { get; set; }
        public string? StoreCode { get; set; }
        public string? SubmitBy { get; set; }
    }
}
