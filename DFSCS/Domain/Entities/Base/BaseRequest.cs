using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public abstract class BaseRequest
    {
        public int Inserted_Updated_By { get; set; } = 0;
    }
}
