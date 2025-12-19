using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class RoleMaster : Modules
    {
        public int Id { get; set; }
        public string? Role_Name { get; set; }
        public bool Active_Status { get; set; }
        public int Inserted_Updated_By { get; set; }
    }
}
