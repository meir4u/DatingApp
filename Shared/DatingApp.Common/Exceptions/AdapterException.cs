using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Common.Exceptions
{
    public class AdapterException : Exception
    {
        public AdapterException() { }

        public AdapterException(string message) : base(message) { }

        public AdapterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
