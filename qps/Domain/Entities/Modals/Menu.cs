using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class Menu : BaseRequest
    {
        public int Id { get; set; } = 0;
        public string? Label { get; set; }
        public string? Url { get; set; }
        public int Order { get; set; }
        public int Parent_Id { get; set; }
        public bool Website_Visibility { get; set; }
        public bool Active_Status { get; set; }
        public string? Page_Title { get; set; }
        public string? Page_Description { get; set; }
        public string? Page_Keywords { get; set; }
        public string? Permission { get; set; }
        public List<RoleList> RoleList { get; set; } = new();
    }
}
