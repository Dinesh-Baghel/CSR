using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ErrorLogger
{
    public class ValueNotHandledException : Exception
    {
        public ValueNotHandledException(string message, string? innerMessage = null)
            : base(innerMessage ?? message) { }

        public ValueNotHandledException(string message, string? innerMessage, bool toBeLogged)
            : base(innerMessage ?? message)
        {

        }
    }
}
