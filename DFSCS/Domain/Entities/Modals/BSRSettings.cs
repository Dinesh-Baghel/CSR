using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class BSRSettings
    {
        public int StartYear { get; set; }
        public int ImageDownloadLimit { get; set; }
        public int PageSize { get; set; }
        public int FirstDayOfWeek { get; set; }
        public string? DefaultSortColumns { get; set; }
        public bool sortAscending { get; set; }
    }
}
