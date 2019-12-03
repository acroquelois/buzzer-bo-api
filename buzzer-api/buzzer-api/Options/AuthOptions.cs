using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Options
{
    public class AuthOptions
    {
        public string Key { get; set; }

        public int ExpireMinutes { get; set; }
    }
}
