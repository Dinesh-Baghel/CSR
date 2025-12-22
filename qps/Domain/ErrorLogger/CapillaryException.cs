using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ErrorLogger
{
    public class CapillaryException : Exception
    {
        public bool IsErrorSavedInApI { get; set; } = true;
        public CapillaryException(string msg) : base(msg) { }
        public CapillaryException(string msg, bool IsErrorSavedInApI) : base(msg)
        {
            this.IsErrorSavedInApI = IsErrorSavedInApI;
        }
    }
}
