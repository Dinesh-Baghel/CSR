using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class MenusList
    {
        public int Id { get; set; }
        public int Parent_Id { get; set; }
        public string? Label { get; set; }
        public string? Url { get; set; }
    }
}
