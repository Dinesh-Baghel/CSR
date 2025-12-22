using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class RoleList
    {
        public int Id { get; set; }
        public string? Role_Name { get; set; }
        public string? Permission { get; set; }
    }
}
