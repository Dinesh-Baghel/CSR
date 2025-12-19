using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class RoleDetails
    {
        public int Menu_Id { get; set; }
        public int Parent_Id { get; set; }
        public string? Label { get; set; }
        public string? Permission { get; set; }
    }
}
