using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Exceptions
{
    public class NoMediaException: Exception
    {
        public NoMediaException()
        {
        }

        public NoMediaException(string message)
            : base(message)
        {
        }

        public NoMediaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
