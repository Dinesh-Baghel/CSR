using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class QuestionAnswerRequest
    {
        [Required(ErrorMessage = "Question Id is required.")]
        public string? questionId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? remark { get; set; }

        
        public string[]? imageFile { get; set; } = null;

    }
    public class AnswerDetailsRequest
    {
        public string? refernceNumber { get; set; }
    }
    public class AnswerRequest
    {
         public string? userName { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? submitDateTime { get; set; } = string.Empty;

        public string? storeId { get; set; }
        public string? submitBy { get; set; }
        public List<QuestionAnswerRequest> questionReply { get; set; }
    }

    public class AnswerListRequest : Pagination, IFilterOptions
    {
        public string fromDate { get; set; } = string.Empty;
        public string uptoDate { get; set; } = string.Empty;
    }
}
