using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class OptionValuesResponses
    {
        [Column("ID")]
        public int id { get; set; }
        [Column("REMARK")]
        public string? remark { get; set; }
        [Column("IS_ACTIVE")]
        public bool isActive { get; set; }
        [Column("CREATED_DATE")]
        public DateTime createdDate { get; set; }
        [Column("LASTUPDATED_DATE")]
        public DateTime lastUpdatedDate { get; set; }
        [Column("CREATE_BY")]
        public string? createBy { get; set; }
    }

    public class OptionValuesdata : ApiBaseResponse
    {
        public List<OptionValuesResponses> optionData { get; set; } = new();
    }
}
