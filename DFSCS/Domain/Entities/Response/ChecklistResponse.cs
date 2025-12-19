using API.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class ChecklistResponse
    {
            [Column("SORT_ID")]
            public int sortId { get; set; }
            [Column("REMARK")]
            public string? remark { get; set; }
            [Column("CAT_ID")]
            public int catId { get; set; }
            [Column("IMG_REQUIRED")]
            public bool imgRequired { get; set; }
            [Column("CREATED_DATE")]
            public DateTime createdDate { get; set; }
            [Column("LASTUPDATED_DATE")]
            public DateTime lastUpdatedDate { get; set; }
            [Column("CREATE_BY")]
            public string? createBy { get; set; }
    }

    public class checklistdata : ApiBaseResponse
    {
        public List<ChecklistResponse> checklistData { get; set; } = new();
    }

}
