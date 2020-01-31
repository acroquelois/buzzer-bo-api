using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Exceptions
{
    public class NoResponseException : Exception
    {
        public NoResponseException()
        {
        }

        public NoResponseException(string message)
            : base(message)
        {
        }

        public NoResponseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}