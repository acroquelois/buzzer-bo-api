using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Exceptions
{
    public class NoQuestionFoundException:Exception
    {
        public NoQuestionFoundException()
        {
        }

        public NoQuestionFoundException(string message)
            : base(message)
        {
        }

        public NoQuestionFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
