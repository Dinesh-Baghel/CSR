using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class UpdateDOIRequest
    {
        public int InspId { get; set; }
        public DateTime? QA_ASSIGNED_DOI { get; set; }
        public int CreatedBy { get; set; }
    }
}
