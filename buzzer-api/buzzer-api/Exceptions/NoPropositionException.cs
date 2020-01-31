using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Exceptions
{
    public class NoPropositionException : Exception
    {
        public NoPropositionException()
        {
        }

        public NoPropositionException(string message)
            : base(message)
        {
        }

        public NoPropositionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
